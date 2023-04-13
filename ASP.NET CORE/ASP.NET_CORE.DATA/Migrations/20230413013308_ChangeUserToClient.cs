using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP.NET_CORE.DATA.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUserToClient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_user",
                table: "Cart");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_user",
                table: "Order");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Order",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Cart",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Cart",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "client",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Account = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Img = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Sdt = table.Column<int>(type: "int", nullable: false),
                    Sex = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_client", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_ClientId",
                table: "Order",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_ClientId",
                table: "Cart",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_Client",
                table: "Cart",
                column: "ClientId",
                principalTable: "client",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_user_UserId",
                table: "Cart",
                column: "UserId",
                principalTable: "user",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Client",
                table: "Order",
                column: "ClientId",
                principalTable: "client",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_user_UserId",
                table: "Order",
                column: "UserId",
                principalTable: "user",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_Client",
                table: "Cart");

            migrationBuilder.DropForeignKey(
                name: "FK_Cart_user_UserId",
                table: "Cart");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Client",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_user_UserId",
                table: "Order");

            migrationBuilder.DropTable(
                name: "client");

            migrationBuilder.DropIndex(
                name: "IX_Order_ClientId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Cart_ClientId",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Cart");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Cart",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_user",
                table: "Cart",
                column: "UserId",
                principalTable: "user",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_user",
                table: "Order",
                column: "UserId",
                principalTable: "user",
                principalColumn: "Id");
        }
    }
}
