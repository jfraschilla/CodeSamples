using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Regions;
using PrismScopedRegions.Infrastructure;
using PrismScopedRegions.Infrastructure.Prism;

namespace ModuleA
{
    public class ViewAViewModel : BindableBase, IRegionManagerAware
    {
        public DelegateCommand NavigateCommand { get; set; }

        public ViewAViewModel()
        {
            NavigateCommand = new DelegateCommand(Navigate);
        }

        void Navigate()
        {
            RegionManager.RequestNavigate(KnownRegionNames.ContentRegion, "ViewB");
        }

        public IRegionManager RegionManager { get; set; }
    }
}
