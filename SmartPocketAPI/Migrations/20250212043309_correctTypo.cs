using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartPocketAPI.Migrations
{
    /// <inheritdoc />
    public partial class correctTypo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movement_Category_CategoriId",
                table: "Movement");

            migrationBuilder.RenameColumn(
                name: "CategoriId",
                table: "Movement",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Movement_CategoriId",
                table: "Movement",
                newName: "IX_Movement_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movement_Category_CategoryId",
                table: "Movement",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movement_Category_CategoryId",
                table: "Movement");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Movement",
                newName: "CategoriId");

            migrationBuilder.RenameIndex(
                name: "IX_Movement_CategoryId",
                table: "Movement",
                newName: "IX_Movement_CategoriId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movement_Category_CategoriId",
                table: "Movement",
                column: "CategoriId",
                principalTable: "Category",
                principalColumn: "Id");
        }
    }
}
