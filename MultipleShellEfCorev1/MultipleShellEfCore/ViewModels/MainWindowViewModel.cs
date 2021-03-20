using Infrastructure;
using Infrastructure.Prism;
using MultipleShellEfCore.Data;
using MultipleShellEfCore.Domain;
using MultipleShellEfCore.Infrastructure;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Linq;

namespace MultipleShellEfCore.ViewModels
{
    public class MainWindowViewModel : BindableBase, IRegionManagerAware
    {
        private string _title = "Prism Application";
        private BloggingContext _bloggingContext;
        //private readonly IRegionManager _regionManager;
        private readonly IShellService _shellService;
        private FileService _fileService;
        private int _itemCount;

        public DelegateCommand<string> NavigateCommand { get; private set; }
        public DelegateCommand AddDbCommand { get; private set; }
        public DelegateCommand<string> OpenShellCommand { get; private set; }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public IRegionManager RegionManager { get; set; }

        public MainWindowViewModel(BloggingContext bloggingContext, IShellService shellService, FileService fileService)
        {
            _bloggingContext = bloggingContext;
            _shellService = shellService;
            _fileService = fileService;

            OpenShellCommand = new DelegateCommand<string>(OpenShell);
            NavigateCommand = new DelegateCommand<string>(Navigate);
            AddDbCommand = new DelegateCommand(AddItemToDb);
        }

        private void AddItemToDb()
        {
            var itemCount = _bloggingContext.Blogs.Count();
            itemCount++;

            var blog = new Blog { Url = $"http://example.{_itemCount}.com", Rating = _itemCount};
            _bloggingContext.Add<Blog>(blog);
            _bloggingContext.SaveChanges();
        }

        private void Navigate(string viewName)
        {
            RegionManager.RequestNavigate(RegionNames.ContentRegion, viewName);
        }

        void OpenShell(string viewName)
        {
            _fileService.FileName = "blog2.db";
            _shellService.ShowShell(viewName);
        }
    }
}
