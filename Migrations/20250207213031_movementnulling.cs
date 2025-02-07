using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartPocketAPI.Migrations
{
    /// <inheritdoc />
    public partial class movementnulling : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movement_PaymentMethod_CreditCardPaymentId",
                table: "Movement");

            migrationBuilder.DropForeignKey(
                name: "FK_Movement_RecurringPayments_RecurringPaymentId",
                table: "Movement");

            migrationBuilder.AlterColumn<int>(
                name: "RecurringPaymentId",
                table: "Movement",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "CreditCardPaymentId",
                table: "Movement",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Movement_PaymentMethod_CreditCardPaymentId",
                table: "Movement",
                column: "CreditCardPaymentId",
                principalTable: "PaymentMethod",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Movement_RecurringPayments_RecurringPaymentId",
                table: "Movement",
                column: "RecurringPaymentId",
                principalTable: "RecurringPayments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movement_PaymentMethod_CreditCardPaymentId",
                table: "Movement");

            migrationBuilder.DropForeignKey(
                name: "FK_Movement_RecurringPayments_RecurringPaymentId",
                table: "Movement");

            migrationBuilder.AlterColumn<int>(
                name: "RecurringPaymentId",
                table: "Movement",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreditCardPaymentId",
                table: "Movement",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Movement_PaymentMethod_CreditCardPaymentId",
                table: "Movement",
                column: "CreditCardPaymentId",
                principalTable: "PaymentMethod",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Movement_RecurringPayments_RecurringPaymentId",
                table: "Movement",
                column: "RecurringPaymentId",
                principalTable: "RecurringPayments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
