using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestLens.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RefactoringOfDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoardGroups_BoardGroups_BoardGroupEntityId",
                table: "BoardGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_BoardGroups_Engines_EngineId",
                table: "BoardGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_BoardGroups_Markets_MarketId",
                table: "BoardGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_Boards_BoardGroups_BoardGroupId",
                table: "Boards");

            migrationBuilder.DropForeignKey(
                name: "FK_Boards_Boards_BoardEntityId",
                table: "Boards");

            migrationBuilder.DropForeignKey(
                name: "FK_Boards_Engines_EngineId",
                table: "Boards");

            migrationBuilder.DropForeignKey(
                name: "FK_Boards_Markets_MarketId",
                table: "Boards");

            migrationBuilder.DropForeignKey(
                name: "FK_Markets_Engines_TradeEngineId",
                table: "Markets");

            migrationBuilder.DropForeignKey(
                name: "FK_Markets_Markets_MarketEntityId",
                table: "Markets");

            migrationBuilder.DropForeignKey(
                name: "FK_SecurityCollections_SecurityGroups_SecurityGroupId",
                table: "SecurityCollections");

            migrationBuilder.DropForeignKey(
                name: "FK_SecurityTypes_Engines_TradeEngineId",
                table: "SecurityTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SecurityTypes",
                table: "SecurityTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SecurityGroups",
                table: "SecurityGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SecurityCollections",
                table: "SecurityCollections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Markets",
                table: "Markets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Engines",
                table: "Engines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Durations",
                table: "Durations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Boards",
                table: "Boards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BoardGroups",
                table: "BoardGroups");

            migrationBuilder.DropColumn(
                name: "trade_engine_name",
                table: "SecurityTypes");

            migrationBuilder.DropColumn(
                name: "board_title",
                table: "Boards");

            migrationBuilder.RenameTable(
                name: "SecurityTypes",
                newName: "Settings.SecurityTypes");

            migrationBuilder.RenameTable(
                name: "SecurityGroups",
                newName: "Settings.SecurityGroups");

            migrationBuilder.RenameTable(
                name: "SecurityCollections",
                newName: "Settings.SecurityCollections");

            migrationBuilder.RenameTable(
                name: "Markets",
                newName: "Settings.Markets");

            migrationBuilder.RenameTable(
                name: "Engines",
                newName: "Settings.Engines");

            migrationBuilder.RenameTable(
                name: "Durations",
                newName: "Settings.Durations");

            migrationBuilder.RenameTable(
                name: "Boards",
                newName: "Settings.Boards");

            migrationBuilder.RenameTable(
                name: "BoardGroups",
                newName: "Settings.BoardGroups");

            migrationBuilder.RenameIndex(
                name: "IX_SecurityTypes_TradeEngineId",
                table: "Settings.SecurityTypes",
                newName: "IX_Settings.SecurityTypes_TradeEngineId");

            migrationBuilder.RenameIndex(
                name: "IX_SecurityCollections_SecurityGroupId",
                table: "Settings.SecurityCollections",
                newName: "IX_Settings.SecurityCollections_SecurityGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Markets_TradeEngineId",
                table: "Settings.Markets",
                newName: "IX_Settings.Markets_TradeEngineId");

            migrationBuilder.RenameIndex(
                name: "IX_Markets_MarketEntityId",
                table: "Settings.Markets",
                newName: "IX_Settings.Markets_MarketEntityId");

            migrationBuilder.RenameColumn(
                name: "days",
                table: "Settings.Durations",
                newName: "Days");

            migrationBuilder.RenameIndex(
                name: "IX_Boards_MarketId",
                table: "Settings.Boards",
                newName: "IX_Settings.Boards_MarketId");

            migrationBuilder.RenameIndex(
                name: "IX_Boards_EngineId",
                table: "Settings.Boards",
                newName: "IX_Settings.Boards_EngineId");

            migrationBuilder.RenameIndex(
                name: "IX_Boards_BoardGroupId",
                table: "Settings.Boards",
                newName: "IX_Settings.Boards_BoardGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Boards_BoardEntityId",
                table: "Settings.Boards",
                newName: "IX_Settings.Boards_BoardEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_BoardGroups_MarketId",
                table: "Settings.BoardGroups",
                newName: "IX_Settings.BoardGroups_MarketId");

            migrationBuilder.RenameIndex(
                name: "IX_BoardGroups_EngineId",
                table: "Settings.BoardGroups",
                newName: "IX_Settings.BoardGroups_EngineId");

            migrationBuilder.RenameIndex(
                name: "IX_BoardGroups_BoardGroupEntityId",
                table: "Settings.BoardGroups",
                newName: "IX_Settings.BoardGroups_BoardGroupEntityId");

            migrationBuilder.AddColumn<string>(
                name: "TradeEngineName",
                table: "Settings.SecurityTypes",
                type: "TEXT",
                maxLength: 45,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BoardTitle",
                table: "Settings.Boards",
                type: "TEXT",
                maxLength: 381,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Settings.SecurityTypes",
                table: "Settings.SecurityTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Settings.SecurityGroups",
                table: "Settings.SecurityGroups",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Settings.SecurityCollections",
                table: "Settings.SecurityCollections",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Settings.Markets",
                table: "Settings.Markets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Settings.Engines",
                table: "Settings.Engines",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Settings.Durations",
                table: "Settings.Durations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Settings.Boards",
                table: "Settings.Boards",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Settings.BoardGroups",
                table: "Settings.BoardGroups",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Settings.BoardGroups_Settings.BoardGroups_BoardGroupEntityId",
                table: "Settings.BoardGroups",
                column: "BoardGroupEntityId",
                principalTable: "Settings.BoardGroups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Settings.BoardGroups_Settings.Engines_EngineId",
                table: "Settings.BoardGroups",
                column: "EngineId",
                principalTable: "Settings.Engines",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Settings.BoardGroups_Settings.Markets_MarketId",
                table: "Settings.BoardGroups",
                column: "MarketId",
                principalTable: "Settings.Markets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Settings.Boards_Settings.BoardGroups_BoardGroupId",
                table: "Settings.Boards",
                column: "BoardGroupId",
                principalTable: "Settings.BoardGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Settings.Boards_Settings.Boards_BoardEntityId",
                table: "Settings.Boards",
                column: "BoardEntityId",
                principalTable: "Settings.Boards",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Settings.Boards_Settings.Engines_EngineId",
                table: "Settings.Boards",
                column: "EngineId",
                principalTable: "Settings.Engines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Settings.Boards_Settings.Markets_MarketId",
                table: "Settings.Boards",
                column: "MarketId",
                principalTable: "Settings.Markets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Settings.Markets_Settings.Engines_TradeEngineId",
                table: "Settings.Markets",
                column: "TradeEngineId",
                principalTable: "Settings.Engines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Settings.Markets_Settings.Markets_MarketEntityId",
                table: "Settings.Markets",
                column: "MarketEntityId",
                principalTable: "Settings.Markets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Settings.SecurityCollections_Settings.SecurityGroups_SecurityGroupId",
                table: "Settings.SecurityCollections",
                column: "SecurityGroupId",
                principalTable: "Settings.SecurityGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Settings.SecurityTypes_Settings.Engines_TradeEngineId",
                table: "Settings.SecurityTypes",
                column: "TradeEngineId",
                principalTable: "Settings.Engines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Settings.BoardGroups_Settings.BoardGroups_BoardGroupEntityId",
                table: "Settings.BoardGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_Settings.BoardGroups_Settings.Engines_EngineId",
                table: "Settings.BoardGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_Settings.BoardGroups_Settings.Markets_MarketId",
                table: "Settings.BoardGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_Settings.Boards_Settings.BoardGroups_BoardGroupId",
                table: "Settings.Boards");

            migrationBuilder.DropForeignKey(
                name: "FK_Settings.Boards_Settings.Boards_BoardEntityId",
                table: "Settings.Boards");

            migrationBuilder.DropForeignKey(
                name: "FK_Settings.Boards_Settings.Engines_EngineId",
                table: "Settings.Boards");

            migrationBuilder.DropForeignKey(
                name: "FK_Settings.Boards_Settings.Markets_MarketId",
                table: "Settings.Boards");

            migrationBuilder.DropForeignKey(
                name: "FK_Settings.Markets_Settings.Engines_TradeEngineId",
                table: "Settings.Markets");

            migrationBuilder.DropForeignKey(
                name: "FK_Settings.Markets_Settings.Markets_MarketEntityId",
                table: "Settings.Markets");

            migrationBuilder.DropForeignKey(
                name: "FK_Settings.SecurityCollections_Settings.SecurityGroups_SecurityGroupId",
                table: "Settings.SecurityCollections");

            migrationBuilder.DropForeignKey(
                name: "FK_Settings.SecurityTypes_Settings.Engines_TradeEngineId",
                table: "Settings.SecurityTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Settings.SecurityTypes",
                table: "Settings.SecurityTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Settings.SecurityGroups",
                table: "Settings.SecurityGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Settings.SecurityCollections",
                table: "Settings.SecurityCollections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Settings.Markets",
                table: "Settings.Markets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Settings.Engines",
                table: "Settings.Engines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Settings.Durations",
                table: "Settings.Durations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Settings.Boards",
                table: "Settings.Boards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Settings.BoardGroups",
                table: "Settings.BoardGroups");

            migrationBuilder.DropColumn(
                name: "TradeEngineName",
                table: "Settings.SecurityTypes");

            migrationBuilder.DropColumn(
                name: "BoardTitle",
                table: "Settings.Boards");

            migrationBuilder.RenameTable(
                name: "Settings.SecurityTypes",
                newName: "SecurityTypes");

            migrationBuilder.RenameTable(
                name: "Settings.SecurityGroups",
                newName: "SecurityGroups");

            migrationBuilder.RenameTable(
                name: "Settings.SecurityCollections",
                newName: "SecurityCollections");

            migrationBuilder.RenameTable(
                name: "Settings.Markets",
                newName: "Markets");

            migrationBuilder.RenameTable(
                name: "Settings.Engines",
                newName: "Engines");

            migrationBuilder.RenameTable(
                name: "Settings.Durations",
                newName: "Durations");

            migrationBuilder.RenameTable(
                name: "Settings.Boards",
                newName: "Boards");

            migrationBuilder.RenameTable(
                name: "Settings.BoardGroups",
                newName: "BoardGroups");

            migrationBuilder.RenameIndex(
                name: "IX_Settings.SecurityTypes_TradeEngineId",
                table: "SecurityTypes",
                newName: "IX_SecurityTypes_TradeEngineId");

            migrationBuilder.RenameIndex(
                name: "IX_Settings.SecurityCollections_SecurityGroupId",
                table: "SecurityCollections",
                newName: "IX_SecurityCollections_SecurityGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Settings.Markets_TradeEngineId",
                table: "Markets",
                newName: "IX_Markets_TradeEngineId");

            migrationBuilder.RenameIndex(
                name: "IX_Settings.Markets_MarketEntityId",
                table: "Markets",
                newName: "IX_Markets_MarketEntityId");

            migrationBuilder.RenameColumn(
                name: "Days",
                table: "Durations",
                newName: "days");

            migrationBuilder.RenameIndex(
                name: "IX_Settings.Boards_MarketId",
                table: "Boards",
                newName: "IX_Boards_MarketId");

            migrationBuilder.RenameIndex(
                name: "IX_Settings.Boards_EngineId",
                table: "Boards",
                newName: "IX_Boards_EngineId");

            migrationBuilder.RenameIndex(
                name: "IX_Settings.Boards_BoardGroupId",
                table: "Boards",
                newName: "IX_Boards_BoardGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Settings.Boards_BoardEntityId",
                table: "Boards",
                newName: "IX_Boards_BoardEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_Settings.BoardGroups_MarketId",
                table: "BoardGroups",
                newName: "IX_BoardGroups_MarketId");

            migrationBuilder.RenameIndex(
                name: "IX_Settings.BoardGroups_EngineId",
                table: "BoardGroups",
                newName: "IX_BoardGroups_EngineId");

            migrationBuilder.RenameIndex(
                name: "IX_Settings.BoardGroups_BoardGroupEntityId",
                table: "BoardGroups",
                newName: "IX_BoardGroups_BoardGroupEntityId");

            migrationBuilder.AddColumn<string>(
                name: "trade_engine_name",
                table: "SecurityTypes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "board_title",
                table: "Boards",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SecurityTypes",
                table: "SecurityTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SecurityGroups",
                table: "SecurityGroups",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SecurityCollections",
                table: "SecurityCollections",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Markets",
                table: "Markets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Engines",
                table: "Engines",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Durations",
                table: "Durations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Boards",
                table: "Boards",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BoardGroups",
                table: "BoardGroups",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BoardGroups_BoardGroups_BoardGroupEntityId",
                table: "BoardGroups",
                column: "BoardGroupEntityId",
                principalTable: "BoardGroups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BoardGroups_Engines_EngineId",
                table: "BoardGroups",
                column: "EngineId",
                principalTable: "Engines",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BoardGroups_Markets_MarketId",
                table: "BoardGroups",
                column: "MarketId",
                principalTable: "Markets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Boards_BoardGroups_BoardGroupId",
                table: "Boards",
                column: "BoardGroupId",
                principalTable: "BoardGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Boards_Boards_BoardEntityId",
                table: "Boards",
                column: "BoardEntityId",
                principalTable: "Boards",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Boards_Engines_EngineId",
                table: "Boards",
                column: "EngineId",
                principalTable: "Engines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Boards_Markets_MarketId",
                table: "Boards",
                column: "MarketId",
                principalTable: "Markets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Markets_Engines_TradeEngineId",
                table: "Markets",
                column: "TradeEngineId",
                principalTable: "Engines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Markets_Markets_MarketEntityId",
                table: "Markets",
                column: "MarketEntityId",
                principalTable: "Markets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SecurityCollections_SecurityGroups_SecurityGroupId",
                table: "SecurityCollections",
                column: "SecurityGroupId",
                principalTable: "SecurityGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SecurityTypes_Engines_TradeEngineId",
                table: "SecurityTypes",
                column: "TradeEngineId",
                principalTable: "Engines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
