using PhoneHome.Client.Constants;
using PhoneHome.Client.Interfaces;
using PhoneHome.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneHome.Client.Web.Providers
{
    internal class IISDataTagProvider : IApplicationInstanceDataTagProvider
    {
        public IList<DataTag> GetTags()
        {
            IList<DataTag> tags = new List<DataTag>();

            try
            {
                tags.Add(new DataTag("AppPoolName", GetAppPoolName()));

                string siteName = System.Web.Hosting.HostingEnvironment.SiteName;

                // Get the sites section from the AppPool.config 
                Microsoft.Web.Administration.ConfigurationSection sitesSection = Microsoft.Web.Administration.WebConfigurationManager.GetSection(null, null, "system.applicationHost/sites");

                foreach (Microsoft.Web.Administration.ConfigurationElement site in sitesSection.GetCollection())
                {
                    // Find the right Site 
                    if (string.Equals((string)site["name"], siteName, StringComparison.OrdinalIgnoreCase))
                    {

                        // For each binding see if they are http based and return the port and protocol 
                        foreach (Microsoft.Web.Administration.ConfigurationElement binding in site.GetCollection("bindings"))
                        {
                            string protocol = (string)binding["protocol"];
                            string bindingInfo = (string)binding["bindingInformation"];

                            if (protocol.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                            {
                                string[] parts = bindingInfo.Split(':');
                                if (parts.Length == 3)
                                {
                                    int port;
                                    if (int.TryParse(parts[1], out port))
                                    {
                                        //iisData.Bindings.Add(new IISBinding() { Host = parts[2], Port = port });

                                        tags.Add(new DataTag("Host", parts[2] + ":" + port));

                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                if (PhoneHomeClient.OnError != null)
                {
                    PhoneHomeClient.OnError(PhoneHomeErrorType.WebsitePortHost, e);
                }
            }

            return tags;
        }
        private static string GetAppPoolName()
        {
            try
            {
                string appPoolName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

                if (!string.IsNullOrEmpty(appPoolName))
                {
                    appPoolName = appPoolName.Replace("IIS APPPOOL\\", string.Empty);
                }

                return appPoolName;
            }
            catch (Exception exception)
            {
                if (PhoneHomeClient.OnError != null)
                {
                    PhoneHomeClient.OnError(PhoneHomeErrorType.WebsitePortHost, exception);
                }

                return null;
            }
        }
    }
}
