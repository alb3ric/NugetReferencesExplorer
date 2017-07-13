using NuGet;
using NugetReferencesExplorer.Model.Domain;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetReferencesExplorer.Model.Repository
{
    internal class RemotePackageRepository : IRemotePackageRepository
    {
        public RemotePackageRepository(IEnumerable<string> sources)
        {
            _sources = sources;
        }

        private readonly IEnumerable<string> _sources;

        private readonly ConcurrentDictionary<string, NuGet.IPackageRepository> _cacheRepo = new ConcurrentDictionary<string, NuGet.IPackageRepository>(); 
        private NuGet.IPackageRepository getNugetRepository(string source)
        {
            return _cacheRepo.GetOrAdd(source, (s) => NuGet.PackageRepositoryFactory.Default.CreateRepository(source));            
        }

        public PackageMetdata GetPackage(string packageId)
        {
            foreach (var s in _sources)
            {
                var repo = getNugetRepository(s);
                if (repo != null)
                {
                    //Connect to the official package repository
                    IVersionSpec v = new VersionSpec();
                    IPackage p = repo.FindPackage(packageId, v, false, false);
                    if (p != null)
                        return PackageMetdata.Create(p, repo.Source);
                }
            }
            return null;
        }
        
    }
}
