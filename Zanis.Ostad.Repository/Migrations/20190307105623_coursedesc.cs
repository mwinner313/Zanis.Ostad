using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Zanis.Ostad.Repository.Migrations
{
    public partial class coursedesc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Courses");

            migrationBuilder.AddColumn<string>(
                name: "AdminMessageForTeacher",
                table: "Courses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TeacherMessageForAdmin",
                table: "Courses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdminMessageForTeacher",
                table: "Contents",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TeacherMessageForAdmin",
                table: "Contents",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RelatedItemType = table.Column<int>(nullable: false),
                    JsonExtraData = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    ReceiverId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_AspNetUsers_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_ReceiverId",
                table: "Notifications",
                column: "ReceiverId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropColumn(
                name: "AdminMessageForTeacher",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "TeacherMessageForAdmin",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "AdminMessageForTeacher",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "TeacherMessageForAdmin",
                table: "Contents");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Courses",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
