using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlatformaSocialBookmarking.Data.Migrations
{
    /// <inheritdoc />
    public partial class Migratie11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookmarks_Categories_CategoryId",
                table: "Bookmarks");

            migrationBuilder.DropIndex(
                name: "IX_Bookmarks_CategoryId",
                table: "Bookmarks");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Bookmarks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Bookmarks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookmarks_CategoryId",
                table: "Bookmarks",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookmarks_Categories_CategoryId",
                table: "Bookmarks",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
