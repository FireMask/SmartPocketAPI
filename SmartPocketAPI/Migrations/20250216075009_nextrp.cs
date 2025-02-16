using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartPocketAPI.Migrations
{
    /// <inheritdoc />
    public partial class nextrp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movement_RecurringPayments_RecurringPaymentId",
                table: "Movement");

            migrationBuilder.DropForeignKey(
                name: "FK_RecurringPayments_Frequency_FrecuencyId",
                table: "RecurringPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_RecurringPayments_User_UserId",
                table: "RecurringPayments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecurringPayments",
                table: "RecurringPayments");

            migrationBuilder.RenameTable(
                name: "RecurringPayments",
                newName: "RecurringPayment");

            migrationBuilder.RenameIndex(
                name: "IX_RecurringPayments_UserId",
                table: "RecurringPayment",
                newName: "IX_RecurringPayment_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_RecurringPayments_FrecuencyId",
                table: "RecurringPayment",
                newName: "IX_RecurringPayment_FrecuencyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecurringPayment",
                table: "RecurringPayment",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_RecurringPayment_CategoryId",
                table: "RecurringPayment",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_RecurringPayment_CreditCardPaymentId",
                table: "RecurringPayment",
                column: "CreditCardPaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_RecurringPayment_MovementTypeId",
                table: "RecurringPayment",
                column: "MovementTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RecurringPayment_PaymentMethodId",
                table: "RecurringPayment",
                column: "PaymentMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movement_RecurringPayment_RecurringPaymentId",
                table: "Movement",
                column: "RecurringPaymentId",
                principalTable: "RecurringPayment",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecurringPayment_Category_CategoryId",
                table: "RecurringPayment",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecurringPayment_Frequency_FrecuencyId",
                table: "RecurringPayment",
                column: "FrecuencyId",
                principalTable: "Frequency",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecurringPayment_MovementType_MovementTypeId",
                table: "RecurringPayment",
                column: "MovementTypeId",
                principalTable: "MovementType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecurringPayment_PaymentMethod_CreditCardPaymentId",
                table: "RecurringPayment",
                column: "CreditCardPaymentId",
                principalTable: "PaymentMethod",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecurringPayment_PaymentMethod_PaymentMethodId",
                table: "RecurringPayment",
                column: "PaymentMethodId",
                principalTable: "PaymentMethod",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecurringPayment_User_UserId",
                table: "RecurringPayment",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movement_RecurringPayment_RecurringPaymentId",
                table: "Movement");

            migrationBuilder.DropForeignKey(
                name: "FK_RecurringPayment_Category_CategoryId",
                table: "RecurringPayment");

            migrationBuilder.DropForeignKey(
                name: "FK_RecurringPayment_Frequency_FrecuencyId",
                table: "RecurringPayment");

            migrationBuilder.DropForeignKey(
                name: "FK_RecurringPayment_MovementType_MovementTypeId",
                table: "RecurringPayment");

            migrationBuilder.DropForeignKey(
                name: "FK_RecurringPayment_PaymentMethod_CreditCardPaymentId",
                table: "RecurringPayment");

            migrationBuilder.DropForeignKey(
                name: "FK_RecurringPayment_PaymentMethod_PaymentMethodId",
                table: "RecurringPayment");

            migrationBuilder.DropForeignKey(
                name: "FK_RecurringPayment_User_UserId",
                table: "RecurringPayment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecurringPayment",
                table: "RecurringPayment");

            migrationBuilder.DropIndex(
                name: "IX_RecurringPayment_CategoryId",
                table: "RecurringPayment");

            migrationBuilder.DropIndex(
                name: "IX_RecurringPayment_CreditCardPaymentId",
                table: "RecurringPayment");

            migrationBuilder.DropIndex(
                name: "IX_RecurringPayment_MovementTypeId",
                table: "RecurringPayment");

            migrationBuilder.DropIndex(
                name: "IX_RecurringPayment_PaymentMethodId",
                table: "RecurringPayment");

            migrationBuilder.RenameTable(
                name: "RecurringPayment",
                newName: "RecurringPayments");

            migrationBuilder.RenameIndex(
                name: "IX_RecurringPayment_UserId",
                table: "RecurringPayments",
                newName: "IX_RecurringPayments_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_RecurringPayment_FrecuencyId",
                table: "RecurringPayments",
                newName: "IX_RecurringPayments_FrecuencyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecurringPayments",
                table: "RecurringPayments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Movement_RecurringPayments_RecurringPaymentId",
                table: "Movement",
                column: "RecurringPaymentId",
                principalTable: "RecurringPayments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecurringPayments_Frequency_FrecuencyId",
                table: "RecurringPayments",
                column: "FrecuencyId",
                principalTable: "Frequency",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecurringPayments_User_UserId",
                table: "RecurringPayments",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
