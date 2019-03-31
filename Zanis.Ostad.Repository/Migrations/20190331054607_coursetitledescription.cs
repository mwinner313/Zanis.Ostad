using Microsoft.EntityFrameworkCore.Migrations;

namespace Zanis.Ostad.Repository.Migrations
{
    public partial class coursetitledescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CourseTitles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "CourseTitles");
        }
    }
}
