using Microsoft.EntityFrameworkCore;
using Prism.Mvvm;

namespace DbContextFactory.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        private readonly IDbContextFactory<BloggingContext> _contextFactory;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel(IDbContextFactory<BloggingContext> contextFactory)
        {
            _contextFactory = contextFactory;

            using (var context = _contextFactory.CreateDbContext())
            {
                var blog = new Blog { Url = "http://example.com", Rating = 10 };
                context.Add<Blog>(blog);
                context.SaveChanges();
            }
        }

    }
}
