using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlatformaSocialBookmarking.Data.Migrations
{
    /// <inheritdoc />
    public partial class Migratie6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Bookmarks",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Bookmarks");
        }
    }
}
