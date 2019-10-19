using System;
using Unity;

namespace UnityFactoryInjection
{
    class Program
    {
        static void Main(string[] args)
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<Controller>();
            container.RegisterType<Foo>();

            var controller = container.Resolve<Controller>();

        }

        public class Controller
        {
            public Controller(Func<Foo> fooCreator)
            {
                var foo1 = fooCreator();
                foo1.a = 2;

                var foo2 = fooCreator();
                foo2.a = 3;
            }

        }

        public class Foo
        {
            public int a { get; set; }
            public Foo()
            {
                a = 1;
            }

        }
           
    }
}
