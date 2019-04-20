using Microsoft.EntityFrameworkCore.Migrations;

namespace Zanis.Ostad.Repository.Migrations
{
    public partial class collegepermalink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CoverPath",
                table: "Colleges",
                newName: "PermaLink");

            migrationBuilder.AddColumn<string>(
                name: "IconPath",
                table: "Colleges",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IconPath",
                table: "Colleges");

            migrationBuilder.RenameColumn(
                name: "PermaLink",
                table: "Colleges",
                newName: "CoverPath");
        }
    }
}
