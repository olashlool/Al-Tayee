using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Admin.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d4cc3902-29cf-4a6d-a87f-bea0a0f05574");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad376a8f",
                column: "ConcurrencyStamp",
                value: "6672b653-348d-4b05-bf54-7a27a109a3d7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd586a8f",
                column: "ConcurrencyStamp",
                value: "e3cae04c-1c20-4031-9e1d-862b045406c9");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "19782f3a-0806-4b9e-8fa6-9ff6eecd3d95", "30faaf1f-3389-4ae9-a1a7-83c90e3944a3", "User", "User" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f3ee22d2-82bc-4221-9764-6be546c1df04", "AQAAAAEAACcQAAAAEBhg3odl3ouN4cosvGO89wVvlYS8IL8qC6Ddyt/a/XZesyfzLqlV/4Oz3fgBuN6+Kw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a50ze710",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "935e23b6-969c-411f-8245-e99422d07011", "AQAAAAEAACcQAAAAEFIXv4dtdvHLV4EIECdJx7TAH9c/saFQWPjRzBsixyJWsTacTO92V4q1L2CcYgNXnQ==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "19782f3a-0806-4b9e-8fa6-9ff6eecd3d95");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad376a8f",
                column: "ConcurrencyStamp",
                value: "248a605d-ae45-48e4-b662-559a1a30eaaa");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd586a8f",
                column: "ConcurrencyStamp",
                value: "2504ad63-68b3-4bff-a13f-5b160be53498");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d4cc3902-29cf-4a6d-a87f-bea0a0f05574", "b798be82-4047-410b-a9ab-9ef6173a0b4d", "User", "User" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "94b46ba6-d9af-42ad-ab7b-9f176014cc9f", "AQAAAAEAACcQAAAAEN9CUqUTkHrUBpCcP4roOqwsBCT+s9LV5+bANcakpazeRbiHhm0ooX/m7S3mc/CAcw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a50ze710",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6f195198-867a-43d6-a0d9-2b1910780592", "AQAAAAEAACcQAAAAEAXTt8EnXgKfS/Rqh37io0bvU9ZrIGMtGSZpuc0UtoBZYzzpbVQpeTVu/eABA1OV9w==" });
        }
    }
}
