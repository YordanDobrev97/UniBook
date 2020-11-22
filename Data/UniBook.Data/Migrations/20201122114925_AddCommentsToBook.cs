using Microsoft.EntityFrameworkCore.Migrations;

namespace UniBook.Data.Migrations
{
    public partial class AddCommentsToBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookComment_Books_BookId",
                table: "BookComment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookComment",
                table: "BookComment");

            migrationBuilder.RenameTable(
                name: "BookComment",
                newName: "BookComments");

            migrationBuilder.RenameIndex(
                name: "IX_BookComment_BookId",
                table: "BookComments",
                newName: "IX_BookComments_BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookComments",
                table: "BookComments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookComments_Books_BookId",
                table: "BookComments",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookComments_Books_BookId",
                table: "BookComments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookComments",
                table: "BookComments");

            migrationBuilder.RenameTable(
                name: "BookComments",
                newName: "BookComment");

            migrationBuilder.RenameIndex(
                name: "IX_BookComments_BookId",
                table: "BookComment",
                newName: "IX_BookComment_BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookComment",
                table: "BookComment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookComment_Books_BookId",
                table: "BookComment",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
