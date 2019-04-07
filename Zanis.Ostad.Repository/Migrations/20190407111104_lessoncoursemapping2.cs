using Microsoft.EntityFrameworkCore.Migrations;

namespace Zanis.Ostad.Repository.Migrations
{
    public partial class lessoncoursemapping2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contents_Courses_CourseId",
                table: "Contents");

            migrationBuilder.DropForeignKey(
                name: "FK_EditAssignment_Contents_CourseItemId",
                table: "EditAssignment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contents",
                table: "Contents");

            migrationBuilder.RenameTable(
                name: "Contents",
                newName: "CourseItems");

            migrationBuilder.RenameIndex(
                name: "IX_Contents_CourseId",
                table: "CourseItems",
                newName: "IX_CourseItems_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseItems",
                table: "CourseItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseItems_Courses_CourseId",
                table: "CourseItems",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EditAssignment_CourseItems_CourseItemId",
                table: "EditAssignment",
                column: "CourseItemId",
                principalTable: "CourseItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseItems_Courses_CourseId",
                table: "CourseItems");

            migrationBuilder.DropForeignKey(
                name: "FK_EditAssignment_CourseItems_CourseItemId",
                table: "EditAssignment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseItems",
                table: "CourseItems");

            migrationBuilder.RenameTable(
                name: "CourseItems",
                newName: "Contents");

            migrationBuilder.RenameIndex(
                name: "IX_CourseItems_CourseId",
                table: "Contents",
                newName: "IX_Contents_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contents",
                table: "Contents",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_Courses_CourseId",
                table: "Contents",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EditAssignment_Contents_CourseItemId",
                table: "EditAssignment",
                column: "CourseItemId",
                principalTable: "Contents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
