using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartPocketAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Frequency",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    NameSpanish = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Frequency", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovementType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    NameSpanish = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovementType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethodType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    NameSpanish = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethodType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Alias = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    NameSpanish = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    DescriptionSpanish = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    PaymentMethodTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Bank = table.Column<string>(type: "TEXT", nullable: false),
                    IsCreditCard = table.Column<bool>(type: "INTEGER", nullable: false),
                    DueDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DefaultInterestRate = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethod", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentMethod_PaymentMethodType_PaymentMethodTypeId",
                        column: x => x.PaymentMethodTypeId,
                        principalTable: "PaymentMethodType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaymentMethod_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Frequency",
                columns: new[] { "Id", "Name", "NameSpanish" },
                values: new object[,]
                {
                    { 1, "Daily", "Diario" },
                    { 2, "Weekly", "Semanal" },
                    { 3, "Monthly", "Mensual" },
                    { 4, "Bimonthly", "Bimestral" },
                    { 5, "Quarter", "Trimestral" },
                    { 6, "Anual", "Anual" }
                });

            migrationBuilder.InsertData(
                table: "MovementType",
                columns: new[] { "Id", "Name", "NameSpanish" },
                values: new object[,]
                {
                    { 1, "Paymenty", "Pago" },
                    { 2, "Income", "Ingreso" },
                    { 3, "Purchase", "Compra" },
                    { 4, "CreditCard Payment", "Pago a Tarjeta de Credito" }
                });

            migrationBuilder.InsertData(
                table: "PaymentMethodType",
                columns: new[] { "Id", "Name", "NameSpanish" },
                values: new object[,]
                {
                    { 1, "Cash", "Efectivo" },
                    { 2, "Credit Card", "Tarjeta de Credito" },
                    { 3, "Debit Card", "Tarjeta de Debito" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Alias", "Email", "Name", "Password" },
                values: new object[] { new Guid("95eb5d5b-dd03-4c31-8a59-80d59b73df7c"), "Admin", "", "Admin", "5IrYpxI+Y3DE8f6ZP6y1qw==:dR01XFPGqVy+ZUb/7gsPLGX7NkpY0dcgzPjgyUT22r8=" });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Description", "DescriptionSpanish", "Name", "NameSpanish", "UserId" },
                values: new object[,]
                {
                    { 1, "", "", "Food", "Comida", new Guid("95eb5d5b-dd03-4c31-8a59-80d59b73df7c") },
                    { 2, "", "", "Gas", "Gasolina", new Guid("95eb5d5b-dd03-4c31-8a59-80d59b73df7c") },
                    { 3, "", "", "Hobbies", "Hobbies", new Guid("95eb5d5b-dd03-4c31-8a59-80d59b73df7c") },
                    { 4, "", "", "Shopping", "Compras", new Guid("95eb5d5b-dd03-4c31-8a59-80d59b73df7c") },
                    { 5, "", "", "Bank", "Bancarios", new Guid("95eb5d5b-dd03-4c31-8a59-80d59b73df7c") }
                });

            migrationBuilder.InsertData(
                table: "PaymentMethod",
                columns: new[] { "Id", "Bank", "DefaultInterestRate", "DueDate", "IsCreditCard", "Name", "PaymentMethodTypeId", "TransactionDate", "UserId" },
                values: new object[] { 1, "", 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Cash", 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("95eb5d5b-dd03-4c31-8a59-80d59b73df7c") });

            migrationBuilder.CreateIndex(
                name: "IX_Category_UserId",
                table: "Category",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethod_PaymentMethodTypeId",
                table: "PaymentMethod",
                column: "PaymentMethodTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethod_UserId",
                table: "PaymentMethod",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Frequency");

            migrationBuilder.DropTable(
                name: "MovementType");

            migrationBuilder.DropTable(
                name: "PaymentMethod");

            migrationBuilder.DropTable(
                name: "PaymentMethodType");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
