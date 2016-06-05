using Prism.Commands;
using Prism.Mvvm;
using System.Windows.Input;
using System;
using Prism.Regions;
using Microsoft.Practices.Unity;
using ModuleA.Views;

namespace TabControlRegion.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public ICommand NavigateCommand { get; set; }

        private string _title = "Prism Unity Application";
        private IRegionManager _regionManager;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            NavigateCommand = new DelegateCommand<string>(Navigate);
        }

        private void Navigate(string navigationPath)
        {
            //This is not preferred due to a number of reasons.
            //1.I would need to put additional logic here to navigate to ViewB
            //2. I needed to add a reference to a concrete type ViewA.  Therefore there is no decoupling
            //3. We do not want to pass the container to our ViewModels
            //var region = _regionManager.Regions["TabRegion"];
            //var view = _container.Resolve<ViewA>();
            //region.Add(view);
            //region.Activate(view);


            _regionManager.RequestNavigate("TabRegion", navigationPath);
        }
    }
}
