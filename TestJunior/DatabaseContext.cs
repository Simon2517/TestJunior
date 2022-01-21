using Microsoft.EntityFrameworkCore;
using TestJunior;
namespace TestJunior
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext>options):base(options)
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Brand> Brand { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Nation> Nation { get; set; }
        public virtual DbSet<ProductCategories> ProductCategories { get; set; }
        public virtual DbSet<InfoRequestReply> InfoRequestReply { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<InfoRequest> InfoRequest { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>(entity => {
                entity.Property(a => a.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Brand>(entity => {
            entity.Property(b => b.Id).ValueGeneratedOnAdd(); 
            modelBuilder.Entity<Brand>()
                .HasOne(b => b.Account)
                .WithOne(a => a.Brand)
                .HasForeignKey<Brand>(b => b.AccountId);
            });

            modelBuilder.Entity<User>(entity => {
                entity.Property(u => u.Id).ValueGeneratedOnAdd();
                modelBuilder.Entity<User>()
                .HasOne(a => a.Account)
                .WithOne(u => u.User)
                .HasForeignKey<User>(u => u.AccountId); 
            });

            modelBuilder.Entity<Product>(entity => {
                entity.Property(p => p.ProductId).ValueGeneratedOnAdd();
                modelBuilder.Entity<Product>()
                .HasOne(p=>p.Brand)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.BrandId);
            });

            modelBuilder.Entity<InfoRequest>(entity => {
                entity.Property(Ir => Ir.Id).ValueGeneratedOnAdd();
                modelBuilder.Entity<InfoRequest>()
                .HasOne(Ir => Ir.Product)
                .WithMany(p => p.InfoRequests)
                .HasForeignKey(Ir => Ir.ProductId);
                modelBuilder.Entity<InfoRequest>()
                .HasOne(Ir=>Ir.User)
                .WithMany(u=>u.InfoRequests)
                .HasForeignKey(u=>u.UserId);
                modelBuilder.Entity<InfoRequest>()
                .HasOne(Ir=>Ir.Nation)
                .WithMany(n=>n.InfoRequests)
                .HasForeignKey(Ir=>Ir.StateId);
            });

            modelBuilder.Entity<InfoRequestReply>(entity => {
                entity.Property(Irr => Irr.Id).ValueGeneratedOnAdd();
                modelBuilder.Entity<InfoRequestReply>()
                .HasOne(irr=>irr.Account)
                .WithMany(a=>a.InfoRequestReplies)
                .HasForeignKey(irr=>irr.AccountId);
                modelBuilder.Entity<InfoRequestReply>()
                .HasOne(irr=>irr.InfoRequest)
                .WithMany(ir=>ir.InfoRequestReplies)
                .HasForeignKey(irr=>irr.InfoRequestId);
            });

            modelBuilder.Entity<Nation>(entity => {
                entity.Property(n => n.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Category>(entity => {
                entity.Property(c => c.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<ProductCategories>(entity =>
            {
                entity.HasKey(Pc => new { Pc.ProductId, Pc.CategoryId });
                entity.HasOne(Pc=>Pc.Category)
                        .WithMany(C=>C.ProdsCategories)
                      .HasForeignKey(C => C.CategoryId);
                entity.HasOne(Pc => Pc.Product)
                        .WithMany(P => P.ProdsCategories)
                      .HasForeignKey(p => p.ProductId);
            });
        }

    }
}
