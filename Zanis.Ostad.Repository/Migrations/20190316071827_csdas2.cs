using Microsoft.EntityFrameworkCore.Migrations;

namespace Zanis.Ostad.Repository.Migrations
{
    public partial class csdas2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HasPendingToApproveItem",
                table: "Courses",
                newName: "HasPendingItemToApprove");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HasPendingItemToApprove",
                table: "Courses",
                newName: "HasPendingToApproveItem");
        }
    }
}
