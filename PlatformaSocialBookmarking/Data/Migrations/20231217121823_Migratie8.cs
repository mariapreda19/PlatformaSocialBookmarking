using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlatformaSocialBookmarking.Data.Migrations
{
    /// <inheritdoc />
    public partial class Migratie8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookmark_has_Category_Bookmarks_BookmarkId",
                table: "Bookmark_has_Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookmark_has_Category_Categories_CategoryId",
                table: "Bookmark_has_Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Bookmarks_BookmarkId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_BookmarkId",
                table: "Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bookmark_has_Category",
                table: "Bookmark_has_Category");

            migrationBuilder.DropColumn(
                name: "BookmarkId",
                table: "Images");

            migrationBuilder.RenameTable(
                name: "Bookmark_has_Category",
                newName: "Bookmark_Has_Category");

            migrationBuilder.RenameIndex(
                name: "IX_Bookmark_has_Category_CategoryId",
                table: "Bookmark_Has_Category",
                newName: "IX_Bookmark_Has_Category_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookmark_has_Category_BookmarkId",
                table: "Bookmark_Has_Category",
                newName: "IX_Bookmark_Has_Category_BookmarkId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bookmark_Has_Category",
                table: "Bookmark_Has_Category",
                columns: new[] { "Id", "BookmarkId", "CategoryId" });

            migrationBuilder.CreateTable(
                name: "Bookmark_Has_Image",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageId = table.Column<int>(type: "int", nullable: false),
                    BookmarkId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookmark_Has_Image", x => new { x.Id, x.BookmarkId, x.ImageId });
                    table.ForeignKey(
                        name: "FK_Bookmark_Has_Image_Bookmarks_BookmarkId",
                        column: x => x.BookmarkId,
                        principalTable: "Bookmarks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookmark_Has_Image_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookmark_Has_Image_BookmarkId",
                table: "Bookmark_Has_Image",
                column: "BookmarkId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookmark_Has_Image_ImageId",
                table: "Bookmark_Has_Image",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookmark_Has_Category_Bookmarks_BookmarkId",
                table: "Bookmark_Has_Category",
                column: "BookmarkId",
                principalTable: "Bookmarks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookmark_Has_Category_Categories_CategoryId",
                table: "Bookmark_Has_Category",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookmark_Has_Category_Bookmarks_BookmarkId",
                table: "Bookmark_Has_Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookmark_Has_Category_Categories_CategoryId",
                table: "Bookmark_Has_Category");

            migrationBuilder.DropTable(
                name: "Bookmark_Has_Image");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bookmark_Has_Category",
                table: "Bookmark_Has_Category");

            migrationBuilder.RenameTable(
                name: "Bookmark_Has_Category",
                newName: "Bookmark_has_Category");

            migrationBuilder.RenameIndex(
                name: "IX_Bookmark_Has_Category_CategoryId",
                table: "Bookmark_has_Category",
                newName: "IX_Bookmark_has_Category_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookmark_Has_Category_BookmarkId",
                table: "Bookmark_has_Category",
                newName: "IX_Bookmark_has_Category_BookmarkId");

            migrationBuilder.AddColumn<int>(
                name: "BookmarkId",
                table: "Images",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bookmark_has_Category",
                table: "Bookmark_has_Category",
                columns: new[] { "Id", "BookmarkId", "CategoryId" });

            migrationBuilder.CreateIndex(
                name: "IX_Images_BookmarkId",
                table: "Images",
                column: "BookmarkId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookmark_has_Category_Bookmarks_BookmarkId",
                table: "Bookmark_has_Category",
                column: "BookmarkId",
                principalTable: "Bookmarks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookmark_has_Category_Categories_CategoryId",
                table: "Bookmark_has_Category",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Bookmarks_BookmarkId",
                table: "Images",
                column: "BookmarkId",
                principalTable: "Bookmarks",
                principalColumn: "Id");
        }
    }
}
