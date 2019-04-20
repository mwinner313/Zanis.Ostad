using Microsoft.EntityFrameworkCore.Migrations;

namespace Zanis.Ostad.Repository.Migrations
{
    public partial class peramlinks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PermaLink",
                table: "Grades",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PermaLink",
                table: "Fields",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PermaLink",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "PermaLink",
                table: "Fields");
        }
    }
}
