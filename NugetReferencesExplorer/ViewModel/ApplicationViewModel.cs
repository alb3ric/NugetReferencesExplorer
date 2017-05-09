using NugetReferencesExplorer.Model.Domain;
using NugetReferencesExplorer.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetReferencesExplorer.ViewModel
{
    public class ApplicationViewModel
    {

        private IEnumerable<Package> _packageItems;
        public IEnumerable<Package> PackageItems
        {
            get
            {
                if (_packageItems == null)
                {                    
                    _packageItems = LocalPackageRepositoryFactory.Create().GetPackages(Properties.Settings.Default.sourcePath).Where(p => p.HasDifferentVersion);
                }
                return _packageItems;
            }
        }

        public Package SelectedPackage { get; set; }
    }
}
