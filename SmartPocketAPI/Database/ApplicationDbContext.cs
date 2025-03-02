using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SmartPocketAPI.Extensions;
using SmartPocketAPI.Models;
using SmartPocketAPI.Models.General;
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
    public DbSet<Configuration> UserConfigurations { get; set; }


    public ConfigurationOptions _options { get; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> _context, IOptions<ConfigurationOptions> options) : base(_context)
    {
        _options = options.Value;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_options.ConnectionString);
    }

    private void SetTimeStamp()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is ITimestampedEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entityEntry in entries)
        {
            ((ITimestampedEntity)entityEntry.Entity).UpdatedAt = DateTime.UtcNow;
            if (entityEntry.State == EntityState.Added)
            {
                ((ITimestampedEntity)entityEntry.Entity).CreatedAt = DateTime.UtcNow;
            }
        }
    }

    public override int SaveChanges()
    {
        SetTimeStamp();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetTimeStamp();
        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        #region User

        modelBuilder.Entity<User>()
            .ToTable("User");

        #endregion

        #region Frequency

        modelBuilder.Entity<Frequency>()
            .ToTable("Frequency");

        #endregion

        #region MovementType

        modelBuilder.Entity<MovementType>()
            .ToTable("MovementType");

        #endregion

        #region PaymentMethodType

        modelBuilder.Entity<PaymentMethodType>()
            .ToTable("PaymentMethodType");

        #endregion

        #region Category

        modelBuilder.Entity<Category>()
            .ToTable("Category");

            modelBuilder.Entity<Category>()
                .HasOne(c => c.User)
                .WithMany(u => u.Categories)
                .HasForeignKey(c => c.UserId);

        #endregion

        #region PaymentMethod

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

        #endregion

        #region RecurringPayment

        modelBuilder.Entity<RecurringPayment>()
            .ToTable("RecurringPayment");

            modelBuilder.Entity<RecurringPayment>()
                .HasOne(c => c.User)
                .WithMany(u => u.RecurringPayments)
                .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<RecurringPayment>()
                .HasOne(c => c.Category)
                .WithMany(u => u.RecurringPayments)
                .HasForeignKey(m => m.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<RecurringPayment>()
                .HasOne(c => c.PaymentMethod)
                .WithMany(u => u.RecurringPayments)
                .HasForeignKey(m => m.PaymentMethodId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<RecurringPayment>()
                .HasOne(c => c.MovementType)
                .WithMany(u => u.RecurringPayments)
                .HasForeignKey(m => m.MovementTypeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<RecurringPayment>()
                .HasOne(c => c.CreditCardPayment)
                .WithMany(u => u.RecurringPaymentCredits)
                .HasForeignKey(m => m.CreditCardPaymentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<RecurringPayment>()
                    .HasOne(c => c.Frequency)
                    .WithMany(p => p.RecurringPayments)
                    .HasForeignKey(c => c.FrequencyId);

        #endregion

        #region Movement

        modelBuilder.Entity<Movement>()
            .ToTable("Movement");

        modelBuilder.Entity<Movement>()
                .HasOne(c => c.User)
                .WithMany(u => u.Movements)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Movement>()
                .HasOne(c => c.Category)
                .WithMany(u => u.Movements)
                .HasForeignKey(m => m.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Movement>()
                .HasOne(c => c.PaymentMethod)
                .WithMany(u => u.Movements)
                .HasForeignKey(m => m.PaymentMethodId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Movement>()
                .HasOne(c => c.RecurringPayment)
                .WithMany(u => u.Movements)
                .HasForeignKey(m => m.RecurringPaymentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Movement>()
                .HasOne(c => c.MovementType)
                .WithMany(u => u.Movements)
                .HasForeignKey(m => m.MovementTypeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Movement>()
                .HasOne(c => c.CreditCardPayment)
                .WithMany(u => u.Payments)
                .HasForeignKey(m => m.CreditCardPaymentId)
                .OnDelete(DeleteBehavior.NoAction);

        #endregion

        #region Configuration

        //modelBuilder.Entity<Configuration>()
        //    .Ignore("User");

        modelBuilder.Entity<Configuration>()
            .ToTable("Configuration");

        modelBuilder.Entity<Configuration>()
                .HasOne(c => c.User)
                .WithMany(u => u.Configurations)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction);

        #endregion

        modelBuilder.Seed();
    }
}