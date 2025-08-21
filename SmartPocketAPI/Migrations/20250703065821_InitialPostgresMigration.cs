using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartPocketAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialPostgresMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Frequency",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    NameSpanish = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Frequency", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovementType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    NameSpanish = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovementType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethodType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    NameSpanish = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethodType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    VerifyCode = table.Column<string>(type: "text", nullable: false),
                    IsPremium = table.Column<bool>(type: "boolean", nullable: false),
                    IsAdmin = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    NameSpanish = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    DescriptionSpanish = table.Column<string>(type: "text", nullable: false),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
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
                name: "Configuration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Key = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configuration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Configuration_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    PaymentMethodTypeId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Bank = table.Column<string>(type: "text", nullable: false),
                    IsCreditCard = table.Column<bool>(type: "boolean", nullable: false),
                    DueDate = table.Column<int>(type: "integer", nullable: false),
                    TransactionDate = table.Column<int>(type: "integer", nullable: false),
                    DefaultInterestRate = table.Column<decimal>(type: "numeric", nullable: false),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "RecurringPayment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: false),
                    IsInterestFreePayment = table.Column<bool>(type: "boolean", nullable: false),
                    InstallmentCount = table.Column<int>(type: "integer", nullable: false),
                    NextInstallmentCount = table.Column<int>(type: "integer", nullable: false),
                    InstallmentAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    InstallmentAmountPerPeriod = table.Column<decimal>(type: "numeric", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    NextInstallmentDate = table.Column<DateOnly>(type: "date", nullable: false),
                    LastInstallmentDate = table.Column<DateOnly>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    PaymentMethodId = table.Column<int>(type: "integer", nullable: false),
                    MovementTypeId = table.Column<int>(type: "integer", nullable: false),
                    CreditCardPaymentId = table.Column<int>(type: "integer", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    FrequencyId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecurringPayment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecurringPayment_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RecurringPayment_Frequency_FrequencyId",
                        column: x => x.FrequencyId,
                        principalTable: "Frequency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecurringPayment_MovementType_MovementTypeId",
                        column: x => x.MovementTypeId,
                        principalTable: "MovementType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RecurringPayment_PaymentMethod_CreditCardPaymentId",
                        column: x => x.CreditCardPaymentId,
                        principalTable: "PaymentMethod",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RecurringPayment_PaymentMethod_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethod",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RecurringPayment_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Movement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MovementDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    InstallmentNumber = table.Column<int>(type: "integer", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    PaymentMethodId = table.Column<int>(type: "integer", nullable: false),
                    RecurringPaymentId = table.Column<int>(type: "integer", nullable: true),
                    MovementTypeId = table.Column<int>(type: "integer", nullable: false),
                    CreditCardPaymentId = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movement_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Movement_MovementType_MovementTypeId",
                        column: x => x.MovementTypeId,
                        principalTable: "MovementType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Movement_PaymentMethod_CreditCardPaymentId",
                        column: x => x.CreditCardPaymentId,
                        principalTable: "PaymentMethod",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Movement_PaymentMethod_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethod",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Movement_RecurringPayment_RecurringPaymentId",
                        column: x => x.RecurringPaymentId,
                        principalTable: "RecurringPayment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Movement_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
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
                    { 4, "Credit Card Payment", "Pago a Tarjeta de Credito" }
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
                columns: new[] { "Id", "Email", "IsAdmin", "IsPremium", "Name", "Password", "VerifyCode" },
                values: new object[] { new Guid("95eb5d5b-dd03-4c31-8a59-80d59b73df7c"), "admin@admin.com", true, true, "Admin", "5IrYpxI+Y3DE8f6ZP6y1qw==:dR01XFPGqVy+ZUb/7gsPLGX7NkpY0dcgzPjgyUT22r8=", "" });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Description", "DescriptionSpanish", "IsDefault", "Name", "NameSpanish", "UserId" },
                values: new object[,]
                {
                    { 1, "", "", true, "Food", "Comida", new Guid("95eb5d5b-dd03-4c31-8a59-80d59b73df7c") },
                    { 2, "", "", true, "Gas", "Gasolina", new Guid("95eb5d5b-dd03-4c31-8a59-80d59b73df7c") },
                    { 3, "", "", true, "Hobbies", "Hobbies", new Guid("95eb5d5b-dd03-4c31-8a59-80d59b73df7c") },
                    { 4, "", "", true, "Shopping", "Compras", new Guid("95eb5d5b-dd03-4c31-8a59-80d59b73df7c") },
                    { 5, "", "", true, "Bank", "Bancarios", new Guid("95eb5d5b-dd03-4c31-8a59-80d59b73df7c") },
                    { 6, "", "", true, "Others", "Otros", new Guid("95eb5d5b-dd03-4c31-8a59-80d59b73df7c") }
                });

            migrationBuilder.InsertData(
                table: "PaymentMethod",
                columns: new[] { "Id", "Bank", "DefaultInterestRate", "DueDate", "IsActive", "IsCreditCard", "IsDefault", "Name", "PaymentMethodTypeId", "TransactionDate", "UserId" },
                values: new object[] { 1, "", 0m, 4, true, false, true, "Cash", 1, 15, new Guid("95eb5d5b-dd03-4c31-8a59-80d59b73df7c") });

            migrationBuilder.CreateIndex(
                name: "IX_Category_UserId",
                table: "Category",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Configuration_UserId",
                table: "Configuration",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Movement_CategoryId",
                table: "Movement",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Movement_CreditCardPaymentId",
                table: "Movement",
                column: "CreditCardPaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Movement_MovementTypeId",
                table: "Movement",
                column: "MovementTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Movement_PaymentMethodId",
                table: "Movement",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Movement_RecurringPaymentId",
                table: "Movement",
                column: "RecurringPaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Movement_UserId",
                table: "Movement",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethod_PaymentMethodTypeId",
                table: "PaymentMethod",
                column: "PaymentMethodTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethod_UserId",
                table: "PaymentMethod",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RecurringPayment_CategoryId",
                table: "RecurringPayment",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_RecurringPayment_CreditCardPaymentId",
                table: "RecurringPayment",
                column: "CreditCardPaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_RecurringPayment_FrequencyId",
                table: "RecurringPayment",
                column: "FrequencyId");

            migrationBuilder.CreateIndex(
                name: "IX_RecurringPayment_MovementTypeId",
                table: "RecurringPayment",
                column: "MovementTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RecurringPayment_PaymentMethodId",
                table: "RecurringPayment",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_RecurringPayment_UserId",
                table: "RecurringPayment",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Configuration");

            migrationBuilder.DropTable(
                name: "Movement");

            migrationBuilder.DropTable(
                name: "RecurringPayment");

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
