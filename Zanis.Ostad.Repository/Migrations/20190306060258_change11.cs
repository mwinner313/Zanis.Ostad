using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Zanis.Ostad.Repository.Migrations
{
    public partial class change11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApprovalStatus",
                table: "Courses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Courses",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "PendingToApproveItemsCount",
                table: "Courses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ZipFilesPath",
                table: "Courses",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Contents",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Contents",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovalStatus",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "PendingToApproveItemsCount",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "ZipFilesPath",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Contents");
        }
    }
}
