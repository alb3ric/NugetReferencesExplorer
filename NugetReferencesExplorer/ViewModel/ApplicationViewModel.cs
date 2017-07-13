using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NugetReferencesExplorer.Helpers;
using NugetReferencesExplorer.Model.BusinessServices;
using NugetReferencesExplorer.Model.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NugetReferencesExplorer.ViewModel
{
    public class ApplicationViewModel : ViewModelBase
    {

        private readonly IWindowService _windowService = new WindowService();
        private readonly PackageService _packageService = new PackageService();


        #region Private methods

        #endregion

        #region Properties

        private bool _displayWithDifferentVersionOnly = true;
        public bool DisplayWithDifferentVersionOnly
        {
            get => _displayWithDifferentVersionOnly;
            set
            {
                _displayWithDifferentVersionOnly = value;
                this.RaisePropertyChanged(nameof(DisplayWithDifferentVersionOnly));
                this.RaisePropertyChanged(nameof(PackageItems));
            }
        }

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
       
        private ObservableCollection<PackageViewModel> _packageItems;
        public ObservableCollection<PackageViewModel> PackageItems
        {
            get
            {
                if (_packageItems == null)
                    return null;

                if (this.DisplayWithDifferentVersionOnly)
                    return new ObservableCollection<PackageViewModel>(_packageItems.Where(x => x.HasDifferentVersion));
                else
                    return _packageItems;
            }
            set
            {
                _packageItems = value;
                this.RaisePropertyChanged(nameof(PackageItems));
            }
        }

        private PackageViewModel _selectedPackage;
        public PackageViewModel SelectedPackage
        {
            get => _selectedPackage;            
            set
            {
                if (_selectedPackage != value)
                {
                    _selectedPackage = value;
                    this.RaisePropertyChanged(nameof(SelectedPackage));
                    this.RaisePropertyChanged(nameof(HasSelectedPackage));
                }
            }
        }

        public bool HasSelectedPackage => this.SelectedPackage != null;

        public string PathToScan
        {
            get => ConfigurationHelper.GetPathToScan();
            set
            {
                if (ConfigurationHelper.SetPathToScan(value))
                    this.RaisePropertyChanged(nameof(this.PathToScan));
            }
        }

        private ObservableCollection<string> _sources;
        public ObservableCollection<string> Sources
        {
            get
            {
                if (_sources == null)
                {
                    _sources = new ObservableCollection<string>(ConfigurationHelper.GetNugetRepositorySources());
                }
                return _sources;
            }
            set
            {
                if (_sources != value)
                {
                    _sources = value;
                    this.RaisePropertyChanged(nameof(Sources));
                }
            }
        }

        #endregion

        #region Private Methods

        private async void LoadPackageItemsAsync()
        {
            if (IsBusy)
                return;
            this.SelectedPackage = null;
            //ViewModel is busy...
            IsBusy = true;
            try
            {
                //Load the package Items asynchronically
                this.PackageItems = await Task.Run(
                                    () => new ObservableCollection<PackageViewModel>(_packageService
                                                                                .LoadPackages(this.PathToScan, this.Sources)
                                                                                .Select(x => new PackageViewModel(x)))
                                                                                );
            }
            catch (Exception ex)
            {
                //Handle loading error
                //TODO : fire message dialog
            }
            finally
            {
                //ViewModel is not busy anymore!
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
                    _loadCommand = new RelayCommand(LoadPackageItemsAsync, () => !IsBusy);
                return _loadCommand;
            }
        }

        private RelayCommand _openSourcePathCommand;
        public RelayCommand OpenSourcePathCommand
        {
            get
            {
                if (_openSourcePathCommand == null)
                    _openSourcePathCommand = new RelayCommand(() => Process.Start(this.PathToScan));
                return _openSourcePathCommand;
            }
        }

        

        #endregion
    }
}
