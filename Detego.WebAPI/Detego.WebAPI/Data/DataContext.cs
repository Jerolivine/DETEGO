using Detego.WebAPI.Models;
using Detego.WebAPI.Models.LookUpModels;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Detego.WebAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Store> Store { get; set; }
        public DbSet<SystemUser> SystemUser { get; set; }
        public DbSet<StoreStockDetail> StoreStockDetail { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<CountryCode> CountryCode { get; set; }
        public DbSet <ReportType> ReportType { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Store>()
            .HasOne(p => p.StoreStockDetail)
            .WithOne(i => i.Store)
            .HasForeignKey<StoreStockDetail>(b => b.StoreId);
            

        }

    }

}