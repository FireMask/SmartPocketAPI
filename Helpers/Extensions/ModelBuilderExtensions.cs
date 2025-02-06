using Microsoft.EntityFrameworkCore;
using SmartPocketAPI.Models;

namespace SmartPocketAPI.Helpers.Extensions;

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
                Password = "5IrYpxI+Y3DE8f6ZP6y1qw==:dR01XFPGqVy+ZUb/7gsPLGX7NkpY0dcgzPjgyUT22r8=" //Admin
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
            new MovementType { Id = 4, Name = "CreditCard Payment", NameSpanish = "Pago a Tarjeta de Credito" }
        );

        modelBuilder.Entity<PaymentMethodType>().HasData(
            new PaymentMethodType { Id = 1, Name = "Cash", NameSpanish = "Efectivo" },
            new PaymentMethodType { Id = 2, Name = "Credit Card", NameSpanish = "Tarjeta de Credito" },
            new PaymentMethodType { Id = 3, Name = "Debit Card", NameSpanish = "Tarjeta de Debito" }
        );

        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Food", NameSpanish = "Comida", UserId = adminid },
            new Category { Id = 2, Name = "Gas", NameSpanish = "Gasolina", UserId = adminid },
            new Category { Id = 3, Name = "Hobbies", NameSpanish = "Hobbies", UserId = adminid },
            new Category { Id = 4, Name = "Shopping", NameSpanish = "Compras", UserId = adminid },
            new Category { Id = 5, Name = "Bank", NameSpanish = "Bancarios", UserId = adminid }
        );

        modelBuilder.Entity<PaymentMethod>().HasData(
            new PaymentMethod { Id = 1, Name = "Cash", UserId = adminid, PaymentMethodTypeId = 1 }
        );
    }
}