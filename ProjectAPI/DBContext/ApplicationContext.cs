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


        //public DbSet<ClientMessage> Clients { get; set; }
        //public DbSet<OrderItemMessage> OrderItems { get; set; }
        //public DbSet<OrderMessage> Orders { get; set; }
        public DbSet<ClientDb> Clients { get; set; }
        public DbSet<OrderItemDb> OrderItems { get; set; }
        public DbSet<OrderDb> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientDb>(entity =>
            {
                entity.ToTable("Clients");

                entity.HasComment("Сущность клиента");

                entity.Property(e => e.Id)
                    .HasColumnName("Id")
                    .HasComment("Идентификатор клиента");

                entity.Property(e => e.Name)
                    .HasColumnName("Name");

                entity.Property(e => e.Surname)
                    .HasColumnName("Surname");

                entity.Property(e => e.Country)
                    .HasColumnName("Country");

                entity.Property(e => e.City)
                    .HasColumnName("City");

                entity.Property(e => e.Cargo)
                    .HasColumnName("Cargo");

                entity.Property(e => e.Tel)
                    .HasColumnName("Tel");

                entity.Property(e => e.DateRegistration)
                    .HasColumnName("DateRegistration")
                    .HasDefaultValueSql("timezone('UTC'::text, CURRENT_TIMESTAMP)")
                    .HasComment("Дата создания записи");
            });

            modelBuilder.Entity<OrderDb>(entity =>
            {
                entity.ToTable("Orders");

                entity.HasComment("Сущность заказа");

                entity.Property(e => e.Id)
                    .HasColumnName("Id")
                    .HasComment("Идентификатор заказа");

                entity.Property(e => e.DateCreate)
                    .HasColumnName("DateCreate")
                    .HasDefaultValueSql("timezone('UTC'::text, CURRENT_TIMESTAMP)")
                    .HasComment("Дата создания записи");

                entity.Property(e => e.DateModify)
                    .HasColumnName("DateModify")
                    .HasComment("Дата изменения записи");

                entity.Property(e => e.ClientId)
                    .HasColumnName("ClientId")
                    .HasComment("Идентификатор клиента");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

            });

            modelBuilder.Entity<OrderItemDb>(entity =>
            {
                entity.ToTable("OrderItems");

                entity.HasComment("Сущность строки заказа");

                entity.Property(e => e.Id)
                    .HasColumnName("Id")
                    .HasComment("Идентификатор строки заказа");

                entity.Property(e => e.Kod)
                    .HasColumnName("Kod");

                entity.Property(e => e.Leather)
                    .HasColumnName("Leather");

                entity.Property(e => e.Color)
                    .HasColumnName("Color");

                entity.Property(e => e.Size35)
                    .HasColumnName("Size35");

                entity.Property(e => e.Size36)
                    .HasColumnName("Size36");

                entity.Property(e => e.Size37)
                    .HasColumnName("Size37");

                entity.Property(e => e.Size38)
                    .HasColumnName("Size38");

                entity.Property(e => e.Size39)
                    .HasColumnName("Size39");

                entity.Property(e => e.Size40)
                    .HasColumnName("Size40");

                entity.Property(e => e.Size41)
                    .HasColumnName("Size41");

                entity.Property(e => e.Price)
                    .HasColumnName("Price");

                entity.Property(e => e.Note)
                    .HasColumnName("Note");

                entity.Property(e => e.OrderId)
                    .HasColumnName("OrderId");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });
        }
    }
}