using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlatformaSocialBookmarking.Data.Migrations
{
    /// <inheritdoc />
    public partial class Migratie7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_AspNetUsers_ApplicationUserId",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Images",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Images_ApplicationUserId",
                table: "Images",
                newName: "IX_Images_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_AspNetUsers_UserId",
                table: "Images",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_AspNetUsers_UserId",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Images",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Images_UserId",
                table: "Images",
                newName: "IX_Images_ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_AspNetUsers_ApplicationUserId",
                table: "Images",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
