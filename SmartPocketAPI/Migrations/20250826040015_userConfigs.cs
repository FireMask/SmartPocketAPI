using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartPocketAPI.Migrations
{
    /// <inheritdoc />
    public partial class userConfigs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Configuration",
                newName: "DefaultValue");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Configuration",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.CreateTable(
                name: "UserConfiguration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<string>(type: "text", nullable: false),
                    ConfigurationId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserConfiguration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserConfiguration_Configuration_ConfigurationId",
                        column: x => x.ConfigurationId,
                        principalTable: "Configuration",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserConfiguration_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Configuration",
                columns: new[] { "Id", "DefaultValue", "Key", "UserId" },
                values: new object[,]
                {
                    { 1, "false", "DarkMode", null },
                    { 2, "10", "ItemsPerPage", null },
                    { 3, "1", "DefaultPaymentMethod", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserConfiguration_ConfigurationId",
                table: "UserConfiguration",
                column: "ConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserConfiguration_UserId",
                table: "UserConfiguration",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserConfiguration");

            migrationBuilder.DeleteData(
                table: "Configuration",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Configuration",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Configuration",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.RenameColumn(
                name: "DefaultValue",
                table: "Configuration",
                newName: "Value");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Configuration",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);
        }
    }
}
