using Microsoft.EntityFrameworkCore.Migrations;

namespace Zanis.Ostad.Repository.Migrations
{
    public partial class studentc2c : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourseMappings_Courses_StudentId",
                table: "StudentCourseMappings");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourseMappings_CourseId",
                table: "StudentCourseMappings",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourseMappings_Courses_CourseId",
                table: "StudentCourseMappings",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourseMappings_Courses_CourseId",
                table: "StudentCourseMappings");

            migrationBuilder.DropIndex(
                name: "IX_StudentCourseMappings_CourseId",
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
