using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ModuleA;
using Prism.Ioc;
using Prism.Microsoft.DependencyInjection;
using Prism.Modularity;
using MultipleShellEfCore.Views;
using System.Windows;
using Prism.Regions;
using Infrastructure.Prism;
using MultipleShellEfCore.Infrastructure;
using MultipleShellEfCore.Data;

namespace MultipleShellEfCore
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private FileService _FileService;

        protected override void OnStartup(StartupEventArgs e)
        {
            _FileService = new FileService();
            _FileService.FileName = "blog1.db";

            base.OnStartup(e);
        }
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();

        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            PrismContainerExtension.Current.RegisterServices(s =>
            {
                //s.AddHttpClient();
                //s.AddDbContext<BloggingContext>(o => o.UseSqlite(@"Data Source=blogDB.db"), ServiceLifetime.Transient);
                s.AddDbContextFactory<BloggingContext>(o => o.UseSqlite(@"Data Source=blogDB.db"), ServiceLifetime.Transient);
            });
            containerRegistry.RegisterInstance<IFileService>(_FileService);
            containerRegistry.RegisterSingleton<IShellService, ShellService>();
            containerRegistry.RegisterScoped<MainWindow>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule(typeof(ModuleAModule));
        }

        protected override void InitializeShell(Window shell)
        {
            var regionManager = RegionManager.GetRegionManager(shell);
            RegionManagerAware.SetRegionManagerAware(shell, regionManager);
        }

        protected override void ConfigureDefaultRegionBehaviors(IRegionBehaviorFactory regionBehaviors)
        {
            base.ConfigureDefaultRegionBehaviors(regionBehaviors);
            regionBehaviors.AddIfMissing(RegionManagerAwareBehavior.BehaviorKey, typeof(RegionManagerAwareBehavior));
        }
    }
}
