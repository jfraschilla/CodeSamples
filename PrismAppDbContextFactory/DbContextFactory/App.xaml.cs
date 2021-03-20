using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Prism.Ioc;
using Prism.Microsoft.DependencyInjection;
using DbContextFactory.Views;
using System.Windows;

namespace DbContextFactory
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();

        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            PrismContainerExtension.Current.RegisterServices(s =>
            {
                //s.AddDbContext<BloggingContext>(o => o.UseSqlite(@"Data Source=blogDB.db"));
                s.AddDbContextFactory<BloggingContext>(o => o.UseSqlite(@"Data Source=blogDB.db"));
            });
        }

    }
}
