using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP.NET_CORE.DATA.Migrations
{
    /// <inheritdoc />
    public partial class updatedb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Total_After_Discount",
                table: "Order",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "2ca8fa08-4a80-4714-a5fb-17b7316fddc4",
                column: "ConcurrencyStamp",
                value: "ae308c80-ad24-433b-941f-3032551d9934");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "88ac3925-8432-4f60-89e2-96433d08bbcf",
                column: "ConcurrencyStamp",
                value: "4b7480be-b9de-428c-9b38-99b814fe4076");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "ff045d07-be86-4a4e-bfa4-0264ec832c12",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a459a876-a897-4f79-a4ca-b029e0fea9c1", "AQAAAAEAACcQAAAAEAHv9Pde8eQGRa0F4IkE7V8G82FFSD6ZLnHxXH/DdlmjHQ4d1pBaVifHvV2l94P/xQ==", "b312d87c-2c22-49c6-9303-01e92ee4b165" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Total_After_Discount",
                table: "Order");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "2ca8fa08-4a80-4714-a5fb-17b7316fddc4",
                column: "ConcurrencyStamp",
                value: "2083e1f8-a5b7-46da-8ba4-5247a958a90d");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "88ac3925-8432-4f60-89e2-96433d08bbcf",
                column: "ConcurrencyStamp",
                value: "f3cd50a2-f7bf-4f1a-8328-dc7828097b2e");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "ff045d07-be86-4a4e-bfa4-0264ec832c12",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "18e087a3-aa69-49b6-a576-792179499ccb", "AQAAAAEAACcQAAAAED+Ja3/1/lA0AhSxCd6pbj1bmh2Suu8TdCggQ2Ed7ne9PWGPSPE1VzJJCt/efjamMg==", "18f95f68-d9e7-43dd-8587-be3515ebfd8a" });
        }
    }
}
