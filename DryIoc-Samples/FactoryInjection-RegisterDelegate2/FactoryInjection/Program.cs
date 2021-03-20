using DryIoc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FactoryInjection
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container();
            //container.Register<IFoo, Foo1>(Reuse.Singleton, serviceKey: "Foo1");
            container.Register<IFoo, Foo1>(serviceKey: "Foo1");
            container.Register<IFoo, Foo2>(serviceKey: "Foo2");
            container.Register<FooFactory>();

            container.RegisterDelegate<Func<string, int, IFoo>>(r => (key, v) => container.Resolve<IFoo>(key));

            var fooFactory = container.Resolve<FooFactory>();
            var foo1 = fooFactory.Create("Foo1", 1);
            var foo2 = fooFactory.Create("Foo2", 2);
            var foo3 = fooFactory.Create("Foo1", 3);

            Console.WriteLine($"{foo1.GetName()}");
            Console.WriteLine($"{foo2.GetName()}");
            Console.WriteLine($"{foo3.GetName()}");
        }

    }

    public class FooFactory
    {
        private Func<string, int, IFoo> _fooFact;

        public FooFactory(Func<string, int, IFoo> fooFact)
        {

            _fooFact = fooFact;
        }


        public IFoo Create(string name, int value)
        {
            return _fooFact(name, value);
        }
    }

    public interface IFoo
    {
        string GetName();
        int Value { get; set; }
    }

    public class Foo1 : IFoo
    {
        public Foo1(int value)
        {
            Value = value;
        }
        public int Value { get; set; }
        public string GetName()
        {
            return "Foo1";
        }
    }

    public class Foo2 : IFoo
    {
        public Foo2(int value)
        {
            Value = value;
        }
        public int Value { get; set; }
        public string GetName()
        {
            return "Foo2";
        }
    }
}
