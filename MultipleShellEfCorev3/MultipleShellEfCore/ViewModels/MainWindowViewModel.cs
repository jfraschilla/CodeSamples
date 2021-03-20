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
        private IDbContextFactory<BloggingContext> _bloggingContextFactory;

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

        public MainWindowViewModel(IDbContextFactory<BloggingContext> bloggingContextFactory, IShellService shellService, FileService fileService)
        {
            _bloggingContextFactory = bloggingContextFactory;
            _shellService = shellService;
            _fileService = fileService;

            OpenShellCommand = new DelegateCommand<string>(OpenShell);
            NavigateCommand = new DelegateCommand<string>(Navigate);
            AddDbCommand = new DelegateCommand(AddItemToDb);

            _fileService.FileName = "blog1.db";

        }

        private void AddItemToDb()
        {
            using (var context = _bloggingContextFactory.CreateDbContext())
            {
                var itemCount = context.Blogs.Count();
                itemCount++;

                var blog = new Blog { Url = $"http://example.{_itemCount}.com", Rating = _itemCount };
                context.Add<Blog>(blog);
                context.SaveChanges();
            }
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
