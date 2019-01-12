using Microsoft.EntityFrameworkCore.Migrations;

namespace Zanis.Ostad.Repository.Migrations
{
    public partial class rrr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Fields_FieldId",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherLessonMappings_Lessons_LessonId",
                table: "TeacherLessonMappings");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_FieldId",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "FieldId",
                table: "Lessons");

            migrationBuilder.AddColumn<int>(
                name: "GradeId",
                table: "LessonFieldMappings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LessonFieldMappings_GradeId",
                table: "LessonFieldMappings",
                column: "GradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonFieldMappings_Grades_GradeId",
                table: "LessonFieldMappings",
                column: "GradeId",
                principalTable: "Grades",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessonFieldMappings_Grades_GradeId",
                table: "LessonFieldMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherLessonMappings_LessonFieldMappings_LessonId",
                table: "TeacherLessonMappings");

            migrationBuilder.DropIndex(
                name: "IX_LessonFieldMappings_GradeId",
                table: "LessonFieldMappings");

            migrationBuilder.DropColumn(
                name: "GradeId",
                table: "LessonFieldMappings");

            migrationBuilder.AddColumn<int>(
                name: "FieldId",
                table: "Lessons",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_FieldId",
                table: "Lessons",
                column: "FieldId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Fields_FieldId",
                table: "Lessons",
                column: "FieldId",
                principalTable: "Fields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherLessonMappings_Lessons_LessonId",
                table: "TeacherLessonMappings",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
