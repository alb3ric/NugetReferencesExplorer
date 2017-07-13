using NuGet;
using NugetReferencesExplorer.Model.Domain;

namespace NugetReferencesExplorer.Model.Repository
{
    public interface IRemotePackageRepository
    {
        PackageMetdata GetPackage(string packageId);
    }
}