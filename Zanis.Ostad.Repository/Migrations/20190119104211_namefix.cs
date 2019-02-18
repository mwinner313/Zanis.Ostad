using Microsoft.EntityFrameworkCore.Migrations;

namespace Zanis.Ostad.Repository.Migrations
{
    public partial class namefix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CatetgoryType",
                table: "TicketCategory");

            migrationBuilder.AddColumn<int>(
                name: "CategoryType",
                table: "TicketCategory",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryType",
                table: "TicketCategory");

            migrationBuilder.AddColumn<int>(
                name: "CatetgoryType",
                table: "TicketCategory",
                nullable: false,
                defaultValue: 0);
        }
    }
}
