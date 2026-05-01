using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestLens.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ModifySomeEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Durations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Interval = table.Column<int>(type: "INTEGER", nullable: false),
                    DurationCount = table.Column<int>(type: "INTEGER", nullable: false),
                    days = table.Column<int>(type: "INTEGER", nullable: true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Hint = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Durations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Engines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Engines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SecurityGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    IsHidden = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Markets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TradeEngineId = table.Column<int>(type: "INTEGER", nullable: false),
                    TradeEngineName = table.Column<string>(type: "TEXT", nullable: false),
                    TradeEngineTitle = table.Column<string>(type: "TEXT", nullable: false),
                    MarketName = table.Column<string>(type: "TEXT", nullable: false),
                    MarketTitle = table.Column<string>(type: "TEXT", nullable: false),
                    MarketId = table.Column<int>(type: "INTEGER", nullable: false),
                    MarketEntityId = table.Column<int>(type: "INTEGER", nullable: true),
                    MarketPlace = table.Column<string>(type: "TEXT", nullable: false),
                    IsOtc = table.Column<bool>(type: "INTEGER", nullable: false),
                    HasHistoryFiles = table.Column<bool>(type: "INTEGER", nullable: false),
                    HasHistoryTradesFiles = table.Column<bool>(type: "INTEGER", nullable: false),
                    HasTrades = table.Column<bool>(type: "INTEGER", nullable: false),
                    HasHistory = table.Column<bool>(type: "INTEGER", nullable: false),
                    HasCandles = table.Column<bool>(type: "INTEGER", nullable: false),
                    HasOrderbook = table.Column<bool>(type: "INTEGER", nullable: false),
                    HasTradingsession = table.Column<bool>(type: "INTEGER", nullable: false),
                    HasExtraYields = table.Column<bool>(type: "INTEGER", nullable: false),
                    HasDelay = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Markets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Markets_Engines_TradeEngineId",
                        column: x => x.TradeEngineId,
                        principalTable: "Engines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Markets_Markets_MarketEntityId",
                        column: x => x.MarketEntityId,
                        principalTable: "Markets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SecurityTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TradeEngineId = table.Column<int>(type: "INTEGER", nullable: false),
                    trade_engine_name = table.Column<string>(type: "TEXT", nullable: false),
                    TradeEngineTitle = table.Column<string>(type: "TEXT", nullable: false),
                    SecurityTypeName = table.Column<string>(type: "TEXT", nullable: false),
                    SecurityTypeTitle = table.Column<string>(type: "TEXT", nullable: false),
                    SecurityGroupName = table.Column<string>(type: "TEXT", nullable: false),
                    StockType = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SecurityTypes_Engines_TradeEngineId",
                        column: x => x.TradeEngineId,
                        principalTable: "Engines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SecurityCollections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    SecurityGroupId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityCollections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SecurityCollections_SecurityGroups_SecurityGroupId",
                        column: x => x.SecurityGroupId,
                        principalTable: "SecurityGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BoardGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TradeEngineId = table.Column<int>(type: "INTEGER", nullable: false),
                    EngineId = table.Column<int>(type: "INTEGER", nullable: true),
                    TradeEngineName = table.Column<string>(type: "TEXT", nullable: false),
                    TradeEngineTitle = table.Column<string>(type: "TEXT", nullable: false),
                    MarketId = table.Column<int>(type: "INTEGER", nullable: false),
                    MarketName = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    IsDefault = table.Column<bool>(type: "INTEGER", nullable: false),
                    board_group_id = table.Column<int>(type: "INTEGER", nullable: false),
                    BoardGroupEntityId = table.Column<int>(type: "INTEGER", nullable: true),
                    IsTraded = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsOrderDriven = table.Column<bool>(type: "INTEGER", nullable: false),
                    Category = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoardGroups_BoardGroups_BoardGroupEntityId",
                        column: x => x.BoardGroupEntityId,
                        principalTable: "BoardGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BoardGroups_Engines_EngineId",
                        column: x => x.EngineId,
                        principalTable: "Engines",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BoardGroups_Markets_MarketId",
                        column: x => x.MarketId,
                        principalTable: "Markets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Boards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BoardGroupId = table.Column<int>(type: "INTEGER", nullable: false),
                    EngineId = table.Column<int>(type: "INTEGER", nullable: false),
                    MarketId = table.Column<int>(type: "INTEGER", nullable: false),
                    BoardId = table.Column<string>(type: "TEXT", nullable: true),
                    BoardEntityId = table.Column<int>(type: "INTEGER", nullable: true),
                    board_title = table.Column<string>(type: "TEXT", nullable: false),
                    IsTraded = table.Column<bool>(type: "INTEGER", nullable: false),
                    HasCandles = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsPrimary = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Boards_BoardGroups_BoardGroupId",
                        column: x => x.BoardGroupId,
                        principalTable: "BoardGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Boards_Boards_BoardEntityId",
                        column: x => x.BoardEntityId,
                        principalTable: "Boards",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Boards_Engines_EngineId",
                        column: x => x.EngineId,
                        principalTable: "Engines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Boards_Markets_MarketId",
                        column: x => x.MarketId,
                        principalTable: "Markets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoardGroups_BoardGroupEntityId",
                table: "BoardGroups",
                column: "BoardGroupEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardGroups_EngineId",
                table: "BoardGroups",
                column: "EngineId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardGroups_MarketId",
                table: "BoardGroups",
                column: "MarketId");

            migrationBuilder.CreateIndex(
                name: "IX_Boards_BoardEntityId",
                table: "Boards",
                column: "BoardEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Boards_BoardGroupId",
                table: "Boards",
                column: "BoardGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Boards_EngineId",
                table: "Boards",
                column: "EngineId");

            migrationBuilder.CreateIndex(
                name: "IX_Boards_MarketId",
                table: "Boards",
                column: "MarketId");

            migrationBuilder.CreateIndex(
                name: "IX_Markets_MarketEntityId",
                table: "Markets",
                column: "MarketEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Markets_TradeEngineId",
                table: "Markets",
                column: "TradeEngineId");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityCollections_SecurityGroupId",
                table: "SecurityCollections",
                column: "SecurityGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityTypes_TradeEngineId",
                table: "SecurityTypes",
                column: "TradeEngineId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Boards");

            migrationBuilder.DropTable(
                name: "Durations");

            migrationBuilder.DropTable(
                name: "SecurityCollections");

            migrationBuilder.DropTable(
                name: "SecurityTypes");

            migrationBuilder.DropTable(
                name: "BoardGroups");

            migrationBuilder.DropTable(
                name: "SecurityGroups");

            migrationBuilder.DropTable(
                name: "Markets");

            migrationBuilder.DropTable(
                name: "Engines");
        }
    }
}
