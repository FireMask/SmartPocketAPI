using Microsoft.EntityFrameworkCore;
using SmartPocketAPI.Models;

namespace SmartPocketAPI.Extensions;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {

        Guid adminid = new Guid("95eb5d5b-dd03-4c31-8a59-80d59b73df7c");

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = adminid,
                Name = "Admin",
                Email = "admin@admin.com",
                Password = "5IrYpxI+Y3DE8f6ZP6y1qw==:dR01XFPGqVy+ZUb/7gsPLGX7NkpY0dcgzPjgyUT22r8=", //Admin
                IsAdmin = true,
                IsPremium = true
            }
        );

        modelBuilder.Entity<Frequency>().HasData(
            new Frequency { Id = 1, Name = "Daily", NameSpanish = "Diario" },
            new Frequency { Id = 2, Name = "Weekly", NameSpanish = "Semanal" },
            new Frequency { Id = 3, Name = "Monthly", NameSpanish = "Mensual" },
            new Frequency { Id = 4, Name = "Bimonthly", NameSpanish = "Bimestral" },
            new Frequency { Id = 5, Name = "Quarter", NameSpanish = "Trimestral" },
            new Frequency { Id = 6, Name = "Anual", NameSpanish = "Anual" }
        );

        modelBuilder.Entity<MovementType>().HasData(
            new MovementType { Id = 1, Name = "Paymenty", NameSpanish = "Pago" },
            new MovementType { Id = 2, Name = "Income", NameSpanish = "Ingreso" },
            new MovementType { Id = 3, Name = "Purchase", NameSpanish = "Compra" },
            new MovementType { Id = 4, Name = "Credit Card Payment", NameSpanish = "Pago a Tarjeta de Credito" }
        );

        modelBuilder.Entity<PaymentMethodType>().HasData(
            new PaymentMethodType { Id = 1, Name = "Cash", NameSpanish = "Efectivo" },
            new PaymentMethodType { Id = 2, Name = "Credit Card", NameSpanish = "Tarjeta de Credito" },
            new PaymentMethodType { Id = 3, Name = "Debit Card", NameSpanish = "Tarjeta de Debito" }
        );

        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Food", NameSpanish = "Comida", UserId = adminid, IsDefault = true },
            new Category { Id = 2, Name = "Gas", NameSpanish = "Gasolina", UserId = adminid, IsDefault = true },
            new Category { Id = 3, Name = "Hobbies", NameSpanish = "Hobbies", UserId = adminid, IsDefault = true },
            new Category { Id = 4, Name = "Shopping", NameSpanish = "Compras", UserId = adminid, IsDefault = true },
            new Category { Id = 5, Name = "Bank", NameSpanish = "Bancarios", UserId = adminid, IsDefault = true },
            new Category { Id = 6, Name = "Others", NameSpanish = "Otros", UserId = adminid, IsDefault = true }
        );

        modelBuilder.Entity<PaymentMethod>().HasData(
            new PaymentMethod { Id = 1, Name = "Cash", UserId = adminid, PaymentMethodTypeId = 1, DueDate = 4, TransactionDate = 15, IsDefault = true }
        );

        modelBuilder.Entity<Configuration>().HasData(
            new Configuration { Id = 1, Key = "DarkMode", DefaultValue = "false" },
            new Configuration { Id = 2, Key = "ItemsPerPage" , DefaultValue = "10" },
            new Configuration { Id = 3, Key = "DefaultPaymentMethod" , DefaultValue = "1" } //Cash
        );
    }
}