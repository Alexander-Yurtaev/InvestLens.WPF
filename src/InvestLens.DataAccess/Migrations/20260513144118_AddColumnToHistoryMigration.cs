using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestLens.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnToHistoryMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FaceUnit",
                table: "History",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "FaceValue",
                table: "History",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FaceUnit",
                table: "History");

            migrationBuilder.DropColumn(
                name: "FaceValue",
                table: "History");
        }
    }
}
