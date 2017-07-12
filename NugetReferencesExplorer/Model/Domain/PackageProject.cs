using NuGet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetReferencesExplorer.Model.Domain
{
    public class PackageProject
    {
        public PackageProject(PackageReference packageReference)
        {
            _packageReference = packageReference;
        }

        private readonly PackageReference _packageReference;

        public string PackageId => _packageReference.Id;
        public SemanticVersion Version => _packageReference.Version;

        public string ProjectPath { get; set; }

        public bool IsChecked { get; set; }

        public string DisplayablePath
            => Path.GetDirectoryName(this.ProjectPath).Replace(Properties.Settings.Default.sourcePath, "");

    }
}
