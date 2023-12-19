using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlatformaSocialBookmarking.Data.Migrations
{
    /// <inheritdoc />
    public partial class Migratie18 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Comments_BookmarkId",
                table: "Comments");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_BookmarkId",
                table: "Comments",
                column: "BookmarkId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Comments_BookmarkId",
                table: "Comments");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_BookmarkId",
                table: "Comments",
                column: "BookmarkId",
                unique: true,
                filter: "[BookmarkId] IS NOT NULL");
        }
    }
}
