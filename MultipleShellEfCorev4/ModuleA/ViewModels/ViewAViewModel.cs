using Infrastructure;
using Infrastructure.Prism;
using MultipleShellEfCore.Data;
using MultipleShellEfCore.Domain;
using MultipleShellEfCore.Infrastructure;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleA.ViewModels
{
    public class ViewAViewModel : BindableBase, IRegionManagerAware, IDatabaseManagerAware, INavigationAware
    {
        private string _message;
        private readonly BloggingContextFactory _contextFactory;
        public ObservableCollection<Blog> Blogs { get; private set; }
        private BloggingContext _dbContext;

        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public DelegateCommand NavigateCommand { get; private set; }
        public DelegateCommand AddBlogCommand { get; private set; }
        public DelegateCommand DeleteBlogCommand { get; private set; }
        public IRegionManager RegionManager { get; set; }
        public IDatabaseManager DatabaseManager { get; set; }

        public ViewAViewModel(BloggingContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
            Blogs = new ObservableCollection<Blog>();
            Message = "View A from your Prism Module";

            NavigateCommand = new DelegateCommand(Navigate);
            AddBlogCommand = new DelegateCommand(AddBlog);
            DeleteBlogCommand = new DelegateCommand(DeleteBlog);
        }

        private void DeleteBlog()
        {
            throw new NotImplementedException();
        }

        private void AddBlog()
        {
            var itemCount = _dbContext.Blogs.Count();

            itemCount++;
            var blog = new Blog
            {
                Url = $"http://example.{itemCount}.com",
                Rating = itemCount,
                //Posts = new List<Post> { new Post { Author = "John Doe", Text = "Post 1 example test" } }
            };
            _dbContext.Add<Blog>(blog);
            _dbContext.SaveChanges();
            Blogs.Clear();
            Blogs.AddRange(_dbContext.Blogs.ToList());

        }

        private void Navigate()
        {
            RegionManager.RequestNavigate(RegionNames.ContentRegion, "ViewB");
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _dbContext = _contextFactory.CreateDbContext(new string[] { DatabaseManager.DatabaseName });
            _dbContext.Database.EnsureCreated();
            Blogs.Clear();
            //Blogs.AddRange(_dbContext.Blogs.Local.ToObservableCollection());
            if (_dbContext.Blogs.Count() > 0)
            {
                Blogs.AddRange(_dbContext.Blogs.ToList());
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            _dbContext.Dispose();
        }
    }
}
