using NuGet;
using NugetReferencesExplorer.Model.Domain;

namespace NugetReferencesExplorer.Model.Repository
{
    public interface IRemotePackageRepository
    {
        RemotePackage GetPackage(string packageId);
    }
}