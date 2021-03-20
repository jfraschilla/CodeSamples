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

            var fooFactory = container.Resolve<FooFactory>();
            var foo1 = fooFactory.Create("Foo1", 1);
            var foo2 = fooFactory.Create("Foo2", 2);
            var foo3 = fooFactory.Create("Foo1", 3);

            Console.WriteLine($"{foo1.GetName()}\n");
            Console.WriteLine($"{foo2.GetName()}\n");
            Console.WriteLine($"{foo3.GetName()}\n");
        }

    }


    public class FooFactory
    {
        private KeyValuePair<string, IFoo>[] _implementations;

        public FooFactory(KeyValuePair<string, IFoo>[] implementations)
        {
            _implementations = implementations;
        }

        public IFoo Create(string name, int value)
        {
            var implementationFactory = _implementations.First(x => x.Key == name).Value;
            //var implementation = implementationFactory();

            //return implementation;
            return implementationFactory;
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
