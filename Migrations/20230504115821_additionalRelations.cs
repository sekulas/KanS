using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace KanS.Migrations {
    /// <inheritdoc />
    public partial class additionalRelations : Migration {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Sections_SectionId",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_Boards_BoardId",
                table: "Sections");

            migrationBuilder.DropIndex(
                name: "IX_Sections_BoardId",
                table: "Sections");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_SectionId",
                table: "Jobs");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AssignmentDate",
                table: "UserBoards",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 4, 11, 58, 21, 556, DateTimeKind.Utc).AddTicks(1783),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 4, 11, 12, 34, 179, DateTimeKind.Utc).AddTicks(3874));

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Sections",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Jobs",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Boards",
                type: "text",
                nullable: false,
                defaultValue: "Board",
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Sections_Id",
                table: "Jobs",
                column: "Id",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_Boards_Id",
                table: "Sections",
                column: "Id",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Sections_Id",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_Boards_Id",
                table: "Sections");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AssignmentDate",
                table: "UserBoards",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 4, 11, 12, 34, 179, DateTimeKind.Utc).AddTicks(3874),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 5, 4, 11, 58, 21, 556, DateTimeKind.Utc).AddTicks(1783));

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Sections",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Jobs",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Boards",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValue: "Board");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_BoardId",
                table: "Sections",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_SectionId",
                table: "Jobs",
                column: "SectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Sections_SectionId",
                table: "Jobs",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_Boards_BoardId",
                table: "Sections",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
