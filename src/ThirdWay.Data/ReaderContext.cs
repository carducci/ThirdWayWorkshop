using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
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
            DbPath = "reader.db";
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) =>
            options.UseSqlite($"Data Source={DbPath}");

    }

 
}
