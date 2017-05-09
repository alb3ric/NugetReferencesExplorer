using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetReferencesExplorer.Model.Repository
{
    public static class LocalPackageRepositoryFactory
    {
        public static ILocalPackageRepository Create()
        {
            return new LocalPackageRepository();
        }
    }
}
