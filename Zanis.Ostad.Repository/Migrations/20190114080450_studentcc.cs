using Microsoft.EntityFrameworkCore.Migrations;

namespace Zanis.Ostad.Repository.Migrations
{
    public partial class studentcc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourseMappings_Courses_StudentId",
                table: "StudentCourseMappings");

            migrationBuilder.AddColumn<long>(
                name: "CourseId1",
                table: "StudentCourseMappings",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourseMappings_CourseId",
                table: "StudentCourseMappings",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourseMappings_CourseId1",
                table: "StudentCourseMappings",
                column: "CourseId1");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourseMappings_Courses_CourseId",
                table: "StudentCourseMappings",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourseMappings_Courses_CourseId1",
                table: "StudentCourseMappings",
                column: "CourseId1",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourseMappings_Courses_CourseId",
                table: "StudentCourseMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourseMappings_Courses_CourseId1",
                table: "StudentCourseMappings");

            migrationBuilder.DropIndex(
                name: "IX_StudentCourseMappings_CourseId",
                table: "StudentCourseMappings");

            migrationBuilder.DropIndex(
                name: "IX_StudentCourseMappings_CourseId1",
                table: "StudentCourseMappings");

            migrationBuilder.DropColumn(
                name: "CourseId1",
                table: "StudentCourseMappings");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourseMappings_Courses_StudentId",
                table: "StudentCourseMappings",
                column: "StudentId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
