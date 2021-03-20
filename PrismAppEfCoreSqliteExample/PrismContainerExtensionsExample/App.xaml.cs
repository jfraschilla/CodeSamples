using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Prism.Ioc;
using Prism.Microsoft.DependencyInjection;
using PrismContainerExtensionsExample.Views;
using System.Windows;

namespace PrismContainerExtensionsExample
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
            _FileService.FileName = "blogDB3.db";

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
                s.AddDbContext<BloggingContext>(o => o.UseSqlite(@"Data Source=blogDB.db"));
            });
            containerRegistry.RegisterInstance<IFileService>(_FileService);
        }

    }
}
