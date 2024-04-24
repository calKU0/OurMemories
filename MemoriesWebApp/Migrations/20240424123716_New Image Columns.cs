using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MemoriesWebApp.Migrations
{
    /// <inheritdoc />
    public partial class NewImageColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Images",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Date",
                table: "Images",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Images");
        }
    }
}
