using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartPocketAPI.Migrations
{
    /// <inheritdoc />
    public partial class installmentMovement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InstallmentNumber",
                table: "Movement",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InstallmentNumber",
                table: "Movement");
        }
    }
}
