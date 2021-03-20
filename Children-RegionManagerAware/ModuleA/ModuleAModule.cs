using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using PrismScopedRegions.Infrastructure;

namespace ModuleA
{
    public class ModuleAModule : IModule
    {
        IRegionManager _regionManager;
        IUnityContainer _container;

        public ModuleAModule(IRegionManager regionManager, IUnityContainer container)
        {
            _regionManager = regionManager;
            _container = container;
        }

        public void Initialize()
        {
            _container.RegisterType(typeof(object), typeof(ViewA), "ViewA");
            _container.RegisterType(typeof(object), typeof(ViewB), "ViewB");
        }
    }
}
