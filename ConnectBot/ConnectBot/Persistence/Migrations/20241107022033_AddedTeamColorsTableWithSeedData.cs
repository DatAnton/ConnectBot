using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ConnectBot.Persistence.Migrations
{
    public partial class AddedTeamColorsTableWithSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TeamColors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamColors", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "TeamColors",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Blue" },
                    { 2, "Red" },
                    { 3, "Yellow" },
                    { 4, "Green" },
                    { 5, "Orange" },
                    { 6, "Purple " }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamColors");
        }
    }
}
