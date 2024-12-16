using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TerminalApp_Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductId = table.Column<string>(type: "TEXT", nullable: false),
                    ProductName = table.Column<string>(type: "TEXT", nullable: false),
                    ProductType = table.Column<string>(type: "TEXT", nullable: false),
                    AdjustmentType = table.Column<string>(type: "TEXT", nullable: false),
                    Quantity = table.Column<string>(type: "TEXT", nullable: false),
                    SelectedUnit = table.Column<string>(type: "TEXT", nullable: false),
                    CreateTimestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateTimestamp = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductResults", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductResults");
        }
    }
}
