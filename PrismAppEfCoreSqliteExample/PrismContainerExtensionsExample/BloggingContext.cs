using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrismContainerExtensionsExample
{
    public class BloggingContext : DbContext
    {
        IFileService _fileService;
        DbSet<Blog> Blogs {get; set;}

        public BloggingContext(DbContextOptions<BloggingContext> options, IFileService fileService)
           : base(options)
        {
            _fileService = fileService;
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={_fileService.FileName}");
        }
    }
}
