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
        public PackageProject(PackageReference packageReference)
        {
            _packageReference = packageReference;
        }

        private readonly PackageReference _packageReference;

        public string Id => _packageReference.Id;
        public SemanticVersion Version => _packageReference.Version;
        public string Path { get; set; }
    }
}
