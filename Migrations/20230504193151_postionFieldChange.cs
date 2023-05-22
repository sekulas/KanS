using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KanS.Migrations
{
    /// <inheritdoc />
    public partial class postionFieldChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FavouritePostion",
                table: "UserBoards");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "UserBoards");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FavouritePosition",
                table: "Boards");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "Boards");

            migrationBuilder.AddColumn<int>(
                name: "FavouritePostion",
                table: "UserBoards",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "UserBoards",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
