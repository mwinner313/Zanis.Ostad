using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Zanis.Ostad.Repository.Migrations
{
    public partial class lessonfieldmapping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LessonFieldMapping",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LessonId = table.Column<long>(nullable: false),
                    FieldId1 = table.Column<int>(nullable: true),
                    FieldId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonFieldMapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonFieldMapping_Fields_FieldId1",
                        column: x => x.FieldId1,
                        principalTable: "Fields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LessonFieldMapping_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LessonFieldMapping_FieldId1",
                table: "LessonFieldMapping",
                column: "FieldId1");

            migrationBuilder.CreateIndex(
                name: "IX_LessonFieldMapping_LessonId",
                table: "LessonFieldMapping",
                column: "LessonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LessonFieldMapping");
        }
    }
}
