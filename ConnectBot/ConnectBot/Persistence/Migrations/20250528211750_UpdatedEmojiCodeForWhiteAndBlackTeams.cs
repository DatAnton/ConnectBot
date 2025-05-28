using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConnectBot.Persistence.Migrations
{
    public partial class UpdatedEmojiCodeForWhiteAndBlackTeams : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TeamColors",
                keyColumn: "Id",
                keyValue: 7,
                column: "ColorSymbol",
                value: "⬜");

            migrationBuilder.UpdateData(
                table: "TeamColors",
                keyColumn: "Id",
                keyValue: 8,
                column: "ColorSymbol",
                value: "⬛");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TeamColors",
                keyColumn: "Id",
                keyValue: 7,
                column: "ColorSymbol",
                value: "□");

            migrationBuilder.UpdateData(
                table: "TeamColors",
                keyColumn: "Id",
                keyValue: 8,
                column: "ColorSymbol",
                value: "■");
        }
    }
}
