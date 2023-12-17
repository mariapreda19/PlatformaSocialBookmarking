using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlatformaSocialBookmarking.Data.Migrations
{
    /// <inheritdoc />
    public partial class Migratie9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookmark_Has_Category_Bookmarks_BookmarkId",
                table: "Bookmark_Has_Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookmark_Has_Category_Categories_CategoryId",
                table: "Bookmark_Has_Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookmark_Has_Image_Bookmarks_BookmarkId",
                table: "Bookmark_Has_Image");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookmark_Has_Image_Images_ImageId",
                table: "Bookmark_Has_Image");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bookmark_Has_Image",
                table: "Bookmark_Has_Image");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bookmark_Has_Category",
                table: "Bookmark_Has_Category");

            migrationBuilder.RenameTable(
                name: "Bookmark_Has_Image",
                newName: "Bookmark_Has_Images");

            migrationBuilder.RenameTable(
                name: "Bookmark_Has_Category",
                newName: "Bookmark_Has_Categories");

            migrationBuilder.RenameIndex(
                name: "IX_Bookmark_Has_Image_ImageId",
                table: "Bookmark_Has_Images",
                newName: "IX_Bookmark_Has_Images_ImageId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookmark_Has_Image_BookmarkId",
                table: "Bookmark_Has_Images",
                newName: "IX_Bookmark_Has_Images_BookmarkId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookmark_Has_Category_CategoryId",
                table: "Bookmark_Has_Categories",
                newName: "IX_Bookmark_Has_Categories_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookmark_Has_Category_BookmarkId",
                table: "Bookmark_Has_Categories",
                newName: "IX_Bookmark_Has_Categories_BookmarkId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bookmark_Has_Images",
                table: "Bookmark_Has_Images",
                columns: new[] { "Id", "BookmarkId", "ImageId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bookmark_Has_Categories",
                table: "Bookmark_Has_Categories",
                columns: new[] { "Id", "BookmarkId", "CategoryId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Bookmark_Has_Categories_Bookmarks_BookmarkId",
                table: "Bookmark_Has_Categories",
                column: "BookmarkId",
                principalTable: "Bookmarks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookmark_Has_Categories_Categories_CategoryId",
                table: "Bookmark_Has_Categories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookmark_Has_Images_Bookmarks_BookmarkId",
                table: "Bookmark_Has_Images",
                column: "BookmarkId",
                principalTable: "Bookmarks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookmark_Has_Images_Images_ImageId",
                table: "Bookmark_Has_Images",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookmark_Has_Categories_Bookmarks_BookmarkId",
                table: "Bookmark_Has_Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookmark_Has_Categories_Categories_CategoryId",
                table: "Bookmark_Has_Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookmark_Has_Images_Bookmarks_BookmarkId",
                table: "Bookmark_Has_Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookmark_Has_Images_Images_ImageId",
                table: "Bookmark_Has_Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bookmark_Has_Images",
                table: "Bookmark_Has_Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bookmark_Has_Categories",
                table: "Bookmark_Has_Categories");

            migrationBuilder.RenameTable(
                name: "Bookmark_Has_Images",
                newName: "Bookmark_Has_Image");

            migrationBuilder.RenameTable(
                name: "Bookmark_Has_Categories",
                newName: "Bookmark_Has_Category");

            migrationBuilder.RenameIndex(
                name: "IX_Bookmark_Has_Images_ImageId",
                table: "Bookmark_Has_Image",
                newName: "IX_Bookmark_Has_Image_ImageId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookmark_Has_Images_BookmarkId",
                table: "Bookmark_Has_Image",
                newName: "IX_Bookmark_Has_Image_BookmarkId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookmark_Has_Categories_CategoryId",
                table: "Bookmark_Has_Category",
                newName: "IX_Bookmark_Has_Category_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookmark_Has_Categories_BookmarkId",
                table: "Bookmark_Has_Category",
                newName: "IX_Bookmark_Has_Category_BookmarkId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bookmark_Has_Image",
                table: "Bookmark_Has_Image",
                columns: new[] { "Id", "BookmarkId", "ImageId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bookmark_Has_Category",
                table: "Bookmark_Has_Category",
                columns: new[] { "Id", "BookmarkId", "CategoryId" });

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

            migrationBuilder.AddForeignKey(
                name: "FK_Bookmark_Has_Image_Bookmarks_BookmarkId",
                table: "Bookmark_Has_Image",
                column: "BookmarkId",
                principalTable: "Bookmarks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookmark_Has_Image_Images_ImageId",
                table: "Bookmark_Has_Image",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
