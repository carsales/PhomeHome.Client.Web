using PhoneHome.Client.Enums;
using PhoneHome.Client.Interfaces;
using PhoneHome.Client.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneHome.Client.Web.Configuration
{

    public class PhoneHomeWebConfig : ConfigurationSection, IPhoneHomeConfig
    {
        private static readonly PhoneHomeWebConfig _section = ConfigurationManager.GetSection("PhoneHome") as PhoneHomeWebConfig;

        private string _applicationName;
        private string _applicationKey;
        private string _applicationVersion;
        private string _environment;
        private int? _phoneHomeFrequencySeconds;
        private Guid? _apiKey;
        private string _apiHostUrl;

        /// <summary>
        /// The name of the application that is phoning home
        /// </summary>
        [ConfigurationProperty("ApplicationName", IsRequired = false)]
        public string ApplicationName
        {
            get { return (string)_section["ApplicationName"] ?? _applicationName; }
            set { _applicationName = value; }
        }

        /// <summary>
        /// A unique key for your application
        /// </summary>
        [ConfigurationProperty("ApplicationKey", IsRequired = false)]
        public string ApplicationKey
        {
            get { return (string)_section["ApplicationKey"] ?? _applicationKey; }
            set { _applicationKey = value; }
        }

        /// <summary>
        /// The version of your dll
        /// </summary>
        [ConfigurationProperty("ApplicationVersion", IsRequired = false)]
        public string ApplicationVersion
        {
            get { return (string)_section["ApplicationVersion"] ?? _applicationVersion; }
            set { _applicationVersion = value; }
        }

        /// <summary>
        /// The environment that the application is running in (Development, Staging, UAT, Production)
        /// </summary>
        [ConfigurationProperty("Environment", IsRequired = false)]
        public string Environment
        {
            get { return (string)_section["Environment"] ?? _environment; }
            set { _environment = value; }
        }

        /// <summary>
        /// How often should the phone home app check the health or your application
        /// </summary>
        [ConfigurationProperty("PhoneHomeFrequencySeconds", IsRequired = false)]
        public int? PhoneHomeFrequencySeconds
        {
            get
            {
                int frequency;
                if(_section["PhoneHomeFrequencySeconds"] != null && int.TryParse((string)_section["PhoneHomeFrequencySeconds"], out frequency))
                {
                    return frequency;
                }

                return _phoneHomeFrequencySeconds;
            }
            set { _phoneHomeFrequencySeconds = value; }
        }

        /// <summary>
        /// Any extra meta data you might want to tag this application / server / environment with
        /// </summary>
        public IList<DataTag> Tags { get; set; }

        public Action<string, Exception> OnError { get; set; }


        [ConfigurationProperty("ApiKey", IsRequired = false)]
        public Guid? ApiKey
        {
            get
            {
                if (_section["ApiKey"] != null)
                {
                    return (Guid)_section["ApiKey"];
                }

                return _apiKey;
            }
            set { _apiKey = value; }
        }

        [ConfigurationProperty("ApiHostUrl", IsRequired = false)]
        public string ApiHostUrl
        {
            get { return (string)_section["ApiHostUrl"] ?? _apiHostUrl; }
            set { _apiHostUrl = value; }
        }

        public IList<IServerDataTagProvider> ServerDataTagProviders { get; internal set; }

        public IList<IApplicationInstanceDataTagProvider> ApplicationInstanceDataTagProviders { get; internal set; }

        public IList<IHealthcheckProvider> HealthcheckProviders { get; internal set; }

        public PhoneHomeWebConfig()
        {
            this.Tags = new List<DataTag>();
            this.ServerDataTagProviders = new List<IServerDataTagProvider>();
            this.ApplicationInstanceDataTagProviders = new List<IApplicationInstanceDataTagProvider>();
            this.HealthcheckProviders = new List<IHealthcheckProvider>();
        }
    }
}
