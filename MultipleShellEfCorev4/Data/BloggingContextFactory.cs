using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultipleShellEfCore.Data
{
    public class BloggingContextFactory : IDesignTimeDbContextFactory<BloggingContext>
    {
        public BloggingContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BloggingContext>();

            if (args.Length == 0)
                optionsBuilder.UseSqlite("Data Source=blog.db");
            else
                optionsBuilder.UseSqlite($"Data Source={args[0]}");

            return new BloggingContext(optionsBuilder.Options);
        }
    }
}
