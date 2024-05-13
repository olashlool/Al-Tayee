using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Admin.Migrations
{
    public partial class updateDataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bb600dad-a9a7-4460-8222-4ecb88cc3813");

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("d4cc25bb-720c-4144-817d-7d42811ee83d"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("2821547d-4b90-418c-ad62-1f82b661c984"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("b3159b47-f4b4-48db-bb83-f4933a98bdbf"));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad376a8f",
                column: "ConcurrencyStamp",
                value: "c9b06c14-070d-467c-b1e2-28dfb7bd006a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd586a8f",
                column: "ConcurrencyStamp",
                value: "a6ad464e-8b87-4e79-9d8e-aba5db86098b");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "39871f99-a24b-498d-84c8-4065ef4448c5", "3c6410e7-9724-4f67-9077-6fab7a63a2d3", "User", "User" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "99498cd1-4998-418c-af0e-f65d9a997a20", "AQAAAAEAACcQAAAAEBQCHJGd0p2Os6jD8nZPUSVAOBLqc9S5rtsieXqgf5hEsCFhzsfYmO6VzZBXZU6g9g==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a50ze710",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "02a4cb66-c01f-4795-b7f4-325ae2c72c12", "AQAAAAEAACcQAAAAEAOTDsyLptHfsDcnYBDT23cJZJGL6Fuu7gCTrzeCB3lp3SKLLHlcdRtBAEAZwADf4Q==" });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "ImageUrl", "NameAr", "NameEn" },
                values: new object[] { new Guid("e6701eaf-0346-4be9-a779-dc2ac17ef5ac"), "/images/Initial_DB/AGRADO.png", "أغرادو", "AGRADO" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AltImage", "BaseImage", "BrandsId", "DescriptionAr", "DescriptionEn", "IsFeatured", "NameAr", "NameEn", "Price", "Size" },
                values: new object[] { new Guid("fb9a69a2-9803-494f-96d7-bea456865bae"), "Initial_DB/AGRADO_Product02.png", "Initial_DB/AGRADO_Product.png", new Guid("e6701eaf-0346-4be9-a779-dc2ac17ef5ac"), "شامبو شعر مع كيراتين اضافية منظف منعش الذي يحفز فروة الرأس ويترك شعور الشعر نظيفة وجديدة وصحية.", "Hair shampoo with extra keratin is a refreshing cleanser that stimulates the scalp and leaves hair feeling clean, fresh and healthy.", false, "شامبو للشعر", "Hair Shampoo", 1.0, "120" });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "ImageName", "ProductsId" },
                values: new object[] { new Guid("05279137-8113-4703-bc5e-75d2245415be"), "Initial_DB/AGRADO_Product02.png", new Guid("fb9a69a2-9803-494f-96d7-bea456865bae") });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "ImageName", "ProductsId" },
                values: new object[] { new Guid("2ea67f82-0f38-43dd-86a9-e87c300e61a4"), "Initial_DB/AGRADO_Product.png", new Guid("fb9a69a2-9803-494f-96d7-bea456865bae") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "39871f99-a24b-498d-84c8-4065ef4448c5");

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("05279137-8113-4703-bc5e-75d2245415be"));

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("2ea67f82-0f38-43dd-86a9-e87c300e61a4"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("fb9a69a2-9803-494f-96d7-bea456865bae"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("e6701eaf-0346-4be9-a779-dc2ac17ef5ac"));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad376a8f",
                column: "ConcurrencyStamp",
                value: "bdbc180d-66f1-4bcb-ac2b-bfd5767af81c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd586a8f",
                column: "ConcurrencyStamp",
                value: "65b596dd-a6b1-474d-adfe-91b0ded4f9bc");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bb600dad-a9a7-4460-8222-4ecb88cc3813", "cadc1031-58c9-4daa-9ba7-9435aba3092c", "User", "User" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7a106057-fa66-4561-b8d9-ee145fd881c5", "AQAAAAEAACcQAAAAENVRuv2Ec7MnGx5sTbTQi10Hc9ByMOAN3rpn9VMJKaDbftg7DxjwHePeHrSr0c29+g==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a50ze710",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "71be468a-6092-465c-825d-da2304876426", "AQAAAAEAACcQAAAAEN3szG4sfqlEc+2vjxw0+JfFa0npf01PFHYEfKR5YXnqpXsuKbhARQ3WD7VfKoP2Gw==" });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "ImageUrl", "NameAr", "NameEn" },
                values: new object[] { new Guid("b3159b47-f4b4-48db-bb83-f4933a98bdbf"), "/images/Initial_DB/AGRADO.png", "أغرادو", "AGRADO" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AltImage", "BaseImage", "BrandsId", "DescriptionAr", "DescriptionEn", "IsFeatured", "NameAr", "NameEn", "Price", "Size" },
                values: new object[] { new Guid("2821547d-4b90-418c-ad62-1f82b661c984"), "Initial_DB/AGRADO_Product02.png", "Initial_DB/AGRADO_Product.png", new Guid("b3159b47-f4b4-48db-bb83-f4933a98bdbf"), "شامبو شعر مع كيراتين اضافية منظف منعش الذي يحفز فروة الرأس ويترك شعور الشعر نظيفة وجديدة وصحية.", "Hair shampoo with extra keratin is a refreshing cleanser that stimulates the scalp and leaves hair feeling clean, fresh and healthy.", false, "شامبو للشعر", "Hair Shampoo", 1.0, "120" });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "ImageName", "ProductsId" },
                values: new object[] { new Guid("d4cc25bb-720c-4144-817d-7d42811ee83d"), "Initial_DB/AGRADO_Product.png", new Guid("2821547d-4b90-418c-ad62-1f82b661c984") });
        }
    }
}
