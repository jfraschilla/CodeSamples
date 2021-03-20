using Microsoft.EntityFrameworkCore;
using MultipleShellEfCore.Domain;
using MultipleShellEfCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultipleShellEfCore.Data
{
    public class BloggingContext : DbContext
    {
        IFileService _fileService;
        public DbSet<Blog> Blogs {get; set;}

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
