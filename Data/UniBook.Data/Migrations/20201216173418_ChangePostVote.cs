namespace UniBook.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ChangePostVote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TypeVote",
                table: "PostVotes",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "TypeVote",
                table: "PostVotes",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
