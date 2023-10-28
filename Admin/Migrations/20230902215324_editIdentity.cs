using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Admin.Migrations
{
    public partial class editIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "483c800b-76bd-49e6-9138-4798fcef1597");

            migrationBuilder.AddColumn<int>(
                name: "Day",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Month",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad376a8f",
                column: "ConcurrencyStamp",
                value: "f20b3020-956a-4c74-9858-4beb699e9df9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd586a8f",
                column: "ConcurrencyStamp",
                value: "67ff1379-1568-4fde-9e80-9872f625fde6");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "425b065b-34fe-4d3a-ab4c-e7efc9f54089", "ea3371d7-f8c7-46df-b26b-9740bd20d0c6", "User", "User" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "075f6682-6da0-4232-bb69-2e5c8f0fb368", "AQAAAAEAACcQAAAAELJgx5YOmq7pDPJOf7OdFp/2KWvi7SA5iW3mFp2qrvUTh/ox80P1aNEZzytcv6KZMg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a50ze710",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "cdd05c02-153e-4e48-8382-3751eaf8553e", "AQAAAAEAACcQAAAAEL21lWbrINmuyc4BvyOjg9VUFuwexfS85xP53a/32yktiqELFTB9d+jkE9MysIiZjQ==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "425b065b-34fe-4d3a-ab4c-e7efc9f54089");

            migrationBuilder.DropColumn(
                name: "Day",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad376a8f",
                column: "ConcurrencyStamp",
                value: "50e9747d-78ec-4753-b46c-e80df1ab8f1e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd586a8f",
                column: "ConcurrencyStamp",
                value: "48bedf0d-425a-43e0-924d-fabf78d5041d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "483c800b-76bd-49e6-9138-4798fcef1597", "66c2b9f1-bbaf-4a73-8087-0798921748e6", "User", "User" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e3668660-0309-445d-a13b-1d17baf9b444", "AQAAAAEAACcQAAAAEMCTM3hWgid4FdcKYLuyhFIuSAmP9PRck/d7BlECwbWjOE6FGreGf/W9Pipi9ycNkg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a50ze710",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7bc4dc23-2058-4fc2-9404-0172930d4834", "AQAAAAEAACcQAAAAEI+BgdGFGQNbjZaQdPz1hlKKqrFU/e/3Q3ESd+sq+xeJwmEbXBwO7cK1ihPgUijcZA==" });
        }
    }
}
