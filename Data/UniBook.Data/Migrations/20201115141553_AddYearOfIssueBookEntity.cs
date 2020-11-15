using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UniBook.Data.Migrations
{
    public partial class AddYearOfIssueBookEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "YearOfIssue",
                table: "Books",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "YearOfIssue",
                table: "Books");
        }
    }
}
