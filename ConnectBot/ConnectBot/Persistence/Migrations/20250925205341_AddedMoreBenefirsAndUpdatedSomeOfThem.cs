using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConnectBot.Persistence.Migrations
{
    public partial class AddedMoreBenefirsAndUpdatedSomeOfThem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "meta",
                table: "EventBenefits",
                keyColumn: "Id",
                keyValue: 4,
                column: "Content",
                value: "Мы отметим тебя в инстаграме нашего Коннекта! Подойди к Кате.");

            migrationBuilder.UpdateData(
                schema: "meta",
                table: "EventBenefits",
                keyColumn: "Id",
                keyValue: 9,
                column: "Content",
                value: "Сделать незаметно смешную фотку кого-то из молодежной команды до конца Коннекта и скинуть ее технической команде.");

            migrationBuilder.UpdateData(
                schema: "meta",
                table: "EventBenefits",
                keyColumn: "Id",
                keyValue: 13,
                column: "Content",
                value: "После звука дверного звонка говорить громко «Окак!».");

            migrationBuilder.UpdateData(
                schema: "meta",
                table: "EventBenefits",
                keyColumn: "Id",
                keyValue: 15,
                column: "Content",
                value: "После звука барабана говорить громко «это фиаско братан!».");

            migrationBuilder.InsertData(
                schema: "meta",
                table: "EventBenefits",
                columns: new[] { "Id", "Content", "EventBenefitType", "IsOneTimeBenefit" },
                values: new object[] { 16, "Носить странную кепку ввесь Коннект. Подойти к организаторам чтобы получить её.", 0, false });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "meta",
                table: "EventBenefits",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.UpdateData(
                schema: "meta",
                table: "EventBenefits",
                keyColumn: "Id",
                keyValue: 4,
                column: "Content",
                value: "Мы отметим тебя в инстаграме нашего Коннекта!");

            migrationBuilder.UpdateData(
                schema: "meta",
                table: "EventBenefits",
                keyColumn: "Id",
                keyValue: 9,
                column: "Content",
                value: "Сделать незаметно смешную фотку кого-то из молодежной команды.");

            migrationBuilder.UpdateData(
                schema: "meta",
                table: "EventBenefits",
                keyColumn: "Id",
                keyValue: 13,
                column: "Content",
                value: "После звука сирены 🚨 говорить громко «Окак!».");

            migrationBuilder.UpdateData(
                schema: "meta",
                table: "EventBenefits",
                keyColumn: "Id",
                keyValue: 15,
                column: "Content",
                value: "После звука сирены 🚨 говорить громко «это фиаско братан!».");
        }
    }
}
