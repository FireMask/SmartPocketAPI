using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartPocketAPI.Migrations
{
    /// <inheritdoc />
    public partial class newcommit2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MovementType",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Credit Card Payment");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MovementType",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "CreditCard Payment");
        }
    }
}
