using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NugetReferencesExplorer.Model.BusinessServices;
using NugetReferencesExplorer.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NugetReferencesExplorer.ViewModel
{
    public class ConsolidateViewModel : ViewModelBase
    {
        public ConsolidateViewModel(Package package, IEnumerable<PackageProject> selectedProjects)
        {
            _package = package;
            _selectedProjects = selectedProjects;
        }

        private readonly PackageService _packageService = new PackageService();

        #region Private methods

        private void consolidate(Window winToClose)
        {
            //update the packages
            _packageService.UpdatePackageProject(this._package, this._selectedProjects, this.SelectedVersion);
            //Close the window when finished
            winToClose.Close();
        }

        #endregion

        #region Properties

        private readonly Package _package;
        private readonly IEnumerable<PackageProject> _selectedProjects;

        public IEnumerable<string> Versions
        {
            get
            {
                return _selectedProjects.Select(p => p.Version).Union(new string[] { this._package.Metadata.Version }).Distinct().OrderBy(v => new Version(v));
            }
        }

        private string _selectedVersion;
        public string SelectedVersion
        {
            get => _selectedVersion;
            set
            {
                if (_selectedVersion != value)
                {
                    _selectedVersion = value;
                    this.RaisePropertyChanged(nameof(this.SelectedVersion));
                    this.ConsolidateCommand.RaiseCanExecuteChanged();
                }
            }
        }

        #endregion

        #region Commands

        private RelayCommand<Window> _consolidateCommand;
        public RelayCommand<Window> ConsolidateCommand
        {
            get
            {
                if (_consolidateCommand == null)
                    _consolidateCommand = new RelayCommand<Window>((o) => consolidate(o), (o) => !string.IsNullOrEmpty(this.SelectedVersion));
                return _consolidateCommand;
            }
        }


        private RelayCommand<Window> _closeCommand;
        public RelayCommand<Window> CloseCommand
        {
            get
            {
                if (_closeCommand == null)
                    _closeCommand = new RelayCommand<Window>((o) => o.Close());
                return _closeCommand;
            }
        }

        #endregion
    }
}
