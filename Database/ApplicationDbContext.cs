using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SmartPocketAPI.Helpers.Extensions;
using SmartPocketAPI.Models;
using SmartPocketAPI.Options;

namespace SmartPocketAPI.Database;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Frequency> Frequency { get; set; }
    public DbSet<MovementType> MovementType { get; set; }
    public DbSet<PaymentMethodType> PaymentMethodType { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<PaymentMethod> PaymentMethods { get; set; }
    public DbSet<RecurringPayment> RecurringPayments { get; set; }
    public DbSet<Movement> Movements { get; set; }

    public ConfigurationOptions _options { get; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> _context, IOptions<ConfigurationOptions> options) : base(_context)
    {
        _options = options.Value;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(_options.ConnectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .ToTable("User");

        modelBuilder.Entity<Frequency>()
            .ToTable("Frequency");

        modelBuilder.Entity<MovementType>()
            .ToTable("MovementType");

        modelBuilder.Entity<PaymentMethodType>()
            .ToTable("PaymentMethodType");

        modelBuilder.Entity<Category>()
            .ToTable("Category");

            modelBuilder.Entity<Category>()
                .HasOne(c => c.User)
                .WithMany(u => u.Categories)
                .HasForeignKey(c => c.UserId);

        modelBuilder.Entity<PaymentMethod>()
            .ToTable("PaymentMethod");

            modelBuilder.Entity<PaymentMethod>()
                .HasOne(c => c.User)
                .WithMany(u => u.PaymentMethods)
                .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<PaymentMethod>()
                .HasOne(c => c.PaymentMethodType)
                .WithMany(p => p.PaymentMethods)
                .HasForeignKey(c => c.PaymentMethodTypeId);

        modelBuilder.Entity<PaymentMethod>()
            .ToTable("PaymentMethod");

            modelBuilder.Entity<RecurringPayment>()
                .HasOne(c => c.User)
                .WithMany(u => u.RecurringPayments)
                .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<RecurringPayment>()
                .HasOne(c => c.Frequency)
                .WithMany(p => p.RecurringPayments)
                .HasForeignKey(c => c.FrecuencyId);

        modelBuilder.Entity<Movement>()
            .ToTable("Movement");

            modelBuilder.Entity<Movement>()
                .HasOne(c => c.User)
                .WithMany(u => u.Movements)
                .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<Movement>()
                .HasOne(c => c.Category)
                .WithMany(u => u.Movements)
                .HasForeignKey(m => m.CategoriId);

            modelBuilder.Entity<Movement>()
                .HasOne(c => c.PaymentMethod)
                .WithMany(u => u.Movements)
                .HasForeignKey(m => m.PaymentMethodId);

            modelBuilder.Entity<Movement>()
                .HasOne(c => c.RecurringPayment)
                .WithMany(u => u.Movements)
                .HasForeignKey(m => m.RecurringPaymentId);

            modelBuilder.Entity<Movement>()
                .HasOne(c => c.MovementType)
                .WithMany(u => u.Movements)
                .HasForeignKey(m => m.MovementTypeId);

            modelBuilder.Entity<Movement>()
                .HasOne(c => c.CreditCardPayment)
                .WithMany(u => u.Payments)
                .HasForeignKey(m => m.CreditCardPaymentId);

        modelBuilder.Seed();
    }
}