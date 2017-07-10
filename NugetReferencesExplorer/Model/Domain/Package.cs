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
        public Package(string packageId, Func<string, IPackage> getRemotePackageFunc)
        {
            _id = packageId;
            _getRemotePackageFunc = getRemotePackageFunc;
        }

        private readonly string _id;
        private readonly Func<string, IPackage> _getRemotePackageFunc;

        public string Id => _id;

        public List<PackageProject> Projects { get; } = new List<PackageProject>();

        public bool HasDifferentVersion
        {
            get
            {
                return this.Projects.Select(p => p.Version).Distinct().Count() > 1;
            }
        }

        private bool _isRemotelyLoaded = false;

        private IPackage _packageInfos;
        public IPackage PackageInfos
        {
            get
            {
                if (!_isRemotelyLoaded)
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
            this._packageInfos = _getRemotePackageFunc(this.Id);
            _isRemotelyLoaded = true;
        }

        public void ConsolidateSelected(SemanticVersion version)
        {
            foreach (var p in this.Projects.Where(p => p.IsChecked))
            {
                p.SetVersion(version);
            }
        }
    }
}
