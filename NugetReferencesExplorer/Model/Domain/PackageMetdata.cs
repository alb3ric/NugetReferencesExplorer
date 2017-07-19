using NuGet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetReferencesExplorer.Model.Domain
{
    public class PackageMetdata
    {
        public PackageMetdata()
        {
        }

        public string Title { get; private set; }

        public string Summary { get; private set; }

        public Uri IconUrl { get; private set; }

        public string Version { get; private set; }

        public IEnumerable<string> Authors { get; private set; }

        public string Source { get; private set; }

        public static PackageMetdata Create(IPackage package, string source)
        {
            PackageMetdata pm = new PackageMetdata();
            pm.IconUrl = package.IconUrl;
            pm.Summary = package.Summary;
            pm.Authors = package.Authors;
            pm.Title = package.Title;
            pm.Version = package.Version.ToFullString();
            pm.Source = source;
            return pm;
        }
    }
}
