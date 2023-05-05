using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KanS.Migrations
{
    /// <inheritdoc />
    public partial class iconsRemoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icon",
                table: "Boards");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "Boards",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
