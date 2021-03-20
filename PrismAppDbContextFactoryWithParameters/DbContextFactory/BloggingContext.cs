using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DbContextFactory
{
    public class BloggingContext : DbContext
    {
        private Func<string, DbContext> _contextFactory;

        DbSet<Blog> Blogs {get; set;}

        //public BloggingContext(DbContextOptions<BloggingContext> options)
        //   : base(options)
        //{
        //    Database.EnsureDeleted();
        //    Database.EnsureCreated();
        //}

        public BloggingContext(Func<string, DbContext> contextFactory, string name)
        {
            _contextFactory = contextFactory;
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=blogDB2.db");
        }
    }
}
