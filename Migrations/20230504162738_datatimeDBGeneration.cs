using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KanS.Migrations {
    /// <inheritdoc />
    public partial class datatimeDBGeneration : Migration {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.AlterColumn<DateTime>(
                name: "AssignmentDate",
                table: "UserBoards",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 4, 13, 49, 31, 811, DateTimeKind.Utc).AddTicks(1029));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.AlterColumn<DateTime>(
                name: "AssignmentDate",
                table: "UserBoards",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 4, 13, 49, 31, 811, DateTimeKind.Utc).AddTicks(1029),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }
    }
}
