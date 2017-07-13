using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetReferencesExplorer.Model.Repository
{
    public static class RemotePackageRepositoryFactory
    {
        public static IRemotePackageRepository Create(IEnumerable<string> sources)
        {
            //Always the same for the moment
            return new RemotePackageRepository(sources);
        }
    }
}
