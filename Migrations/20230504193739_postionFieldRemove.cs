using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KanS.Migrations
{
    /// <inheritdoc />
    public partial class postionFieldRemove : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FavouritePosition",
                table: "Boards");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "Boards");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FavouritePosition",
                table: "Boards",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "Boards",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
