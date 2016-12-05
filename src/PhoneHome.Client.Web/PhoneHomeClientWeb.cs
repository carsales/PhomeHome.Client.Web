using System;
using System.Configuration;
using System.Reflection;
using System.Timers;
using PhoneHome.Client.Web.Providers;
using PhoneHome.Client.Interfaces;

namespace PhoneHome.Client.Web
{
    public class PhoneHomeClientWeb
    {
        public static void Register(IPhoneHomeConfig config)
        {
            config.ApplicationInstanceDataTagProviders.Add(new IISDataTagProvider());

            PhoneHomeClient.Register(config);
        }
    }
}

