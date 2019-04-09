using Microsoft.EntityFrameworkCore.Migrations;

namespace Zanis.Ostad.Repository.Migrations
{
    public partial class coursetitlechangedtocoursecategory1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseTitles_CourseTitleId",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "CourseTitleId",
                table: "Courses",
                newName: "CourseCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_CourseTitleId",
                table: "Courses",
                newName: "IX_Courses_CourseCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CourseTitles_CourseCategoryId",
                table: "Courses",
                column: "CourseCategoryId",
                principalTable: "CourseTitles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseTitles_CourseCategoryId",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "CourseCategoryId",
                table: "Courses",
                newName: "CourseTitleId");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_CourseCategoryId",
                table: "Courses",
                newName: "IX_Courses_CourseTitleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CourseTitles_CourseTitleId",
                table: "Courses",
                column: "CourseTitleId",
                principalTable: "CourseTitles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
