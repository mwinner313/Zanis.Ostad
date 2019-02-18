using Microsoft.EntityFrameworkCore.Migrations;

namespace Zanis.Ostad.Repository.Migrations
{
    public partial class ticketinfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastMessageText",
                table: "Tickets",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnReadedMessagesCount",
                table: "Tickets",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsSeen",
                table: "TicketItems",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastMessageText",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "UnReadedMessagesCount",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "IsSeen",
                table: "TicketItems");
        }
    }
}
