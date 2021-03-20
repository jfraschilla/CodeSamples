using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;
using ModuleA;
using PrismScopedRegions.Infrastructure;
using System;
using System.Globalization;
using System.Reflection;
using Microsoft.Practices.Prism.Regions;
using PrismScopedRegions.Infrastructure.Prism;

namespace PrismScopedRegions
{
    public class Bootstrapper : UnityBootstrapper
    {
        public Bootstrapper()
        {
            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver((viewType) =>
            {
                var viewName = viewType.FullName;
                var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
                var viewModelName = String.Format(CultureInfo.InvariantCulture, "{0}ViewModel, {1}", viewName, viewAssemblyName);
                return Type.GetType(viewModelName);
            });
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            ViewModelLocationProvider.SetDefaultViewModelFactory((type) =>
            {
                return Container.Resolve(type);
            });

            Container.RegisterType<IShellService, ShellService>(new ContainerControlledLifetimeManager());
        }

        protected override System.Windows.DependencyObject CreateShell()
        {
            return Container.Resolve<Shell>();
        }

        protected override void InitializeShell()
        {
            var regionManager = RegionManager.GetRegionManager((Shell));
            RegionManagerAware.SetRegionManagerAware(Shell, regionManager);

            App.Current.MainWindow.Show();
        }

        protected override void ConfigureModuleCatalog()
        {
            ModuleCatalog catalog = (ModuleCatalog)ModuleCatalog;
            catalog.AddModule(typeof(ModuleAModule));
        }

        protected override IRegionBehaviorFactory ConfigureDefaultRegionBehaviors()
        {
            IRegionBehaviorFactory behaviors = base.ConfigureDefaultRegionBehaviors();
            behaviors.AddIfMissing(RegionManagerAwareBehavior.BehaviorKey, typeof(RegionManagerAwareBehavior));
            return behaviors;
        }
    }
}
