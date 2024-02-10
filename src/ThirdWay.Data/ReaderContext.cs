using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ThirdWay.Data.Extensions;
using ThirdWay.Data.Model;

namespace ThirdWay.Data
{
    public class ReaderContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Feed> Feeds { get; set; }

        public string DbPath { get; }
        public ReaderContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "reader.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) =>
            options.UseSqlite($"Data Source={DbPath}");

    }

 
}
