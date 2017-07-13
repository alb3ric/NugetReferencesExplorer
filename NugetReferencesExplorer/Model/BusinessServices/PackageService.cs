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
        }

        private readonly ILocalPackageRepository _localRepository; 

        public IEnumerable<Package> LoadPackages(string path, IEnumerable<string> sources)
        {
            //Load the packages
            var packages = _localRepository.LoadPackages(path).OrderBy(x => x.Id).ToList();
            //Get the remote package infos
            IRemotePackageRepository remoteRepository = RemotePackageRepositoryFactory.Create(sources);
            packages.AsParallel().ForAll(x => x.Metadata = remoteRepository.GetPackage(x.Id));
            //Return
            return packages;
        }

        //public void UpdatePackageProject(PackageProject packageProject, SemanticVersion version)
        //{
            //Initialize the package manager
            //PackageManager packageManager = new PackageManager(_repo.Value, packageProject.ProjectPath);
            //Update the package
            //packageManager.UpdatePackage(packageProject.PackageId, version, false, false);
        //}
    }
}
