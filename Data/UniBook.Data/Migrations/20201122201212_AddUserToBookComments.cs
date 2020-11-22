using Microsoft.EntityFrameworkCore.Migrations;

namespace UniBook.Data.Migrations
{
    public partial class AddUserToBookComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "BookComments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookComments_UserId",
                table: "BookComments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookComments_AspNetUsers_UserId",
                table: "BookComments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookComments_AspNetUsers_UserId",
                table: "BookComments");

            migrationBuilder.DropIndex(
                name: "IX_BookComments_UserId",
                table: "BookComments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BookComments");
        }
    }
}
