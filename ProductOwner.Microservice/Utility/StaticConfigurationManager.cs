﻿using System.Reflection.Metadata;
using System.Runtime.InteropServices;

namespace ProductOwner.Microservice.Utility
{
    static class StaticConfigurationManager 
    {
        public static IConfiguration AppSetting{ get; }

        static StaticConfigurationManager()
        {
            AppSetting = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
        }
    }
}
