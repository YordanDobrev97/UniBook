using Microsoft.EntityFrameworkCore.Migrations;

namespace UniBook.Data.Migrations
{
    public partial class SpellingCorrection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReadedBook_Books_BookId",
                table: "ReadedBook");

            migrationBuilder.DropForeignKey(
                name: "FK_ReadedBook_AspNetUsers_UserId",
                table: "ReadedBook");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReadedBook",
                table: "ReadedBook");

            migrationBuilder.RenameTable(
                name: "ReadedBook",
                newName: "ReadedBooks");

            migrationBuilder.RenameIndex(
                name: "IX_ReadedBook_UserId",
                table: "ReadedBooks",
                newName: "IX_ReadedBooks_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ReadedBook_BookId",
                table: "ReadedBooks",
                newName: "IX_ReadedBooks_BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReadedBooks",
                table: "ReadedBooks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReadedBooks_Books_BookId",
                table: "ReadedBooks",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReadedBooks_AspNetUsers_UserId",
                table: "ReadedBooks",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReadedBooks_Books_BookId",
                table: "ReadedBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_ReadedBooks_AspNetUsers_UserId",
                table: "ReadedBooks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReadedBooks",
                table: "ReadedBooks");

            migrationBuilder.RenameTable(
                name: "ReadedBooks",
                newName: "ReadedBook");

            migrationBuilder.RenameIndex(
                name: "IX_ReadedBooks_UserId",
                table: "ReadedBook",
                newName: "IX_ReadedBook_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ReadedBooks_BookId",
                table: "ReadedBook",
                newName: "IX_ReadedBook_BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReadedBook",
                table: "ReadedBook",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReadedBook_Books_BookId",
                table: "ReadedBook",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReadedBook_AspNetUsers_UserId",
                table: "ReadedBook",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
