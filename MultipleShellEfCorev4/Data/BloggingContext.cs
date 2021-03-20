using Microsoft.EntityFrameworkCore;
using MultipleShellEfCore.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultipleShellEfCore.Data
{
    public class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs {get; set;}

        public BloggingContext(DbContextOptions<BloggingContext> options)
           : base(options)
        {

        }
    }
}
