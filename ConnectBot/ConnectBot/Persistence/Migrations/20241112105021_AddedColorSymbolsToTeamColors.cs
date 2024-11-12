using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConnectBot.Persistence.Migrations
{
    public partial class AddedColorSymbolsToTeamColors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ColorSymbol",
                table: "TeamColors",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "TeamColors",
                keyColumn: "Id",
                keyValue: 1,
                column: "ColorSymbol",
                value: "🟦");

            migrationBuilder.UpdateData(
                table: "TeamColors",
                keyColumn: "Id",
                keyValue: 2,
                column: "ColorSymbol",
                value: "🟥");

            migrationBuilder.UpdateData(
                table: "TeamColors",
                keyColumn: "Id",
                keyValue: 3,
                column: "ColorSymbol",
                value: "🟨");

            migrationBuilder.UpdateData(
                table: "TeamColors",
                keyColumn: "Id",
                keyValue: 4,
                column: "ColorSymbol",
                value: "🟩");

            migrationBuilder.UpdateData(
                table: "TeamColors",
                keyColumn: "Id",
                keyValue: 5,
                column: "ColorSymbol",
                value: "🟧");

            migrationBuilder.UpdateData(
                table: "TeamColors",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ColorSymbol", "Name" },
                values: new object[] { "🟪", "Purple" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColorSymbol",
                table: "TeamColors");

            migrationBuilder.UpdateData(
                table: "TeamColors",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Purple ");
        }
    }
}
