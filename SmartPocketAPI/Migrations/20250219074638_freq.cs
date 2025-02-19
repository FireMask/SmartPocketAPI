using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartPocketAPI.Migrations
{
    /// <inheritdoc />
    public partial class freq : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecurringPayment_Frequency_FrecuencyId",
                table: "RecurringPayment");

            migrationBuilder.RenameColumn(
                name: "FrecuencyId",
                table: "RecurringPayment",
                newName: "FrequencyId");

            migrationBuilder.RenameIndex(
                name: "IX_RecurringPayment_FrecuencyId",
                table: "RecurringPayment",
                newName: "IX_RecurringPayment_FrequencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecurringPayment_Frequency_FrequencyId",
                table: "RecurringPayment",
                column: "FrequencyId",
                principalTable: "Frequency",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecurringPayment_Frequency_FrequencyId",
                table: "RecurringPayment");

            migrationBuilder.RenameColumn(
                name: "FrequencyId",
                table: "RecurringPayment",
                newName: "FrecuencyId");

            migrationBuilder.RenameIndex(
                name: "IX_RecurringPayment_FrequencyId",
                table: "RecurringPayment",
                newName: "IX_RecurringPayment_FrecuencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecurringPayment_Frequency_FrecuencyId",
                table: "RecurringPayment",
                column: "FrecuencyId",
                principalTable: "Frequency",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
