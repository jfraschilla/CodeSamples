using NUnit.Framework;
using System;
using Unity;
using Abmes.Unity.TypedFactories;


namespace UnityFactoryExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var container = new UnityContainer();
            container.RegisterTypedFactory<IMyFactory>().ForConcreteType<Foo>();
            container.RegisterType<IFoo, Foo>();
            var factory = container.Resolve<IMyFactory>();
            var foo = factory.Create("some name");
            Assert.AreEqual("some name", foo.Name);
        }
    }

    public interface IMyFactory
    {
        IFoo Create(string name);
    }

    public interface IFoo
    {
        string Name { get; }
    }

    public class Foo : IFoo
    {
        public string Name { get; set; }
        public Foo(string name)
        {
            this.Name = name;
        }
    }
}
