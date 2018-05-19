using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BankWebApplication.Models
{
    public partial class BankDBContext : DbContext
    {
        public virtual DbSet<AccountDetails> AccountDetails { get; set; }
        public virtual DbSet<AllTransactions> AllTransactions { get; set; }
        public virtual DbSet<NextAccountNumber> NextAccountNumber { get; set; }
        public virtual DbSet<NextTransactionNo> NextTransactionNo { get; set; }
        public virtual DbSet<OnlineAccounts> OnlineAccounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=KALE\SQLEXPRESS;Database=BankDB;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountDetails>(entity =>
            {
                entity.HasKey(e => e.AccountNumber);

                entity.Property(e => e.AccountNumber)
                    .HasColumnName("accountNumber")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("firstName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("lastName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNo).HasColumnName("phoneNo");

                entity.Property(e => e.Proof)
                    .HasColumnName("proof")
                    .HasColumnType("image");

                entity.Property(e => e.Ssn)
                    .HasColumnName("ssn")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AllTransactions>(entity =>
            {
                entity.HasKey(e => e.TransId);

                entity.Property(e => e.TransId)
                    .HasColumnName("transID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AccountNumber).HasColumnName("accountNumber");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Statements)
                    .IsRequired()
                    .HasColumnName("statements");

                entity.HasOne(d => d.AccountNumberNavigation)
                    .WithMany(p => p.AllTransactions)
                    .HasForeignKey(d => d.AccountNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AllTransactions_AccountDetails");
            });

            modelBuilder.Entity<NextAccountNumber>(entity =>
            {
                entity.HasKey(e => e.NextId);

                entity.Property(e => e.NextId)
                    .HasColumnName("nextID")
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<NextTransactionNo>(entity =>
            {
                entity.HasKey(e => e.NextId);

                entity.Property(e => e.NextId)
                    .HasColumnName("nextID")
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<OnlineAccounts>(entity =>
            {
                entity.HasKey(e => e.UserName);

                entity.Property(e => e.UserName)
                    .HasColumnName("userName")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.LastLogin)
                    .HasColumnName("lastLogin")
                    .HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
