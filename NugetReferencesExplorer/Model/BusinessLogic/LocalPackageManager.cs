using NugetReferencesExplorer.Model.Domain;
using NugetReferencesExplorer.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetReferencesExplorer.Model.BusinessLogic
{
    public class LocalPackageManager
    {
        public LocalPackageManager()
        {

        }

        public IEnumerable<Package> LoadPackages(string path)
        {
            //Load the packages
            var res = LocalPackageRepositoryFactory.Create().LoadPackages(path).OrderBy(x => x.Id).ToList();
            //Init the repository
            IRemotePackageRepository remotePackageRepository = RemotePackageRepositoryFactory.Create();
            //Get the remote package infos
            res.AsParallel().ForAll(x => x.RemotePackage = remotePackageRepository.GetPackage(x.Id));
            //Return
            return res;
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
