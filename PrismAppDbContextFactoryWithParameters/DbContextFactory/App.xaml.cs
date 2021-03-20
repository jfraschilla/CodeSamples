using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Prism.Ioc;
using Prism.Microsoft.DependencyInjection;
using DbContextFactory.Views;
using System.Windows;
using DryIoc;
using System.Linq;
using System;
using Prism.DryIoc;

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
            //PrismContainerExtension.Current.RegisterServices(s =>
            //{
            //    //s.AddDbContext<BloggingContext>(o => o.UseSqlite(@"Data Source=blogDB.db"));
            //    s.AddDbContextFactory<BloggingContext>(o => o.UseSqlite(@"Data Source=blogDB.db"));
            //});

            var container = containerRegistry.GetContainer();

            //container.Register(typeof(ISomeService<>), typeof(SomeService<>), Reuse.Transient,
            //    made: Parameters.Of.Details((req, p) =>
            //        p.ParameterType.GetGenericDefinitionOrNull() == typeof(IStrategy<>) &&
            //        p.ParameterType.GetGenericParamsAndArgs().Any(x => x.IsAssignableTo<IFoo>())
            //        ? null                              // the default injection behavior 
            //        : ServiceDetails.Of(value: null))   // otherwise return the `null` value
            //    );

            //container.Register(typeof(BloggingContext), Reuse.Transient,
            //    made: Parameters.Of.Details((req, p) =>
            //        p.ParameterType.GetGenericDefinitionOrNull() == typeof(string)
            //        ? null                              // the default injection behavior 
            //        : ServiceDetails.Of(value: null))   // otherwise return the `null` value
            //    );

            container.RegisterDelegate<Func<string, BloggingContext>>(r => (key) => container.Resolve<BloggingContext>(key));
        }

    }
}
