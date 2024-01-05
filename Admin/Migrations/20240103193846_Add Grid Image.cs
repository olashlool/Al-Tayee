using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Admin.Migrations
{
    public partial class AddGridImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "67090959-4746-4662-bc6d-27232c93e6c4");

            migrationBuilder.CreateTable(
                name: "GridImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GridImage", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad376a8f",
                column: "ConcurrencyStamp",
                value: "bf34a1e2-bdc2-4c24-8353-ea2e33b4e138");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd586a8f",
                column: "ConcurrencyStamp",
                value: "7a006715-ba66-4ac7-a492-a5a092c59b00");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e9eba5ec-71fa-4ec4-8332-2620beab0047", "ef0f591c-df22-43bf-9f26-7e7a4c7ab52a", "User", "User" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "dbd84c77-f4fd-44ba-a3a8-5e6963d31e92", "AQAAAAEAACcQAAAAENvwDvti5EP4YHwIdr1E1arwwXi6B9DO3qyHdaJ3HRFCh/zX4sGGJSucCfW4W3bAYw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a50ze710",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d78fb54e-1947-4a4d-8c7f-d04ca14e4cce", "AQAAAAEAACcQAAAAEI92SMSfTh8eNBJURX8kqaYaYLQS/OOMDDuYXGfjK+dM2A3x/JVJrjvg+y5dL9olNw==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GridImage");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e9eba5ec-71fa-4ec4-8332-2620beab0047");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad376a8f",
                column: "ConcurrencyStamp",
                value: "7092b468-113d-46e3-b294-cfeb2446e22d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd586a8f",
                column: "ConcurrencyStamp",
                value: "06bc08ae-d05d-4fb0-bdc6-c5e4630d2243");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "67090959-4746-4662-bc6d-27232c93e6c4", "c3e1f474-67ea-466b-a186-64f9cb559d7e", "User", "User" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4cd66024-02cd-42aa-ae89-83c5a34beb94", "AQAAAAEAACcQAAAAENyLy2uRPbl4aSNEurbJm4UzO3pcsHT4gMYiM+qqJh42fh6u5t7RxYjWmxeMuMECTg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a50ze710",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d8b565fe-97b5-4676-aa98-85bfef91ab00", "AQAAAAEAACcQAAAAENwAEe71YnEdHjMSdhKRo4kECsRlJ0liz3h6XIJpXcYQEKtEG9GvlDu49iq2PFZKIA==" });
        }
    }
}
