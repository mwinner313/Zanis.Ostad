using Microsoft.EntityFrameworkCore.Migrations;

namespace Zanis.Ostad.Repository.Migrations
{
    public partial class lessonfieldmapping2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessonFieldMapping_Fields_FieldId1",
                table: "LessonFieldMapping");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonFieldMapping_Lessons_LessonId",
                table: "LessonFieldMapping");

            migrationBuilder.DropIndex(
                name: "IX_LessonFieldMapping_FieldId1",
                table: "LessonFieldMapping");

            migrationBuilder.DropColumn(
                name: "FieldId1",
                table: "LessonFieldMapping");

            migrationBuilder.AlterColumn<int>(
                name: "FieldId",
                table: "LessonFieldMapping",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.CreateIndex(
                name: "IX_LessonFieldMapping_FieldId",
                table: "LessonFieldMapping",
                column: "FieldId");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonFieldMapping_Fields_FieldId",
                table: "LessonFieldMapping",
                column: "FieldId",
                principalTable: "Fields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LessonFieldMapping_Lessons_LessonId",
                table: "LessonFieldMapping",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessonFieldMapping_Fields_FieldId",
                table: "LessonFieldMapping");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonFieldMapping_Lessons_LessonId",
                table: "LessonFieldMapping");

            migrationBuilder.DropIndex(
                name: "IX_LessonFieldMapping_FieldId",
                table: "LessonFieldMapping");

            migrationBuilder.AlterColumn<long>(
                name: "FieldId",
                table: "LessonFieldMapping",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "FieldId1",
                table: "LessonFieldMapping",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LessonFieldMapping_FieldId1",
                table: "LessonFieldMapping",
                column: "FieldId1");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonFieldMapping_Fields_FieldId1",
                table: "LessonFieldMapping",
                column: "FieldId1",
                principalTable: "Fields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LessonFieldMapping_Lessons_LessonId",
                table: "LessonFieldMapping",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
