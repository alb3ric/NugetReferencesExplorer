using NuGet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetReferencesExplorer.Model.Domain
{
    public class PackageProject
    {
        public string Id { get; set; }
        public SemanticVersion Version { get; set; }
        public string Path { get; set; }
    }
}
