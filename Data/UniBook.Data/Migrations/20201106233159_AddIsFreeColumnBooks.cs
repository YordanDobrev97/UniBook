using Microsoft.EntityFrameworkCore.Migrations;

namespace UniBook.Data.Migrations
{
    public partial class AddIsFreeColumnBooks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFree",
                table: "Books",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFree",
                table: "Books");

            migrationBuilder.AddColumn<bool>(
                name: "IsStartRead",
                table: "Books",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
