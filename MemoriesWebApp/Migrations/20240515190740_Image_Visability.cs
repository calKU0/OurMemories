using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MemoriesWebApp.Migrations
{
    /// <inheritdoc />
    public partial class Image_Visability : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVisableForUser",
                table: "Images",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVisableForUser",
                table: "Images");
        }
    }
}
