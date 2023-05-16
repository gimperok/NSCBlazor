using Microsoft.EntityFrameworkCore;
using ProjectAPI.Models;
using ProjectJson.Models;

namespace ProjectAPI.DBContext
{
    public partial class ApplicationContext : DbContext
    {
        public ApplicationContext() {}

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }


        public DbSet<ClientMessage> Clients { get; set; }
        public DbSet<OrderItemMessage> OrderItems { get; set; }
        public DbSet<OrderMessage> Orders { get; set; }

    }
}
