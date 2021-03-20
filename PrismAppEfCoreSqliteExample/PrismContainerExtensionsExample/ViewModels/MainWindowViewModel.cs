using Prism.Mvvm;

namespace PrismContainerExtensionsExample.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        private BloggingContext _bloggingContext;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel(BloggingContext bloggingContext)
        {
            _bloggingContext = bloggingContext;

            var blog = new Blog { Url = "http://example.com", Rating = 10 };
            _bloggingContext.Add<Blog>(blog);
            _bloggingContext.SaveChanges();
        }
    }
}
