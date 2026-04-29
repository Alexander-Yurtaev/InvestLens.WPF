using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestLens.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSecurityEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmitentTitle",
                table: "Securities",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IsTraded",
                table: "Securities",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Isin",
                table: "Securities",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MarketpriceBoardid",
                table: "Securities",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Securities",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrimaryBoardid",
                table: "Securities",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RegNumber",
                table: "Securities",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShortName",
                table: "Securities",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmitentTitle",
                table: "Securities");

            migrationBuilder.DropColumn(
                name: "IsTraded",
                table: "Securities");

            migrationBuilder.DropColumn(
                name: "Isin",
                table: "Securities");

            migrationBuilder.DropColumn(
                name: "MarketpriceBoardid",
                table: "Securities");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Securities");

            migrationBuilder.DropColumn(
                name: "PrimaryBoardid",
                table: "Securities");

            migrationBuilder.DropColumn(
                name: "RegNumber",
                table: "Securities");

            migrationBuilder.DropColumn(
                name: "ShortName",
                table: "Securities");
        }
    }
}
