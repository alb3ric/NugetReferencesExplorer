using NuGet;
using NugetReferencesExplorer.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetReferencesExplorer.Model.Repository
{
    internal class RemotePackageRepository : IRemotePackageRepository
    {
        //TODO : Should load from different URL, use nuget.config to get remote repostory urls

        private readonly Lazy<IPackageRepository> _repo = new Lazy<IPackageRepository>(() => PackageRepositoryFactory.Default.CreateRepository(Properties.Settings.Default.officialRepositoryUrl));

        public RemotePackage GetPackage(string packageId)
        {
            //Connect to the official package repository
            IVersionSpec v = new VersionSpec();
            IPackage p = _repo.Value.FindPackage(packageId, v, false, false);
            return new RemotePackage(p, _repo.Value);
        }
        
    }
}
