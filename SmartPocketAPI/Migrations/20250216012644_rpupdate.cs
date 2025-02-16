using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartPocketAPI.Migrations
{
    /// <inheritdoc />
    public partial class rpupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "RecurringPayments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreditCardPaymentId",
                table: "RecurringPayments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CurrentInstallmentCount",
                table: "RecurringPayments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "InstallmentAmountPerPeriod",
                table: "RecurringPayments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "MovementTypeId",
                table: "RecurringPayments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PaymentMethodId",
                table: "RecurringPayments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "RecurringPayments");

            migrationBuilder.DropColumn(
                name: "CreditCardPaymentId",
                table: "RecurringPayments");

            migrationBuilder.DropColumn(
                name: "CurrentInstallmentCount",
                table: "RecurringPayments");

            migrationBuilder.DropColumn(
                name: "InstallmentAmountPerPeriod",
                table: "RecurringPayments");

            migrationBuilder.DropColumn(
                name: "MovementTypeId",
                table: "RecurringPayments");

            migrationBuilder.DropColumn(
                name: "PaymentMethodId",
                table: "RecurringPayments");
        }
    }
}
