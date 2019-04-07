using Microsoft.EntityFrameworkCore.Migrations;

namespace Zanis.Ostad.Repository.Migrations
{
    public partial class lessoncoursemapping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_TeacherLessonMappings_TeacherLessonMappingId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherLessonMappings_AspNetUsers_TeacherId",
                table: "TeacherLessonMappings");

            migrationBuilder.RenameColumn(
                name: "TeacherId",
                table: "TeacherLessonMappings",
                newName: "CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_TeacherLessonMappings_TeacherId",
                table: "TeacherLessonMappings",
                newName: "IX_TeacherLessonMappings_CourseId");

            migrationBuilder.RenameColumn(
                name: "TeacherLessonMappingId",
                table: "Courses",
                newName: "TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_TeacherLessonMappingId",
                table: "Courses",
                newName: "IX_Courses_TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_AspNetUsers_TeacherId",
                table: "Courses",
                column: "TeacherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherLessonMappings_Courses_CourseId",
                table: "TeacherLessonMappings",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_AspNetUsers_TeacherId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherLessonMappings_Courses_CourseId",
                table: "TeacherLessonMappings");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "TeacherLessonMappings",
                newName: "TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_TeacherLessonMappings_CourseId",
                table: "TeacherLessonMappings",
                newName: "IX_TeacherLessonMappings_TeacherId");

            migrationBuilder.RenameColumn(
                name: "TeacherId",
                table: "Courses",
                newName: "TeacherLessonMappingId");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_TeacherId",
                table: "Courses",
                newName: "IX_Courses_TeacherLessonMappingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_TeacherLessonMappings_TeacherLessonMappingId",
                table: "Courses",
                column: "TeacherLessonMappingId",
                principalTable: "TeacherLessonMappings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherLessonMappings_AspNetUsers_TeacherId",
                table: "TeacherLessonMappings",
                column: "TeacherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
