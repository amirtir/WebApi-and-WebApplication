using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MyWeb.Models
{
    public partial class EshopApi_DBContext : DbContext
    {
        public EshopApi_DBContext()
        {  
        }

        public EshopApi_DBContext(DbContextOptions<EshopApi_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<OrderItem> OrderItem { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<SalePersons> SalePersons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {


//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=EshopApi_DB;Integrated Security=True;");
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasKey(e => e.CustomerId);

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.Address).HasMaxLength(150);

                entity.Property(e => e.City).HasMaxLength(150);

                entity.Property(e => e.Email).HasMaxLength(150);

                entity.Property(e => e.FirstName).HasMaxLength(150);

                entity.Property(e => e.LastName).HasMaxLength(150);

                entity.Property(e => e.Phone).HasMaxLength(150);

                entity.Property(e => e.State).HasMaxLength(150);

                entity.Property(e => e.ZipCode).HasMaxLength(150);
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.Property(e => e.OrderItemId).ValueGeneratedNever();

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItem)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderItem_Orders");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderItem)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderItem_Products");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.OrderId);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Status).HasMaxLength(150);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_Costumers");

                entity.HasOne(d => d.SalePerson)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.SalePersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_SalePersons");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.Property(e => e.ProductName).HasMaxLength(150);

                entity.Property(e => e.Status).HasMaxLength(150);

                entity.Property(e => e.Varienty).HasMaxLength(150);
            });

            modelBuilder.Entity<SalePersons>(entity =>
            {
                entity.HasKey(e => e.SalePersonId);

                entity.Property(e => e.SalePersonId).HasColumnName("SalePersonID");

                entity.Property(e => e.Address).HasMaxLength(150);

                entity.Property(e => e.City).HasMaxLength(150);

                entity.Property(e => e.Email).HasMaxLength(150);

                entity.Property(e => e.FirstName).HasMaxLength(150);

                entity.Property(e => e.LastName).HasMaxLength(150);

                entity.Property(e => e.Phone).HasMaxLength(150);

                entity.Property(e => e.State).HasMaxLength(150);

                entity.Property(e => e.ZipCode).HasMaxLength(150);
            });
        }
    }
}
