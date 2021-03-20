using Infrastructure;
using Infrastructure.Prism;
using MultipleShellEfCore.Infrastructure;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleA.ViewModels
{
    public class ViewAViewModel : BindableBase, IRegionManagerAware
    {
        private string _message;

        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public DelegateCommand NavigateCommand { get; private set; }
        public IRegionManager RegionManager { get; set; }

        public ViewAViewModel()
        {
            Message = "View A from your Prism Module";

            NavigateCommand = new DelegateCommand(Navigate);
        }

        private void Navigate()
        {
            RegionManager.RequestNavigate(RegionNames.ContentRegion, "ViewB");
        }
    }
}
