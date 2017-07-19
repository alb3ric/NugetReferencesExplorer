using NuGet;
using NugetReferencesExplorer.Model.Domain;
using System.Collections.Generic;

namespace NugetReferencesExplorer.Model.Repository
{
    public interface IRemotePackageRepository
    {
        PackageMetdata GetPackageMetada(string packageId, IEnumerable<string> sources);

        void UpdatePackage(string packageId, string source, string path, string version);
    }
}