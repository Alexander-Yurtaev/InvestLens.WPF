using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestLens.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FixBoardModelMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Settings.Boards_Settings.Boards_BoardEntityId",
                table: "Settings.Boards");

            migrationBuilder.DropIndex(
                name: "IX_Settings.Boards_BoardEntityId",
                table: "Settings.Boards");

            migrationBuilder.DropColumn(
                name: "BoardEntityId",
                table: "Settings.Boards");

            migrationBuilder.RenameColumn(
                name: "board_group_id",
                table: "Settings.BoardGroups",
                newName: "BoardGroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BoardGroupId",
                table: "Settings.BoardGroups",
                newName: "board_group_id");

            migrationBuilder.AddColumn<int>(
                name: "BoardEntityId",
                table: "Settings.Boards",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Settings.Boards_BoardEntityId",
                table: "Settings.Boards",
                column: "BoardEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Settings.Boards_Settings.Boards_BoardEntityId",
                table: "Settings.Boards",
                column: "BoardEntityId",
                principalTable: "Settings.Boards",
                principalColumn: "Id");
        }
    }
}
