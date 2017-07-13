using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetReferencesExplorer.Helpers
{
    public static class ConfigurationHelper
    {
        public static string GetPathToScan()
            => Properties.Settings.Default.sourcePath;

        public static bool SetPathToScan(string newPath)
        {
            if (Properties.Settings.Default.sourcePath != newPath)
            {
                Properties.Settings.Default.sourcePath = newPath;
                Properties.Settings.Default.Save();
                return true;
            }
            return false;
        }

        public static IEnumerable<string> GetNugetRepositorySources()
        {
            return Properties.Settings.Default.nugetRepositorySources.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
        }


    }
}
