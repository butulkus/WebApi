using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalog.DataContext.Migrations
{
    public partial class updatetablesname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankType_Bank_CatalogBrandId",
                table: "BankType");

            migrationBuilder.DropForeignKey(
                name: "FK_BankType_ContactPhones_CatalogTypeId",
                table: "BankType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContactPhones",
                table: "ContactPhones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BankType",
                table: "BankType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bank",
                table: "Bank");

            migrationBuilder.RenameTable(
                name: "ContactPhones",
                newName: "CatalogType");

            migrationBuilder.RenameTable(
                name: "BankType",
                newName: "CatalogItem");

            migrationBuilder.RenameTable(
                name: "Bank",
                newName: "CatalogBrand");

            migrationBuilder.RenameIndex(
                name: "IX_BankType_ItemHashCode",
                table: "CatalogItem",
                newName: "IX_CatalogItem_ItemHashCode");

            migrationBuilder.RenameIndex(
                name: "IX_BankType_CatalogTypeId",
                table: "CatalogItem",
                newName: "IX_CatalogItem_CatalogTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_BankType_CatalogBrandId",
                table: "CatalogItem",
                newName: "IX_CatalogItem_CatalogBrandId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CatalogType",
                table: "CatalogType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CatalogItem",
                table: "CatalogItem",
                column: "ItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CatalogBrand",
                table: "CatalogBrand",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CatalogItem_CatalogBrand_CatalogBrandId",
                table: "CatalogItem",
                column: "CatalogBrandId",
                principalTable: "CatalogBrand",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CatalogItem_CatalogType_CatalogTypeId",
                table: "CatalogItem",
                column: "CatalogTypeId",
                principalTable: "CatalogType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CatalogItem_CatalogBrand_CatalogBrandId",
                table: "CatalogItem");

            migrationBuilder.DropForeignKey(
                name: "FK_CatalogItem_CatalogType_CatalogTypeId",
                table: "CatalogItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CatalogType",
                table: "CatalogType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CatalogItem",
                table: "CatalogItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CatalogBrand",
                table: "CatalogBrand");

            migrationBuilder.RenameTable(
                name: "CatalogType",
                newName: "ContactPhones");

            migrationBuilder.RenameTable(
                name: "CatalogItem",
                newName: "BankType");

            migrationBuilder.RenameTable(
                name: "CatalogBrand",
                newName: "Bank");

            migrationBuilder.RenameIndex(
                name: "IX_CatalogItem_ItemHashCode",
                table: "BankType",
                newName: "IX_BankType_ItemHashCode");

            migrationBuilder.RenameIndex(
                name: "IX_CatalogItem_CatalogTypeId",
                table: "BankType",
                newName: "IX_BankType_CatalogTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_CatalogItem_CatalogBrandId",
                table: "BankType",
                newName: "IX_BankType_CatalogBrandId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContactPhones",
                table: "ContactPhones",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BankType",
                table: "BankType",
                column: "ItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bank",
                table: "Bank",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BankType_Bank_CatalogBrandId",
                table: "BankType",
                column: "CatalogBrandId",
                principalTable: "Bank",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BankType_ContactPhones_CatalogTypeId",
                table: "BankType",
                column: "CatalogTypeId",
                principalTable: "ContactPhones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
