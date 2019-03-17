using Microsoft.EntityFrameworkCore.Migrations;

namespace Zanis.Ostad.Repository.Migrations
{
    public partial class qqq2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "EditAssignment",
                newName: "CreatedOn");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "EditAssignment",
                newName: "DateTime");
        }
    }
}
