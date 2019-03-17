using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Zanis.Ostad.Repository.Migrations
{
    public partial class qqq : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LatestEditStatus",
                table: "Contents",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EditAssignment",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EditorId = table.Column<long>(nullable: false),
                    CourseItemId = table.Column<long>(nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    FilePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EditAssignment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EditAssignment_Contents_CourseItemId",
                        column: x => x.CourseItemId,
                        principalTable: "Contents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EditAssignment_AspNetUsers_EditorId",
                        column: x => x.EditorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EditAssignment_CourseItemId",
                table: "EditAssignment",
                column: "CourseItemId");

            migrationBuilder.CreateIndex(
                name: "IX_EditAssignment_EditorId",
                table: "EditAssignment",
                column: "EditorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EditAssignment");

            migrationBuilder.DropColumn(
                name: "LatestEditStatus",
                table: "Contents");
        }
    }
}
