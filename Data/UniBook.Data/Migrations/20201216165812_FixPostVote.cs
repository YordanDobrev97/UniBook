namespace UniBook.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class FixPostVote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountNegative",
                table: "PostVotes");

            migrationBuilder.DropColumn(
                name: "CountPositive",
                table: "PostVotes");

            migrationBuilder.AddColumn<bool>(
                name: "TypeVote",
                table: "PostVotes",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeVote",
                table: "PostVotes");

            migrationBuilder.AddColumn<int>(
                name: "CountNegative",
                table: "PostVotes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CountPositive",
                table: "PostVotes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
