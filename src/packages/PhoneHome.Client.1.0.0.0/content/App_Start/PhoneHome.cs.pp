using System;
using System.Configuration;
using PhoneHome.Client.Configuration;
using PhoneHome.Client;


namespace $rootnamespace$
{
    public static class PhoneHome
    {
        public static void Configure()
        {
		    PhoneHomeConfig config = new PhoneHomeConfig();
            config.ApplicationKey = "$rootnamespace$";

            config.OnError = (type, exception) =>
            {
				// Log any PhoneHome exceptions here
            };
			
            PhoneHomeClient.Register(config);
        }
    }
}
