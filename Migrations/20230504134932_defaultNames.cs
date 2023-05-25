using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KanS.Migrations {
    /// <inheritdoc />
    public partial class defaultNames : Migration {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.RenameColumn(
                name: "Postion",
                table: "UserBoards",
                newName: "Position");

            migrationBuilder.AlterColumn<int>(
                name: "FavouritePostion",
                table: "UserBoards",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AssignmentDate",
                table: "UserBoards",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 4, 13, 49, 31, 811, DateTimeKind.Utc).AddTicks(1029),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 4, 11, 58, 21, 556, DateTimeKind.Utc).AddTicks(1783));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Sections",
                type: "text",
                nullable: false,
                defaultValue: "New Section",
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValue: "Section");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Jobs",
                type: "text",
                nullable: false,
                defaultValue: "New Task",
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValue: "Task");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Boards",
                type: "text",
                nullable: false,
                defaultValue: "New Board",
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValue: "Board");

            migrationBuilder.AlterColumn<string>(
                name: "Icon",
                table: "Boards",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Boards",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.RenameColumn(
                name: "Position",
                table: "UserBoards",
                newName: "Postion");

            migrationBuilder.AlterColumn<int>(
                name: "FavouritePostion",
                table: "UserBoards",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "AssignmentDate",
                table: "UserBoards",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 4, 11, 58, 21, 556, DateTimeKind.Utc).AddTicks(1783),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 4, 13, 49, 31, 811, DateTimeKind.Utc).AddTicks(1029));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Sections",
                type: "text",
                nullable: false,
                defaultValue: "Section",
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValue: "New Section");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Jobs",
                type: "text",
                nullable: false,
                defaultValue: "Task",
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValue: "New Task");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Boards",
                type: "text",
                nullable: false,
                defaultValue: "Board",
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValue: "New Board");

            migrationBuilder.AlterColumn<string>(
                name: "Icon",
                table: "Boards",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Boards",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValue: "");
        }
    }
}
