using GalaSoft.MvvmLight;
using NugetReferencesExplorer.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetReferencesExplorer.ViewModel
{
    public class ConsolidateViewModel : ViewModelBase
    {
        public ConsolidateViewModel(Package package, IEnumerable<PackageProject> selectedProjects)
        {
            _package = package;
            _selectedProjects = selectedProjects;
        }

        private readonly Package _package;
        private readonly IEnumerable<PackageProject> _selectedProjects;

        public IEnumerable<string> Versions
        {
            get
            {
                return _selectedProjects.Select(p => p.Version).Union(new string[] { this._package.Metadata.Version }).Distinct().OrderBy(_ => _);
            }
        }
    }
}
