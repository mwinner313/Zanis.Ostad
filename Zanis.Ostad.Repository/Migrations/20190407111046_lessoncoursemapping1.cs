using Microsoft.EntityFrameworkCore.Migrations;

namespace Zanis.Ostad.Repository.Migrations
{
    public partial class lessoncoursemapping1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeacherLessonMappings_Courses_CourseId",
                table: "TeacherLessonMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherLessonMappings_LessonFieldMappings_LessonId",
                table: "TeacherLessonMappings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeacherLessonMappings",
                table: "TeacherLessonMappings");

            migrationBuilder.RenameTable(
                name: "TeacherLessonMappings",
                newName: "CourseLessonFieldGradeMappings");

            migrationBuilder.RenameIndex(
                name: "IX_TeacherLessonMappings_LessonId",
                table: "CourseLessonFieldGradeMappings",
                newName: "IX_CourseLessonFieldGradeMappings_LessonId");

            migrationBuilder.RenameIndex(
                name: "IX_TeacherLessonMappings_CourseId",
                table: "CourseLessonFieldGradeMappings",
                newName: "IX_CourseLessonFieldGradeMappings_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseLessonFieldGradeMappings",
                table: "CourseLessonFieldGradeMappings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseLessonFieldGradeMappings_Courses_CourseId",
                table: "CourseLessonFieldGradeMappings",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseLessonFieldGradeMappings_LessonFieldMappings_LessonId",
                table: "CourseLessonFieldGradeMappings",
                column: "LessonId",
                principalTable: "LessonFieldMappings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseLessonFieldGradeMappings_Courses_CourseId",
                table: "CourseLessonFieldGradeMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseLessonFieldGradeMappings_LessonFieldMappings_LessonId",
                table: "CourseLessonFieldGradeMappings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseLessonFieldGradeMappings",
                table: "CourseLessonFieldGradeMappings");

            migrationBuilder.RenameTable(
                name: "CourseLessonFieldGradeMappings",
                newName: "TeacherLessonMappings");

            migrationBuilder.RenameIndex(
                name: "IX_CourseLessonFieldGradeMappings_LessonId",
                table: "TeacherLessonMappings",
                newName: "IX_TeacherLessonMappings_LessonId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseLessonFieldGradeMappings_CourseId",
                table: "TeacherLessonMappings",
                newName: "IX_TeacherLessonMappings_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeacherLessonMappings",
                table: "TeacherLessonMappings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherLessonMappings_Courses_CourseId",
                table: "TeacherLessonMappings",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherLessonMappings_LessonFieldMappings_LessonId",
                table: "TeacherLessonMappings",
                column: "LessonId",
                principalTable: "LessonFieldMappings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
