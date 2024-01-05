using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Admin.Migrations
{
    public partial class AddBackgroundSection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5a2d5554-d6d5-4de6-9daa-d936c2c0cc44");

            migrationBuilder.CreateTable(
                name: "BackgroundSection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitleAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BackgroundSection", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad376a8f",
                column: "ConcurrencyStamp",
                value: "f6827384-fc89-4c49-804a-0ec61775fd10");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd586a8f",
                column: "ConcurrencyStamp",
                value: "afb41841-ee11-417e-a039-c556c1c6ca12");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cbb8d6fd-bf24-4594-a6f5-36744ceb050c", "ed5293f8-bd89-462d-b446-7bbeab6af42f", "User", "User" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "55a5911b-c0ad-4746-86d3-adb5b9682617", "AQAAAAEAACcQAAAAEDxJyyqz7Mi3Od9DykY60KgzWldzPpekoW8qQFabqD2pWIR51Ex+0qCqq2z0ijk2+A==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a50ze710",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ec6c13f7-aa37-42e1-a5b3-fb0e32f19ee5", "AQAAAAEAACcQAAAAELHjogjJ05qLr4ppIH8CHLiQ8JqdsxA/safKqbDtWkWUisBdIjnAsZyGVtch7hezZQ==" });

            migrationBuilder.InsertData(
                table: "BackgroundSection",
                columns: new[] { "Id", "DescriptionAr", "DescriptionEn", "ImageUrl", "TitleAr", "TitleEn" },
                values: new object[] { 1, "بالطبع نحن", "NATURALLY WE", "/images/Slider-4.png", "جمال طبيعي", "NATURAL BEAUTY" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BackgroundSection");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cbb8d6fd-bf24-4594-a6f5-36744ceb050c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad376a8f",
                column: "ConcurrencyStamp",
                value: "9ee1a408-ceba-46ea-bd26-e7ce92ac7cd6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd586a8f",
                column: "ConcurrencyStamp",
                value: "98fb6de2-bd5d-4fb3-9888-61d346697a8a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5a2d5554-d6d5-4de6-9daa-d936c2c0cc44", "7ad73b74-4a6b-440c-88eb-05cc67acf532", "User", "User" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "04ada24c-d0de-498c-ad77-d0e6f9be7990", "AQAAAAEAACcQAAAAENEAZdfonltPoSALKsKn+kjgJw6c5rlZNLjVYObzXh7Sr3m9GEDd4cpLMMPtHJMwVg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a50ze710",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b575bec2-aaf0-4414-a440-3d486e95d34d", "AQAAAAEAACcQAAAAEBw1EBcWdWWjnyJkupnEyW2N9R8uVw9pgbvvtBt6u6XJyx8TkgVXUnEO5IqbiHQaBA==" });
        }
    }
}
