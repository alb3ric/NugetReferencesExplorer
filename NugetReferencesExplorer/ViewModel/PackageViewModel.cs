using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NugetReferencesExplorer.Model.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetReferencesExplorer.ViewModel
{
    public class PackageViewModel : ViewModelBase
    {
        public PackageViewModel(Package p)
        {
            _package = p;
            //On SelectedItems change force RaiseCanExecuteChanged
            this.SelectedItems.CollectionChanged += (sender, e) => this.ConsolidateCommand.RaiseCanExecuteChanged();
        }

        private readonly Package _package;

        #region Properties

        public string Id => _package.Id;

        public bool HasDifferentVersion => _package.HasDifferentVersion;

        public string Summary => _package.Metadata.Summary;

        public string Title => _package.Metadata.Title;

        public Uri IconUrl => _package.Metadata.IconUrl;

        public string Version => _package.Metadata.Version;

        public IEnumerable<string> Authors => _package.Metadata.Authors;

        private ObservableCollection<PackageProject> _projects;
        public ObservableCollection<PackageProject> Projects
        {
            get
            {
                if (_projects == null)
                    _projects = new ObservableCollection<PackageProject>(_package.Projects);
                return _projects;
            }
            set
            {
                if (_projects != value)
                {
                    _projects = value;
                    this.RaisePropertyChanged(nameof(this.Projects));
                }

            }
        }

        private ObservableCollection<object> _selectedItems = new ObservableCollection<object>();
        public ObservableCollection<object> SelectedItems
        {
            get => this._selectedItems;
            set
            {
               if (this._selectedItems != value)
                {
                    this._selectedItems = value;
                    this.RaisePropertyChanged(nameof(this.SelectedItems));
                }
            }
        }

        #endregion

        #region Private methods

        private void doConsolidate()
        {
            throw new NotImplementedException();
        }

        private bool canConsolidate()
        {
            return this.SelectedItems.Count > 1 
                    && this.SelectedItems.Cast<PackageProject>().Select(x => x.Version).Distinct().Count() > 1;
        }

        #endregion

        #region Commands

        private RelayCommand _consolidateCommand;
        public RelayCommand ConsolidateCommand
        {
            get
            {
                if (_consolidateCommand == null)
                    _consolidateCommand = new RelayCommand(this.doConsolidate, this.canConsolidate);
                return _consolidateCommand;
            }
        }

        #endregion
    }
}
