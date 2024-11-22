using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MemoriesWebApp.Migrations
{
    /// <inheritdoc />
    public partial class addDescColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Meetings",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Meetings");
        }
    }
}
