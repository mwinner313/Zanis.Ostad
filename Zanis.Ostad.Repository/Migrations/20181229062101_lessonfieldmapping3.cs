using Microsoft.EntityFrameworkCore.Migrations;

namespace Zanis.Ostad.Repository.Migrations
{
    public partial class lessonfieldmapping3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessonFieldMapping_Fields_FieldId",
                table: "LessonFieldMapping");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonFieldMapping_Lessons_LessonId",
                table: "LessonFieldMapping");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LessonFieldMapping",
                table: "LessonFieldMapping");

            migrationBuilder.RenameTable(
                name: "LessonFieldMapping",
                newName: "LessonFieldMappings");

            migrationBuilder.RenameIndex(
                name: "IX_LessonFieldMapping_LessonId",
                table: "LessonFieldMappings",
                newName: "IX_LessonFieldMappings_LessonId");

            migrationBuilder.RenameIndex(
                name: "IX_LessonFieldMapping_FieldId",
                table: "LessonFieldMappings",
                newName: "IX_LessonFieldMappings_FieldId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LessonFieldMappings",
                table: "LessonFieldMappings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonFieldMappings_Fields_FieldId",
                table: "LessonFieldMappings",
                column: "FieldId",
                principalTable: "Fields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LessonFieldMappings_Lessons_LessonId",
                table: "LessonFieldMappings",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessonFieldMappings_Fields_FieldId",
                table: "LessonFieldMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonFieldMappings_Lessons_LessonId",
                table: "LessonFieldMappings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LessonFieldMappings",
                table: "LessonFieldMappings");

            migrationBuilder.RenameTable(
                name: "LessonFieldMappings",
                newName: "LessonFieldMapping");

            migrationBuilder.RenameIndex(
                name: "IX_LessonFieldMappings_LessonId",
                table: "LessonFieldMapping",
                newName: "IX_LessonFieldMapping_LessonId");

            migrationBuilder.RenameIndex(
                name: "IX_LessonFieldMappings_FieldId",
                table: "LessonFieldMapping",
                newName: "IX_LessonFieldMapping_FieldId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LessonFieldMapping",
                table: "LessonFieldMapping",
                column: "Id");

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
    }
}
