using NuGet;
using NugetReferencesExplorer.Model.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetReferencesExplorer.Model.Repository
{
    internal class LocalPackageRepository : ILocalPackageRepository
    {

        public IList<Package> GetPackages(string path)
        {
            if (!Directory.Exists(path))
                throw new DirectoryNotFoundException();

            Dictionary<string, Package> res = new Dictionary<string, Package>();
            //Retrieve all the files in the specified path
            string[] files = Directory.GetFiles(path, Properties.Settings.Default.defaultPackageConfigFilename, SearchOption.AllDirectories);

            //Create the remote repository
            IRemotePackageRepository remoteRepository = RemotePackageRepositoryFactory.Create();
            remoteRepository.Preload();

            foreach (var fileName in files)
            {
                //For each file load the package reference file
                var file = new PackageReferenceFile(fileName);
                foreach (PackageReference packageReference in file.GetPackageReferences())
                {
                    //For each references in the package look if already created
                    //If not create one and add it to the dictionary
                    Package pack;
                    if (!res.TryGetValue(packageReference.Id, out pack))
                    {
                        pack = new Package(packageReference.Id, remoteRepository.GetPackage);
                        res.Add(pack.Id, pack);
                    }
                    //Create the package project
                    PackageProject project = new PackageProject(packageReference)
                    {
                        PackagePath = fileName
                    };
                    //Add it to the package
                    pack.Projects.Add(project);
                }
            }
            return res.Values.ToList();
        }
        
    }
}
