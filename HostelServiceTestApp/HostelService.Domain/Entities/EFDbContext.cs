namespace HostelService.Domain.Entities {
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class EFDbContext : DbContext {
        public EFDbContext()
            : base("name=EFDbContext") {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Booking> Booking { get; set; }
        public virtual DbSet<Card> Card { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Entity<Account>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.Type)
                .IsUnicode(false);

            modelBuilder.Entity<Booking>()
                .Property(e => e.SiteID)
                .IsUnicode(false);

            modelBuilder.Entity<Booking>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<Booking>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Booking>()
                .Property(e => e.TypeOfRoom)
                .IsUnicode(false);

            modelBuilder.Entity<Booking>()
                .Property(e => e.Campaign)
                .IsUnicode(false);

            modelBuilder.Entity<Booking>()
                .Property(e => e.Category)
                .IsUnicode(false);

            modelBuilder.Entity<Card>()
                .Property(e => e.CardNumberBase)
                .IsUnicode(false);

            modelBuilder.Entity<Card>()
                .Property(e => e.SiteID)
                .IsUnicode(false);

            modelBuilder.Entity<Card>()
                .Property(e => e.CardType)
                .IsUnicode(false);

            modelBuilder.Entity<Card>()
                .Property(e => e.ZipCode)
                .IsUnicode(false);

            modelBuilder.Entity<Card>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.ZipCode)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.SiteID)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Category)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Interests)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Booking)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);
        }
    }
}
