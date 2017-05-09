using System.Collections.Generic;
using NugetReferencesExplorer.Model.Domain;

namespace NugetReferencesExplorer.Model.Repository
{
    public interface ILocalPackageRepository
    {
        IList<Package> GetPackages(string path);
    }
}