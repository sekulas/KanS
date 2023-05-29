using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KanS.Migrations
{
    /// <inheritdoc />
    public partial class favouriteUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Favourite",
                table: "Boards");

            migrationBuilder.AddColumn<bool>(
                name: "Favourite",
                table: "UserBoards",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Favourite",
                table: "UserBoards");

            migrationBuilder.AddColumn<bool>(
                name: "Favourite",
                table: "Boards",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
