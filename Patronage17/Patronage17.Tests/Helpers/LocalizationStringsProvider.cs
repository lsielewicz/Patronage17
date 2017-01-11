using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Patronage17.Tests.Helpers
{
    public class LocalizationStringsProvider
    {
        public static string ApplicationLocalization
        {
            get
            {
                return System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            }
        }

        public static string NotExistingPath
        {
            get
            {
                return System.IO.Path.Combine(ApplicationLocalization, "NotExistingDirectory", "NotExistingSubDirectory");
            }
        }

        public static string TooLongPathString
        {
            get
            {
                return ApplicationLocalization + @"\" + new string('z', 300); 
            }
        }

        public static string NotPathString
        {
            get
            {
                return "I am not a path";
            }
        }
    }
}
