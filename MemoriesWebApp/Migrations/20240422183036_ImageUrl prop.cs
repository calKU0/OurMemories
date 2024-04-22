using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MemoriesWebApp.Migrations
{
    /// <inheritdoc />
    public partial class ImageUrlprop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Meetings",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Meetings");
        }
    }
}
