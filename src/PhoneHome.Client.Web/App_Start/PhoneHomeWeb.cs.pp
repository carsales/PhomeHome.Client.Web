using System;
using System.Reflection;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using PhoneHome.Client.Web;
using PhoneHome.Client.Web.Configuration;


namespace $rootnamespace$
{
    public static class PhoneHome
    {
        public static void Configure()
        {
            PhoneHomeWebConfig config = new PhoneHomeWebConfig();
			config.ApplicationKey = "$rootnamespace$";
			config.ApplicationVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            config.OnError = (type, exception) =>
            {
				// Log any PhoneHome exceptions here
            };

            PhoneHomeClientWeb.Register(config);
        }
    }
}
