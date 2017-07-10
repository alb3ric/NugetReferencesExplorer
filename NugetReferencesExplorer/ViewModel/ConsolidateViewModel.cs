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
        public ConsolidateViewModel(Package package)
        {
            _package = package;
        }

        private readonly Package _package;
    }
}
