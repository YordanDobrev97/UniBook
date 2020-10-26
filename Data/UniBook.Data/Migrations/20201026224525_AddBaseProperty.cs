using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UniBook.Data.Migrations
{
    public partial class AddBaseProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "UserBooks",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "UserBooks",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "UserBooks",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "UserBooks",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserBooks_IsDeleted",
                table: "UserBooks",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserBooks_IsDeleted",
                table: "UserBooks");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "UserBooks");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "UserBooks");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "UserBooks");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "UserBooks");
        }
    }
}
