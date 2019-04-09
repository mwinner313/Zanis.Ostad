using Microsoft.EntityFrameworkCore.Migrations;

namespace Zanis.Ostad.Repository.Migrations
{
    public partial class coursetitlechangedtocoursecategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Courses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Courses");
        }
    }
}
