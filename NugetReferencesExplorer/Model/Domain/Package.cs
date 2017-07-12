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
        public Package(string packageId)
        {
            _id = packageId;
        }

        private readonly string _id;

        public string Id => _id;

        public List<PackageProject> Projects { get; } = new List<PackageProject>();

        public bool HasDifferentVersion
        {
            get
            {
                return this.Projects.Select(p => p.Version).Distinct().Count() > 1;
            }
        }

        public RemotePackage RemotePackage { get; set; }

        public IPackage PackageInfos
        {
            get
            {                           
                return RemotePackage?.Package;
            }
        }

    }
}
