using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlatformaSocialBookmarking.Data.Migrations
{
    /// <inheritdoc />
    public partial class ProiectMig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Images_ImageId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_ImageId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Comments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Comments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ImageId",
                table: "Comments",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Images_ImageId",
                table: "Comments",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id");
        }
    }
}
