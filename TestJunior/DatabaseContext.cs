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

            ///Mapping the model for Account, specifying its primary key and not null properties
            modelBuilder.Entity<Account>(entity => {
                entity.Property(a => a.Id).ValueGeneratedOnAdd();
                entity.Property(a=>a.Email).IsRequired();
                entity.Property(a=>a.Password).IsRequired();
                entity.Property(a=>a.AccountType).IsRequired();

            });
            ///Mapping the model for Brand, specifying its primary key as identity and not null properties

            modelBuilder.Entity<Brand>(entity => {
            entity.Property(b => b.Id).ValueGeneratedOnAdd();
            entity.Property(b => b.AccountId).IsRequired();
            entity.Property(b => b.BrandName).IsRequired();

            ///Mapping foreign key between Account and Brand
                modelBuilder.Entity<Brand>()
                    .HasOne(b => b.Account)
                        .WithOne(a => a.Brand)
                    .HasForeignKey<Brand>(b => b.AccountId);
            });

            ///Mapping the model for Brand, specifying its primary key as identity and not null properties

            modelBuilder.Entity<User>(entity => {
                entity.Property(u => u.Id).ValueGeneratedOnAdd();
                entity.Property(u => u.AccountId).IsRequired();
                entity.Property(u => u.Name).IsRequired();
                entity.Property(u => u.LastName).IsRequired();


                ///Mapping foreign key between Account and User
                modelBuilder.Entity<User>()
                    .HasOne(a => a.Account)
                        .WithOne(u => u.User)
                    .HasForeignKey<User>(u => u.AccountId); 
            });

            ///Mapping the model for Product, specifying its primary key as identity and not null properties
            modelBuilder.Entity<Product>(entity => {
                entity.Property(p => p.ProductId).ValueGeneratedOnAdd();
                entity.Property(p => p.BrandId).IsRequired();
                entity.Property(p => p.Name).IsRequired();
                entity.Property(p => p.Price).IsRequired();

                ///Mapping foreign key between Product and Brand
                modelBuilder.Entity<Product>()
                    .HasOne(p=>p.Brand)
                        .WithMany(b => b.Products)
                    .HasForeignKey(p => p.BrandId);
            });

            ///Mapping the model for InfoRequest, specifying its primary key as identity and not null properties
            modelBuilder.Entity<InfoRequest>(entity => {
                entity.Property(Ir => Ir.Id).ValueGeneratedOnAdd();
                entity.Property(Ir => Ir.StateId).IsRequired();
                entity.Property(Ir => Ir.Email).IsRequired();
                entity.Property(Ir => Ir.ProductId).IsRequired();
                entity.Property(Ir => Ir.RequestText).IsRequired();
                entity.Property(Ir => Ir.InsertedDate).IsRequired();
                entity.Property(Ir => Ir.Name).IsRequired();
                entity.Property(Ir => Ir.LastName).IsRequired();

                ///Mapping foreign key between InfoRequest and Product
                modelBuilder.Entity<InfoRequest>()
                    .HasOne(Ir => Ir.Product)
                        .WithMany(p => p.InfoRequests)
                    .HasForeignKey(Ir => Ir.ProductId);

                ///Mapping foreign key between InfoRequest and User
                modelBuilder.Entity<InfoRequest>()
                    .HasOne(Ir=>Ir.User)
                        .WithMany(u=>u.InfoRequests)
                    .HasForeignKey(u=>u.UserId);

                ///Mapping foreign key between InfoRequest and Nation
                modelBuilder.Entity<InfoRequest>()
                    .HasOne(Ir=>Ir.Nation)
                        .WithMany(n=>n.InfoRequests)
                    .HasForeignKey(Ir=>Ir.StateId);
            });

            ///Mapping the model for InfoRequestReply, specifying its primary key as identity and not null properties
            modelBuilder.Entity<InfoRequestReply>(entity => {
                entity.Property(Irr => Irr.Id).ValueGeneratedOnAdd();
                entity.Property(Irr => Irr.AccountId).IsRequired();
                entity.Property(Irr => Irr.InfoRequestId).IsRequired();
                entity.Property(Irr => Irr.ReplyText).IsRequired();
                entity.Property(Irr => Irr.InsertedDate).IsRequired();

                ///Mapping foreign key between InfoRequestReply and Account
                modelBuilder.Entity<InfoRequestReply>()
                .HasOne(irr=>irr.Account)
                .WithMany(a=>a.InfoRequestReplies)
                .HasForeignKey(irr=>irr.AccountId);

                ///Mapping foreign key between InfoRequestReply and InfoRequest
                modelBuilder.Entity<InfoRequestReply>()
                .HasOne(irr=>irr.InfoRequest)
                .WithMany(ir=>ir.InfoRequestReplies)
                .HasForeignKey(irr=>irr.InfoRequestId);
            });

            ///Mapping the model for Nation, specifying its primary key as identity and not null properties
            modelBuilder.Entity<Nation>(entity => {
                entity.Property(n => n.Id).ValueGeneratedOnAdd();
                entity.Property(n => n.Name).IsRequired();
            });

            ///Mapping the model for Category, specifying its primary key as identity and not null properties
            modelBuilder.Entity<Category>(entity => {
                entity.Property(c => c.Id).ValueGeneratedOnAdd();
                entity.Property(c => c.Name).IsRequired();

            });

            ///Mapping the model for ProductCategories, specifying its primary keys and
            ///its many to many reletionships between (Product and ProductsCategories) 
            ///and (Category and ProductCategories)
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
