using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Regions;
using TabControlRegion.Core;

namespace ModuleA.ViewModels
{
    public class ViewBViewModel : ViewModelBase, IConfirmNavigationRequest
    {
        public ViewBViewModel()
        {
            Title = "View B";
        }

        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            continuationCallback(false);
        }

        public override bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

    }
}
