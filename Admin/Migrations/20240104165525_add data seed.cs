using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Admin.Migrations
{
    public partial class adddataseed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e9eba5ec-71fa-4ec4-8332-2620beab0047");

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

            migrationBuilder.InsertData(
                table: "GridImage",
                columns: new[] { "Id", "ImageUrl" },
                values: new object[,]
                {
                    { 1, "/images/grid/des-img-1.png" },
                    { 2, "/images/grid/des-img-2.png" },
                    { 3, "/images/grid/des-img-3.png" },
                    { 4, "/images/grid/des-img-4.png" },
                    { 5, "/images/grid/des-img-5.png" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5a2d5554-d6d5-4de6-9daa-d936c2c0cc44");

            migrationBuilder.DeleteData(
                table: "GridImage",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "GridImage",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "GridImage",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "GridImage",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "GridImage",
                keyColumn: "Id",
                keyValue: 5);

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
    }
}
