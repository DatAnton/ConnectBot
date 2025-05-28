using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConnectBot.Persistence.Migrations
{
    public partial class AddedWhiteAndBlackTeams : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TeamColors",
                columns: new[] { "Id", "ColorSymbol", "Name" },
                values: new object[,]
                {
                    { 7, "□", "White" },
                    { 8, "■", "Black" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TeamColors",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "TeamColors",
                keyColumn: "Id",
                keyValue: 8);
        }
    }
}
