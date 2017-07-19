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
        protected readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public RemotePackageRepository()
        {

        }
        
        private readonly ConcurrentDictionary<string, NuGet.IPackageRepository> _cacheRepo = new ConcurrentDictionary<string, NuGet.IPackageRepository>(); 
        private NuGet.IPackageRepository getNugetRepository(string source)
        {
            return _cacheRepo.GetOrAdd(source, (s) => NuGet.PackageRepositoryFactory.Default.CreateRepository(source));            
        }

        public PackageMetdata GetPackageMetada(string packageId, IEnumerable<string> sources)
        {
            foreach (var s in sources)
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

        public void UpdatePackage(string packageId, string source, string path, string version)
        {
            try
            {
                //Get the repository
                var repo = getNugetRepository(source);
                //Initialize the package manager
                PackageManager packageManager = new PackageManager(repo, path);
                //Update the package
                //packageManager.InstallPackage(packageId, new SemanticVersion(version));
                packageManager.UpdatePackage(packageId, new SemanticVersion(version), false, false);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw ex;
            }
        }

    }
}
