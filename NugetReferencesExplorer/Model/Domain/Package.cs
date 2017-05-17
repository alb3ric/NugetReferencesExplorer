using NuGet;
using NugetReferencesExplorer.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetReferencesExplorer.Model.Domain
{
    public class Package
    {
        public Package(PackageReference packageReference, Func<string, IPackage> getRemotePackageFunc)
        {
            _packageRef = packageReference;
            _getRemotePackageFunc = getRemotePackageFunc;
        }

        private readonly PackageReference _packageRef;
        private readonly Func<string, IPackage> _getRemotePackageFunc;

        public string Id => _packageRef.Id;

        public SemanticVersion LastVersion => _packageRef.Version;

        public List<PackageProject> Projects { get; } = new List<PackageProject>();

        public bool HasDifferentVersion
        {
            get
            {
                return this.Projects.Select(p => p.Version).Distinct().Count() > 1;
            }
        }

        private IPackage _packageInfos;
        public IPackage PackageInfos
        {
            get
            {
                if (_packageInfos == null)
                {
                    this.LoadRemotePackageInfos();
                }                
                return _packageInfos;
            }
            private set
            {
                _packageInfos = value;
            }
        }

        public void LoadRemotePackageInfos()
        {
            this.PackageInfos = _getRemotePackageFunc(this.Id);
        }
    }
}
