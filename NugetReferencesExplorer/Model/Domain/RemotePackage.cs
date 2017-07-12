using NuGet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetReferencesExplorer.Model.Domain
{
    public class RemotePackage
    {
        public RemotePackage(IPackage package, IPackageRepository repository)
        {
            this.Package = package;
            this.Repository = repository;
        }

        public IPackage Package { get; private set; }
        public IPackageRepository Repository { get; private set; }
    }
}
