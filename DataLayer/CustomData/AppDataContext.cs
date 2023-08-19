using DataLayer.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.CustomData
{
    public class AppDataContext : DbContext
    {
        protected readonly IConfiguration _configuration;
        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
        {
        }
        public DbSet<User> User { get; set; }
        public DbSet<CMSModel> CMS { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Email).IsRequired().IsUnicode();
            });
            modelBuilder.Entity<CMSModel>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Page).IsRequired().IsUnicode();
                entity.Property(e => e.Content).IsRequired();
                entity.Property(e => e.IsEdit).HasDefaultValue(false);
            });
        }
    }
}
