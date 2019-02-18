using Microsoft.EntityFrameworkCore.Migrations;

namespace Zanis.Ostad.Repository.Migrations
{
    public partial class ticketedit3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UnReadedMessagesCount",
                table: "Tickets",
                newName: "TicketOwnerUnReadedMessagesCount");

            migrationBuilder.AddColumn<int>(
                name: "OperatorUnReadedMessagesCount",
                table: "Tickets",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OperatorUnReadedMessagesCount",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "TicketOwnerUnReadedMessagesCount",
                table: "Tickets",
                newName: "UnReadedMessagesCount");
        }
    }
}
