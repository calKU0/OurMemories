using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MemoriesWebApp.Migrations
{
    /// <inheritdoc />
    public partial class Migration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Meetings");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Meetings",
                newName: "DateStart");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Images",
                newName: "Url");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateEnd",
                table: "Meetings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MeetingCity",
                table: "Meetings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateEnd",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "MeetingCity",
                table: "Meetings");

            migrationBuilder.RenameColumn(
                name: "DateStart",
                table: "Meetings",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "Images",
                newName: "ImageUrl");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Meetings",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
