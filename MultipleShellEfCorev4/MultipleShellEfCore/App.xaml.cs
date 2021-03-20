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
using DryIoc;
using Prism.DryIoc;
using MultipleShellEfCore.ViewModels;

namespace MultipleShellEfCore
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        //private FileService _FileService;
        private string _filename;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            if (e.Args.Length == 0)
            {
                _filename = "blog1.db";
            }
            else
            {
                _filename = e.Args[0];
            }
        }
        protected override Window CreateShell()
        {
            var shell = Container.Resolve<MainWindow>();
            var viewModel = shell.DataContext as MainWindowViewModel;
            _filename = "blog1.db";
            //viewModel.SetFile(_filename);
            return shell;

        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            PrismContainerExtension.Current.RegisterServices(s =>
            {
                //s.AddHttpClient();
                s.AddDbContext<BloggingContext>(o => o.UseSqlite(@"Data Source=blogDB.db"), ServiceLifetime.Transient);
            });
            //containerRegistry.RegisterInstance<IFileService>(_FileService);
            containerRegistry.RegisterSingleton<IFileService, FileService>();
            containerRegistry.RegisterSingleton<IShellService, ShellService>();
            containerRegistry.RegisterSingleton<IDatabaseManager, DatabaseManager>();
            //containerRegistry.RegisterScoped<IFileService, FileService>();

            //var c = containerRegistry.GetContainer();
            //c.Register<IFileService, FileService>(Reuse.InResolutionScopeOf<MainWindow>());
            //c.RegisterMany<BloggingContext>(Reuse.Singleton, setup: Setup.With(openResolutionScope: true));
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule(typeof(ModuleAModule));
        }

        protected override void InitializeShell(Window shell)
        {
            if (shell != null)
            {
                DatabaseManager.SetDatabaseManager(shell, Container.Resolve<DatabaseManager>());
            }

            var regionManager = RegionManager.GetRegionManager(shell);
            RegionManagerAware.SetRegionManagerAware(shell, regionManager);

            var databaseManager = DatabaseManager.GetDatabaseManager(shell);
            databaseManager.DatabaseName = _filename;
            DatabaseManagerAware.SetDatabaseManagerAware(shell, databaseManager);
        }

        protected override void ConfigureDefaultRegionBehaviors(IRegionBehaviorFactory regionBehaviors)
        {
            base.ConfigureDefaultRegionBehaviors(regionBehaviors);
            regionBehaviors.AddIfMissing(RegionManagerAwareBehavior.BehaviorKey, typeof(RegionManagerAwareBehavior));
            regionBehaviors.AddIfMissing(DatabaseManagerAwareBehavior.BehaviorKey, typeof(DatabaseManagerAwareBehavior));
        }
    }
}
