using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConnectBot.Persistence.Migrations
{
    public partial class AddedStackTraceToLogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StackTrace",
                table: "Logs",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StackTrace",
                table: "Logs");
        }
    }
}
