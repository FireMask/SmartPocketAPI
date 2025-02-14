using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartPocketAPI.Migrations
{
    /// <inheritdoc />
    public partial class recurrentDesc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "RecurringPayments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "RecurringPayments");
        }
    }
}
