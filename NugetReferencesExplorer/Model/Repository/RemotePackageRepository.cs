﻿using NuGet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetReferencesExplorer.Model.Repository
{
    internal class RemotePackageRepository : IRemotePackageRepository
    {
        private readonly Lazy<IPackageRepository> _repo = new Lazy<IPackageRepository>(() => PackageRepositoryFactory.Default.CreateRepository(Properties.Settings.Default.officialRepositoryUrl));

        public IPackage GetPackage(string packageId)
        {            
            //Connect to the official package repository
            return _repo.Value.FindPackage(packageId);
        }
    }
}