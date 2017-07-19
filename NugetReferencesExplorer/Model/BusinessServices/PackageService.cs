using NuGet;
using NugetReferencesExplorer.Model.Domain;
using NugetReferencesExplorer.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetReferencesExplorer.Model.BusinessServices
{
    public class PackageService
    {
        public PackageService()
        {
            _localRepository = LocalPackageRepositoryFactory.Create();
            _remoteRepository = RemotePackageRepositoryFactory.Create();
        }

        private readonly ILocalPackageRepository _localRepository;
        private readonly IRemotePackageRepository _remoteRepository;

        public IEnumerable<Package> LoadPackages(string path, IEnumerable<string> sources)
        {
            //Load the packages
            var packages = _localRepository.LoadPackages(path).OrderBy(x => x.Id).ToList();
            //Get the remote package infos
            packages.AsParallel().ForAll(x => x.Metadata = _remoteRepository.GetPackageMetada(x.Id, sources));
            //Return
            return packages;
        }

        public void UpdatePackageProject(Package package, IEnumerable<PackageProject> packageProjects, string version)
        {
            foreach (var p in packageProjects)
            {
                if (p.Version != version)
                    _remoteRepository.UpdatePackage(package.Id, package.Metadata.Source, p.ProjectPath, version);
            }            
        }
    }
}
