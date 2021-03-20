using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Prism.Regions;
using System.Threading.Tasks;
using DryIoc;
using MultipleShellEfCore.Views;
using Infrastructure.Prism;
using MultipleShellEfCore.Infrastructure;
using MultipleShellEfCore.ViewModels;

namespace MultipleShellEfCore
{
    class ShellService : IShellService
    {
        private readonly IContainer _container;
        private readonly IRegionManager _regionManager;
        private readonly IDatabaseManager _databaseManager;

        public ShellService(IContainer container, IRegionManager regionManager, IDatabaseManager databaseManager)
        {
            _container = container;
            _regionManager = regionManager;
            _databaseManager = databaseManager;
        }
        public void ShowShell(string uri)
        {
            var shell = _container.Resolve<MainWindow>();

            var scopedRegion = _regionManager.CreateRegionManager();
            RegionManager.SetRegionManager(shell, scopedRegion);

            RegionManagerAware.SetRegionManagerAware(shell, scopedRegion);

            var databaseManager = _databaseManager.CreateDatabaseManager();
            DatabaseManager.SetDatabaseManager(shell, databaseManager);
            DatabaseManagerAware.SetDatabaseManagerAware(shell, databaseManager);

            scopedRegion.RequestNavigate(RegionNames.ContentRegion, uri);

            shell.Show();

        }

        public void ShowShell(string uri, string filename)
        {
            var shell = _container.Resolve<MainWindow>();

            var scopedRegion = _regionManager.CreateRegionManager();
            RegionManager.SetRegionManager(shell, scopedRegion);

            RegionManagerAware.SetRegionManagerAware(shell, scopedRegion);

            var databaseManager = _databaseManager.CreateDatabaseManager();
            databaseManager.DatabaseName = filename;
            DatabaseManager.SetDatabaseManager(shell, databaseManager);
            DatabaseManagerAware.SetDatabaseManagerAware(shell, databaseManager);

            scopedRegion.RequestNavigate(RegionNames.ContentRegion, uri);

            shell.Show();

        }

    }
}
