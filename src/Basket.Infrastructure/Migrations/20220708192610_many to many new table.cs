using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Basket.Infrastructure.Migrations
{
    public partial class manytomanynewtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketItemCustomerBasket_basketItems_GetItemsId",
                table: "BasketItemCustomerBasket");

            migrationBuilder.DropForeignKey(
                name: "FK_BasketItemCustomerBasket_customerBaskets_GetCustomerBaskets~",
                table: "BasketItemCustomerBasket");

            migrationBuilder.DropTable(
                name: "BasketItemCustomerBasket1");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BasketItemCustomerBasket",
                table: "BasketItemCustomerBasket");

            migrationBuilder.RenameTable(
                name: "BasketItemCustomerBasket",
                newName: "PotentialPurchases");

            migrationBuilder.RenameColumn(
                name: "GetItemsId",
                table: "PotentialPurchases",
                newName: "customerBasketsCustomerId");

            migrationBuilder.RenameColumn(
                name: "GetCustomerBasketsCustomerId",
                table: "PotentialPurchases",
                newName: "ItemsId");

            migrationBuilder.RenameIndex(
                name: "IX_BasketItemCustomerBasket_GetItemsId",
                table: "PotentialPurchases",
                newName: "IX_PotentialPurchases_customerBasketsCustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PotentialPurchases",
                table: "PotentialPurchases",
                columns: new[] { "ItemsId", "customerBasketsCustomerId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PotentialPurchases_basketItems_ItemsId",
                table: "PotentialPurchases",
                column: "ItemsId",
                principalTable: "basketItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PotentialPurchases_customerBaskets_customerBasketsCustomerId",
                table: "PotentialPurchases",
                column: "customerBasketsCustomerId",
                principalTable: "customerBaskets",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PotentialPurchases_basketItems_ItemsId",
                table: "PotentialPurchases");

            migrationBuilder.DropForeignKey(
                name: "FK_PotentialPurchases_customerBaskets_customerBasketsCustomerId",
                table: "PotentialPurchases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PotentialPurchases",
                table: "PotentialPurchases");

            migrationBuilder.RenameTable(
                name: "PotentialPurchases",
                newName: "BasketItemCustomerBasket");

            migrationBuilder.RenameColumn(
                name: "customerBasketsCustomerId",
                table: "BasketItemCustomerBasket",
                newName: "GetItemsId");

            migrationBuilder.RenameColumn(
                name: "ItemsId",
                table: "BasketItemCustomerBasket",
                newName: "GetCustomerBasketsCustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_PotentialPurchases_customerBasketsCustomerId",
                table: "BasketItemCustomerBasket",
                newName: "IX_BasketItemCustomerBasket_GetItemsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BasketItemCustomerBasket",
                table: "BasketItemCustomerBasket",
                columns: new[] { "GetCustomerBasketsCustomerId", "GetItemsId" });

            migrationBuilder.CreateTable(
                name: "BasketItemCustomerBasket1",
                columns: table => new
                {
                    ItemsId = table.Column<Guid>(type: "uuid", nullable: false),
                    customerBasketsCustomerId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasketItemCustomerBasket1", x => new { x.ItemsId, x.customerBasketsCustomerId });
                    table.ForeignKey(
                        name: "FK_BasketItemCustomerBasket1_basketItems_ItemsId",
                        column: x => x.ItemsId,
                        principalTable: "basketItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BasketItemCustomerBasket1_customerBaskets_customerBasketsCu~",
                        column: x => x.customerBasketsCustomerId,
                        principalTable: "customerBaskets",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BasketItemCustomerBasket1_customerBasketsCustomerId",
                table: "BasketItemCustomerBasket1",
                column: "customerBasketsCustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItemCustomerBasket_basketItems_GetItemsId",
                table: "BasketItemCustomerBasket",
                column: "GetItemsId",
                principalTable: "basketItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItemCustomerBasket_customerBaskets_GetCustomerBaskets~",
                table: "BasketItemCustomerBasket",
                column: "GetCustomerBasketsCustomerId",
                principalTable: "customerBaskets",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
