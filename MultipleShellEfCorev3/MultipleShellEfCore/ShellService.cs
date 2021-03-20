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

namespace MultipleShellEfCore
{
    class ShellService : IShellService
    {
        private readonly IContainer _container;
        private readonly IRegionManager _regionManager;

        public ShellService(IContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }
        public void ShowShell(string uri)
        {
            var shell = _container.Resolve<MainWindow>();

            var scopedRegion = _regionManager.CreateRegionManager();
            RegionManager.SetRegionManager(shell, scopedRegion);

            RegionManagerAware.SetRegionManagerAware(shell, scopedRegion);

            scopedRegion.RequestNavigate(RegionNames.ContentRegion, uri);

            shell.Show();

        }
    }
}
