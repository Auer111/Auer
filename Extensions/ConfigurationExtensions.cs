using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Linq;

namespace Auer.Extensions
{
    public static class ConfigurationExtensions
    {
        public static string GetAppSetting(this Configuration config, string key)
        {
            return config.AppSettings.Settings.AllKeys.Contains(key) ? config.AppSettings.Settings[key].Value : null; 
        }
        public static void SetAppSetting(this Configuration config, string key, string value)
        {
            if (config.AppSettings.Settings.AllKeys.Contains(key))
            {
                config.AppSettings.Settings[key].Value = value;
            }
            else { config.AppSettings.Settings.Add(key, value); }
        }

    }
}
