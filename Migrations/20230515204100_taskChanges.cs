using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KanS.Migrations
{
    /// <inheritdoc />
    public partial class taskChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Jobs");

            migrationBuilder.RenameColumn(
                name: "Postion",
                table: "Jobs",
                newName: "Position");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Jobs",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValue: "New Task");

            migrationBuilder.AlterColumn<string>(
                name: "AssignedTo",
                table: "Jobs",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Jobs",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Jobs");

            migrationBuilder.RenameColumn(
                name: "Position",
                table: "Jobs",
                newName: "Postion");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Jobs",
                type: "text",
                nullable: false,
                defaultValue: "New Task",
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "AssignedTo",
                table: "Jobs",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Jobs",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
