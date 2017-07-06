using NuGet;

namespace NugetReferencesExplorer.Model.Repository
{
    public interface IRemotePackageRepository
    {
        IPackage GetPackage(string packageId);
        void Preload();
    }
}