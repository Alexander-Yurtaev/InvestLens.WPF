using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestLens.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddSecurityEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Securities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SecId = table.Column<string>(type: "TEXT", nullable: false),
                    IsLoaded = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Securities", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Securities");
        }
    }
}
