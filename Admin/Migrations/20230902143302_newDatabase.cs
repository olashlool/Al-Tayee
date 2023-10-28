using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Admin.Migrations
{
    public partial class newDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "975f8e9b-2b93-4827-88d9-29644994ee01");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "483c800b-76bd-49e6-9138-4798fcef1597");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad376a8f",
                column: "ConcurrencyStamp",
                value: "b007b996-9292-4bdf-bd96-699b710c2982");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd586a8f",
                column: "ConcurrencyStamp",
                value: "57214cd1-3a67-4bc5-a3ea-de7b590b0abc");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "975f8e9b-2b93-4827-88d9-29644994ee01", "3d750f73-26a4-4f64-8ef9-2a2b2dea46c7", "User", "User" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "effd2bcb-2f83-4366-ad16-ad153b473763", "AQAAAAEAACcQAAAAECQO1ecr7Kt5yMwTvZoorNSBzKET3gnTs/raFNs9h6hV2LEE609FpWNDCuDpgnQV+g==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a50ze710",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a6a89570-5a0e-42ac-80a9-06833bd5573b", "AQAAAAEAACcQAAAAEE/qTz5z6/ot1WoupKczhKSl5W+2mTYoSjd1IgDRJ1XIM9l2w5GNPneBU2IVPFD95g==" });
        }
    }
}
