using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KanS.Migrations {
    /// <inheritdoc />
    public partial class @virtual : Migration {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.AddColumn<int>(
                name: "BoardId",
                table: "Jobs",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropColumn(
                name: "BoardId",
                table: "Jobs");
        }
    }
}
