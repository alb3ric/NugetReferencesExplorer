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
            Dictionary<string, Package> res = new Dictionary<string, Package>();
            //Retrieve all the files in the specified path
            string[] files = Directory.GetFiles(path, Properties.Settings.Default.defaultPackageConfigFilename, SearchOption.AllDirectories);

            //Create the remote repository
            IRemotePackageRepository remoteRepository = RemotePackageRepositoryFactory.Create();
            
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
                        pack = new Package(packageReference, remoteRepository);
                        res.Add(pack.Id, pack);
                    }
                    //Create the package project
                    PackageProject project = new PackageProject()
                    {
                        Id = packageReference.Id,
                        Version = packageReference.Version,
                        Path = fileName
                    };
                    //Add it to the package
                    pack.Projects.Add(project);
                }
            }
            return res.Values.ToList();
        }
        
    }
}
