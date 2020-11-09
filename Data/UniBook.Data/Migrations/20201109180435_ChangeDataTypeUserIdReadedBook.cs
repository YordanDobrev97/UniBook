using Microsoft.EntityFrameworkCore.Migrations;

namespace UniBook.Data.Migrations
{
    public partial class ChangeDataTypeUserIdReadedBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReadedBook_AspNetUsers_UserId1",
                table: "ReadedBook");

            migrationBuilder.DropIndex(
                name: "IX_ReadedBook_UserId1",
                table: "ReadedBook");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "ReadedBook");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ReadedBook",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_ReadedBook_UserId",
                table: "ReadedBook",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReadedBook_AspNetUsers_UserId",
                table: "ReadedBook",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReadedBook_AspNetUsers_UserId",
                table: "ReadedBook");

            migrationBuilder.DropIndex(
                name: "IX_ReadedBook_UserId",
                table: "ReadedBook");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "ReadedBook",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "ReadedBook",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReadedBook_UserId1",
                table: "ReadedBook",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ReadedBook_AspNetUsers_UserId1",
                table: "ReadedBook",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
