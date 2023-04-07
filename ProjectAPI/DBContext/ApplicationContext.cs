using Microsoft.EntityFrameworkCore;
using ProjectAPI.Models;

namespace ProjectAPI.DBContext
{
    public partial class ApplicationContext : DbContext
    {
        public ApplicationContext() { }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }


        public DbSet<ClientDb> Clients { get; set; }
        public DbSet<OrderStringDb> OrderStrings { get; set; }
        public DbSet<OrderListDb> OrderLists { get; set; }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlite(AppSettings.ConnectionString);
        //    }
        //}
    }
}
