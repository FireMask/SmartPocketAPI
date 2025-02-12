using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartPocketAPI.Migrations
{
    /// <inheritdoc />
    public partial class newcommit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Description", "DescriptionSpanish", "IsDefault", "Name", "NameSpanish", "UserId" },
                values: new object[] { 6, "", "", true, "Others", "Otros", new Guid("95eb5d5b-dd03-4c31-8a59-80d59b73df7c") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
