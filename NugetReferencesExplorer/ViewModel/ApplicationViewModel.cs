using GalaSoft.MvvmLight;
using NugetReferencesExplorer.Model.Domain;
using NugetReferencesExplorer.Model.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetReferencesExplorer.ViewModel
{
    public class ApplicationViewModel : ViewModelBase
    {
        private bool _isBusy = false;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                if (_isBusy != value)
                {
                    _isBusy = value;
                    this.RaisePropertyChanged(nameof(IsBusy));
                }
            }
        }

        private async void LoadPackageItemsAsync()
        {
            IsBusy = true;
            this.PackageItems = await Task.Run(() => LocalPackageRepositoryFactory.Create().GetPackages(Properties.Settings.Default.sourcePath).Where(p => p.HasDifferentVersion).ToList());//.ForEach(x => x.LoadPackageInfos()));
            IsBusy = false;            
        }

        private IEnumerable<Package> _packageItems;
        public IEnumerable<Package> PackageItems
        {
            get
            {                
                if (_packageItems == null)
                {
                    this.LoadPackageItemsAsync();                    
                }   
                return _packageItems;              
            }
            set
            {
                _packageItems = value;
                this.RaisePropertyChanged(nameof(PackageItems));
            }
        }

        public Package SelectedPackage { get; set; }
    }
}
