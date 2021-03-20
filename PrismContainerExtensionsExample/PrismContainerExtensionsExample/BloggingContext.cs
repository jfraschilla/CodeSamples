using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrismContainerExtensionsExample
{
    public class BloggingContext : DbContext
    {
        DbSet<Blog> Blogs {get; set;}

        public BloggingContext(DbContextOptions<BloggingContext> options)
           : base(options)
        {
            Database.
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }
}
