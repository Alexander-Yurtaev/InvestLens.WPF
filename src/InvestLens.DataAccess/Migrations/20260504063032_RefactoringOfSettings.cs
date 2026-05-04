using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestLens.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RefactoringOfSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SecType",
                table: "Securities",
                newName: "SecTypeId");

            migrationBuilder.RenameColumn(
                name: "SecGroup",
                table: "Securities",
                newName: "SecGroupId");

            migrationBuilder.AlterColumn<bool>(
                name: "IsTraded",
                table: "Securities",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "EmitentTitle",
                table: "Securities",
                type: "TEXT",
                maxLength: 765,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.CreateIndex(
                name: "IX_Securities_SecGroupId",
                table: "Securities",
                column: "SecGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Securities_SecTypeId",
                table: "Securities",
                column: "SecTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Securities_Settings.SecurityGroups_SecGroupId",
                table: "Securities",
                column: "SecGroupId",
                principalTable: "Settings.SecurityGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Securities_Settings.SecurityTypes_SecTypeId",
                table: "Securities",
                column: "SecTypeId",
                principalTable: "Settings.SecurityTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Securities_Settings.SecurityGroups_SecGroupId",
                table: "Securities");

            migrationBuilder.DropForeignKey(
                name: "FK_Securities_Settings.SecurityTypes_SecTypeId",
                table: "Securities");

            migrationBuilder.DropIndex(
                name: "IX_Securities_SecGroupId",
                table: "Securities");

            migrationBuilder.DropIndex(
                name: "IX_Securities_SecTypeId",
                table: "Securities");

            migrationBuilder.RenameColumn(
                name: "SecTypeId",
                table: "Securities",
                newName: "SecType");

            migrationBuilder.RenameColumn(
                name: "SecGroupId",
                table: "Securities",
                newName: "SecGroup");

            migrationBuilder.AlterColumn<string>(
                name: "IsTraded",
                table: "Securities",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "EmitentTitle",
                table: "Securities",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 765,
                oldNullable: true);
        }
    }
}
