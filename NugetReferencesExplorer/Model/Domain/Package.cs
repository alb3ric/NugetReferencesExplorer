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
        public Package(PackageReference packageReference, IRemotePackageRepository remoteRepository)
        {
            _packageRef = packageReference;
            _packageRepository = remoteRepository;
        }

        private readonly PackageReference _packageRef;
        private readonly IRemotePackageRepository _packageRepository;

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
                    _packageInfos = _packageRepository.GetPackage(this.Id);
                }                
                return _packageInfos;
            }
        }
    }
}
