using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ConnectBot.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedEventBenefits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "meta",
                table: "EventBenefits",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                schema: "meta",
                table: "EventBenefits",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                schema: "meta",
                table: "EventBenefits",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "IsActive", "IsOneTimeBenefit" },
                values: new object[] { true, false });

            migrationBuilder.UpdateData(
                schema: "meta",
                table: "EventBenefits",
                keyColumn: "Id",
                keyValue: 3,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                schema: "meta",
                table: "EventBenefits",
                keyColumn: "Id",
                keyValue: 4,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                schema: "meta",
                table: "EventBenefits",
                keyColumn: "Id",
                keyValue: 5,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                schema: "meta",
                table: "EventBenefits",
                keyColumn: "Id",
                keyValue: 6,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                schema: "meta",
                table: "EventBenefits",
                keyColumn: "Id",
                keyValue: 7,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                schema: "meta",
                table: "EventBenefits",
                keyColumn: "Id",
                keyValue: 8,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                schema: "meta",
                table: "EventBenefits",
                keyColumn: "Id",
                keyValue: 9,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                schema: "meta",
                table: "EventBenefits",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Content", "IsActive" },
                values: new object[] { "Крикнуть «вот это кринж!» после любой игры.", true });

            migrationBuilder.UpdateData(
                schema: "meta",
                table: "EventBenefits",
                keyColumn: "Id",
                keyValue: 11,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                schema: "meta",
                table: "EventBenefits",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Content", "IsActive" },
                values: new object[] { "Через каждые 30 минут говорить «Я люблю вас, булочки!».", true });

            migrationBuilder.UpdateData(
                schema: "meta",
                table: "EventBenefits",
                keyColumn: "Id",
                keyValue: 13,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                schema: "meta",
                table: "EventBenefits",
                keyColumn: "Id",
                keyValue: 14,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                schema: "meta",
                table: "EventBenefits",
                keyColumn: "Id",
                keyValue: 15,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                schema: "meta",
                table: "EventBenefits",
                keyColumn: "Id",
                keyValue: 16,
                column: "IsActive",
                value: false);

            migrationBuilder.InsertData(
                schema: "meta",
                table: "EventBenefits",
                columns: new[] { "Id", "Content", "EventBenefitType", "IsActive", "IsOneTimeBenefit" },
                values: new object[,]
                {
                    { 17, "Найти самого тихого человека и поддержать его.", 0, true, false },
                    { 18, "Стать «амбассадором кухни» и рекламировать еду.", 0, true, false },
                    { 19, "Спросить у 3 людей: «Какой твой вайб сегодня?»", 0, true, false },
                    { 20, "До конца Коннекта здороваться со всеми как ведущий новостей.", 0, true, false }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "meta",
                table: "EventBenefits",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                schema: "meta",
                table: "EventBenefits",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                schema: "meta",
                table: "EventBenefits",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                schema: "meta",
                table: "EventBenefits",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "meta",
                table: "EventBenefits");

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
                keyValue: 10,
                column: "Content",
                value: "Крикнуть «вот это кринж!» после какой-то игры.");

            migrationBuilder.UpdateData(
                schema: "meta",
                table: "EventBenefits",
                keyColumn: "Id",
                keyValue: 12,
                column: "Content",
                value: "Через каждые 20 минут говорить «я люблю вас!».");
        }
    }
}
