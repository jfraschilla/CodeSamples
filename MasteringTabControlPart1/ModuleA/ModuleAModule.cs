using Microsoft.Practices.Unity;
using ModuleA.Views;
using Prism.Modularity;
using Prism.Regions;
using System;

namespace ModuleA
{
    public class ModuleAModule : IModule
    {
        private IUnityContainer _container;

        public ModuleAModule(IUnityContainer container)
        {
            _container = container;
            //_regionManager = regionManager;
        }

        public void Initialize()
        {
            //_regionManager.RegisterViewWithRegion("TabRegion", typeof(ViewA));
            _container.RegisterType<object, ViewA>("ViewA");
            _container.RegisterType<object, ViewB>("ViewB");
        }
    }
}