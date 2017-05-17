using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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
        #region Properties


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
       
        private IEnumerable<Package> _packageItems;
        public IEnumerable<Package> PackageItems
        {
            get => _packageItems;
            set
            {
                _packageItems = value;
                this.RaisePropertyChanged(nameof(PackageItems));
            }
        }

        private Package _selectedPackage;
        public Package SelectedPackage
        {
            get => _selectedPackage;            
            set
            {
                if (_selectedPackage != value)
                {
                    _selectedPackage = value;
                    this.RaisePropertyChanged(nameof(SelectedPackage));
                }
            }
        }

        #endregion

        #region Private Methods

        private async void LoadPackageItemsAsync()
        {
            IsBusy = true;
            try
            {
                this.PackageItems = await Task.Run(() =>
                {
                    var res = LocalPackageRepositoryFactory.Create().GetPackages(Properties.Settings.Default.sourcePath).Where(p => p.HasDifferentVersion).ToList();
                    res.ForEach(x => x.LoadPackageInfos());
                    return res;
                });
            }
            catch (Exception ex)
            {
                //Handle loading error
                //TODO : fire message dialog
            }
            finally
            {
                IsBusy = false;
            }
        }


        #endregion

        #region Commands

        private RelayCommand _loadCommand;
        public RelayCommand LoadCommand
        {
            get
            {
                if (_loadCommand == null)
                    _loadCommand = new RelayCommand(LoadPackageItemsAsync);
                return _loadCommand;
            }
        }

        #endregion
    }
}
