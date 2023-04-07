//using Microsoft.EntityFrameworkCore;
//using NscProject.Shared;

//namespace NscProject.Data
//{
//    public partial class ApplicationContext : DbContext
//    {
//        public ApplicationContext()
//        {

//        }
//        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }


//        public DbSet<Client> Clients { get; set; }
//        public DbSet<OrderString> OrderStrings { get; set; }
//        public DbSet<OrderList> OrderLists { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//                optionsBuilder.UseSqlite(AppSettings.ConnectionString);
//            }
//        }


//    }

//    //protected override void OnModelCreating(ModelBuilder modelBuilder)
//    //{
//    //    modelBuilder.Entity<Client>(entity =>
//    //    {
//    //        entity.ToTable("Clients");
//    //        entity.Property(e => e.Id).HasColumnName("Id");
//    //        entity.Property(e => e.Name).HasColumnName("Name");
//    //        entity.Property(e => e.Surname).HasColumnName("Surname");
//    //        entity.Property(e => e.Country).HasColumnName("Country");
//    //        entity.Property(e => e.City).HasColumnName("City");
//    //        entity.Property(e => e.Cargo).HasColumnName("Cargo");
//    //        entity.Property(e => e.Tel).HasColumnName("Tel");
//    //    });

//    //    //modelBuilder.Entity<OrderString>(entity =>
//    //    //{
//    //    //    entity.ToTable("OrderStrings");
//    //    //    entity.Property(e => e.Id).HasColumnName("Id");
//    //    //    entity.Property(e => e.Kod).HasColumnName("Kod");
//    //    //    entity.Property(e => e.Leather).HasColumnName("Leather");
//    //    //    entity.Property(e => e.Color).HasColumnName("Color");

//    //    //    entity.Property(e => e.Size35).HasColumnName("Size35");
//    //    //    entity.Property(e => e.Size36).HasColumnName("Size36");
//    //    //    entity.Property(e => e.Size37).HasColumnName("Size37");
//    //    //    entity.Property(e => e.Size38).HasColumnName("Size38");
//    //    //    entity.Property(e => e.Size39).HasColumnName("Size39");
//    //    //    entity.Property(e => e.Size40).HasColumnName("Size40");
//    //    //    entity.Property(e => e.Size41).HasColumnName("Size41");


//    //    //    entity.Property(e => e.TotalCountPairs).HasColumnName("TotalCountPairs");
//    //    //    entity.Property(e => e.Price).HasColumnName("Price");
//    //    //    entity.Property(e => e.Note).HasColumnName("Note");
//    //    //});

//    //    //modelBuilder.Entity<OrderList>(entity =>
//    //    //{
//    //    //    entity.ToTable("OrderLists");
//    //    //    entity.Property(e => e.Id).HasColumnName("Id");
//    //    //    entity.Property(e => e.DateCreate).HasColumnName("Kod");
//    //    //    entity.Property(e => e.DateModify).HasColumnName("Leather");
//    //    //    entity.Property(e => e.ClientId).HasColumnName("Color");
//    //    //});


//    //        base.OnModelCreating(modelBuilder);
//    //    }

//    //}
//}
