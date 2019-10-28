using System;
using Unity;
using Unity.Injection;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new UnityContainer();
            container.EnableDebugDiagnostic();

            container.RegisterType<IFoo, Foo1>("Foo1");
            container.RegisterType<IFoo, Foo2>("Foo2");

            container.RegisterType<MainViewModel>();

            // This works with new RegisterFactory
            container.RegisterFactory<Func<string, IFoo>>(c => new Func<string, IFoo>(name => c.Resolve<IFoo>(name)));

            // This works with old deprecated InjectionFfactory
            //container.RegisterType<Func<string, IFoo>>(new InjectionFactory(
            //    ctx => new Func<string, IFoo>(name => container.Resolve<IFoo>(name))));


            var vm = container.Resolve<MainViewModel>();
            var foo1 = vm.CreateFoo("Foo1");
            var foo2 = vm.CreateFoo("Foo2");
            var foo3 = vm.CreateFoo("Foo1");
        }
    }

    public class MainViewModel
    {
        private Func<string, IFoo> _fooFactory;
        public MainViewModel(Func<string, IFoo> fooFactory)
        {
            _fooFactory = fooFactory;
        }

        public IFoo CreateFoo(string name)
        {
            return _fooFactory.Invoke(name);
        }

    }

    public interface IFoo
    {
        string Name { get; set; }
    }

    public class Foo1 : IFoo
    {
        public string Name { get; set; }

        public Foo1()
        {
            Name = "Foo1";
        }
    }

    public class Foo2 : IFoo
    {
        public string Name { get; set; }

        public Foo2()
        {
            Name = "Foo2";
        }
    }

}
