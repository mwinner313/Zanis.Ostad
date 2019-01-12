using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Zanis.Ostad.Repository.Migrations
{
    public partial class ticket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Desicription",
                table: "Courses",
                newName: "Description");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Tickets",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "CourseId",
                table: "Tickets",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Tickets",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Tickets",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TicketReason",
                table: "Tickets",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "TicketItems",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "TicketItems",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "TicketItems",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "AppVersions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsRequired = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppVersions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TicketCategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    IsDeletible = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppVersionFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    VersionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppVersionFeatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppVersionFeatures_AppVersions_VersionId",
                        column: x => x.VersionId,
                        principalTable: "AppVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_CategoryId",
                table: "Tickets",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_CourseId",
                table: "Tickets",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketItems_UserId",
                table: "TicketItems",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppVersionFeatures_VersionId",
                table: "AppVersionFeatures",
                column: "VersionId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketItems_AspNetUsers_UserId",
                table: "TicketItems",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_TicketCategory_CategoryId",
                table: "Tickets",
                column: "CategoryId",
                principalTable: "TicketCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Courses_CourseId",
                table: "Tickets",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketItems_AspNetUsers_UserId",
                table: "TicketItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_TicketCategory_CategoryId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Courses_CourseId",
                table: "Tickets");

            migrationBuilder.DropTable(
                name: "AppVersionFeatures");

            migrationBuilder.DropTable(
                name: "TicketCategory");

            migrationBuilder.DropTable(
                name: "AppVersions");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_CategoryId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_CourseId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_TicketItems_UserId",
                table: "TicketItems");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "TicketReason",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "TicketItems");

            migrationBuilder.DropColumn(
                name: "Message",
                table: "TicketItems");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TicketItems");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Courses",
                newName: "Desicription");
        }
    }
}
