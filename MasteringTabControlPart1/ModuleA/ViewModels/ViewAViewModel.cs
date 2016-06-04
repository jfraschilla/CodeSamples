using Prism;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabControlRegion.Core;

namespace ModuleA.ViewModels
{
    public class ViewAViewModel : ViewModelBase, INavigationAware, IActiveAware
    {
        public ViewAViewModel()
        {
            Title = "View A";
        }

        private bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                SetProperty(ref _isActive, value);
                Title = value ? "I'm Active" : "Not Active";
            }
        }

        public event EventHandler IsActiveChanged;

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            
        }
    }
}
