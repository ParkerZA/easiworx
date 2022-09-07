using System;
using System.Configuration;
using Finx.App.Models;

namespace easiplan.app.Services
{
    public class ApplicationProperties
    {
        public String GetApplicationProperties(String key)
        {
            Console.WriteLine("getApplicationProperties");
            try  
            {  
                var appSettings = ConfigurationManager.AppSettings;
                
                if (appSettings.Count == 0)  
                {  
                    Console.WriteLine("AppSettings is empty.");  
                }  
                else  
                {  
                    return appSettings.Get(key);
                   
                }  
            }  
            catch (ConfigurationErrorsException)  
            {  
                Console.WriteLine("Error reading app settings");  
            }

            return null;
        }
    }
}