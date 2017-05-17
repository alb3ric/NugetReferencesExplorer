﻿using GalaSoft.MvvmLight;
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
            //ViewModel is busy...
            IsBusy = true;
            try
            {
                //Load the package Items asynchronically
                this.PackageItems = await Task.Run(() =>
                {
                    //Get the package
                    var res = LocalPackageRepositoryFactory.Create().GetPackages(Properties.Settings.Default.sourcePath).Where(p => p.HasDifferentVersion).ToList();
                    //Load the package info from the feed
                    res.ForEach(x => x.LoadRemotePackageInfos());
                    return res;
                });
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
                    _loadCommand = new RelayCommand(LoadPackageItemsAsync);
                return _loadCommand;
            }
        }

        #endregion
    }
}
