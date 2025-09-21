using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ConnectBot.Persistence.Migrations
{
    public partial class AddedEventBenefits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "meta");

            migrationBuilder.AddColumn<int>(
                name: "EventBenefitId",
                table: "EventParticipations",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EventBenefits",
                schema: "meta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Content = table.Column<string>(type: "text", nullable: false),
                    EventBenefitType = table.Column<int>(type: "integer", nullable: false),
                    IsOneTimeBenefit = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventBenefits", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "meta",
                table: "EventBenefits",
                columns: new[] { "Id", "Content", "EventBenefitType", "IsOneTimeBenefit" },
                values: new object[,]
                {
                    { 1, "Cпециальное блюдо только для тебя! Просто подойди на кухню и покажи это сообщение.", 1, false },
                    { 2, "Отдельный напиток только для тебя! Просто подойди на кухню и покажи это сообщение.", 1, false },
                    { 3, "Минута славы! Мы официально тебя представим как главного гостя нашего Коннекта", 1, false },
                    { 4, "Мы отметим тебя в инстаграмме нашего Коннекта!", 1, false },
                    { 5, "Ты получишь оригинальный комплимент от молодежной команды!", 1, false },
                    { 6, "Помочь убраться команде после Коннекта. Спасибо наперед!", 0, false },
                    { 7, "Сказать комплимент кухне в микрофон.", 0, false },
                    { 8, "Весь Коннект называть себя другим именем.", 0, false },
                    { 9, "Сделать незаметно смешную фотку кого-то из молодежной команды.", 0, false },
                    { 10, "Крикнуть «вот это кринж» после какой-то игры.", 0, false },
                    { 11, "Называть и вести себя как человек паук ввесь Коннект.", 0, false },
                    { 12, "Через каждые 20 минут говорить «я люблю вас!».", 0, false },
                    { 13, "Через каждые 20 минут говорить «я люблю вас!».", 0, false },
                    { 14, "После звука сирены 🚨 говорить громко «Окак».", 0, false },
                    { 15, "После звука сирены 🚨 говорить громко «Абаюдна».", 0, false },
                    { 16, "После звука сирены 🚨 говорить громко «это фиаско братан».", 0, false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventParticipations_EventBenefitId",
                table: "EventParticipations",
                column: "EventBenefitId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventParticipations_EventBenefits_EventBenefitId",
                table: "EventParticipations",
                column: "EventBenefitId",
                principalSchema: "meta",
                principalTable: "EventBenefits",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventParticipations_EventBenefits_EventBenefitId",
                table: "EventParticipations");

            migrationBuilder.DropTable(
                name: "EventBenefits",
                schema: "meta");

            migrationBuilder.DropIndex(
                name: "IX_EventParticipations_EventBenefitId",
                table: "EventParticipations");

            migrationBuilder.DropColumn(
                name: "EventBenefitId",
                table: "EventParticipations");
        }
    }
}
