using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConnectBot.Persistence.Migrations
{
    public partial class EditedEventBenefitsText : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                keyValue: 1,
                column: "IsOneTimeBenefit",
                value: true);

            migrationBuilder.UpdateData(
                schema: "meta",
                table: "EventBenefits",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsOneTimeBenefit",
                value: true);

            migrationBuilder.UpdateData(
                schema: "meta",
                table: "EventBenefits",
                keyColumn: "Id",
                keyValue: 3,
                column: "Content",
                value: "Минута славы! Мы официально тебя представим как главного гостя нашего Коннекта.");

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
                keyValue: 10,
                column: "Content",
                value: "Крикнуть «вот это кринж!» после какой-то игры.");

            migrationBuilder.UpdateData(
                schema: "meta",
                table: "EventBenefits",
                keyColumn: "Id",
                keyValue: 11,
                column: "Content",
                value: "Называть и вести себя как человек-паук ввесь Коннект.");

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
                keyValue: 14,
                column: "Content",
                value: "После звука сирены 🚨 говорить громко «Абаюдна!».");

            migrationBuilder.UpdateData(
                schema: "meta",
                table: "EventBenefits",
                keyColumn: "Id",
                keyValue: 15,
                column: "Content",
                value: "После звука сирены 🚨 говорить громко «это фиаско братан!».");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "meta",
                table: "EventBenefits",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsOneTimeBenefit",
                value: false);

            migrationBuilder.UpdateData(
                schema: "meta",
                table: "EventBenefits",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsOneTimeBenefit",
                value: false);

            migrationBuilder.UpdateData(
                schema: "meta",
                table: "EventBenefits",
                keyColumn: "Id",
                keyValue: 3,
                column: "Content",
                value: "Минута славы! Мы официально тебя представим как главного гостя нашего Коннекта");

            migrationBuilder.UpdateData(
                schema: "meta",
                table: "EventBenefits",
                keyColumn: "Id",
                keyValue: 4,
                column: "Content",
                value: "Мы отметим тебя в инстаграмме нашего Коннекта!");

            migrationBuilder.UpdateData(
                schema: "meta",
                table: "EventBenefits",
                keyColumn: "Id",
                keyValue: 10,
                column: "Content",
                value: "Крикнуть «вот это кринж» после какой-то игры.");

            migrationBuilder.UpdateData(
                schema: "meta",
                table: "EventBenefits",
                keyColumn: "Id",
                keyValue: 11,
                column: "Content",
                value: "Называть и вести себя как человек паук ввесь Коннект.");

            migrationBuilder.UpdateData(
                schema: "meta",
                table: "EventBenefits",
                keyColumn: "Id",
                keyValue: 13,
                column: "Content",
                value: "Через каждые 20 минут говорить «я люблю вас!».");

            migrationBuilder.UpdateData(
                schema: "meta",
                table: "EventBenefits",
                keyColumn: "Id",
                keyValue: 14,
                column: "Content",
                value: "После звука сирены 🚨 говорить громко «Окак».");

            migrationBuilder.UpdateData(
                schema: "meta",
                table: "EventBenefits",
                keyColumn: "Id",
                keyValue: 15,
                column: "Content",
                value: "После звука сирены 🚨 говорить громко «Абаюдна».");

            migrationBuilder.InsertData(
                schema: "meta",
                table: "EventBenefits",
                columns: new[] { "Id", "Content", "EventBenefitType", "IsOneTimeBenefit" },
                values: new object[] { 16, "После звука сирены 🚨 говорить громко «это фиаско братан».", 0, false });
        }
    }
}
