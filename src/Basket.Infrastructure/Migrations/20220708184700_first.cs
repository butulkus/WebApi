using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Basket.Infrastructure.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "basketItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basketItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "customerBaskets",
                columns: table => new
                {
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customerBaskets", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "BasketItemCustomerBasket",
                columns: table => new
                {
                    GetCustomerBasketsCustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    GetItemsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasketItemCustomerBasket", x => new { x.GetCustomerBasketsCustomerId, x.GetItemsId });
                    table.ForeignKey(
                        name: "FK_BasketItemCustomerBasket_basketItems_GetItemsId",
                        column: x => x.GetItemsId,
                        principalTable: "basketItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BasketItemCustomerBasket_customerBaskets_GetCustomerBaskets~",
                        column: x => x.GetCustomerBasketsCustomerId,
                        principalTable: "customerBaskets",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_BasketItemCustomerBasket_GetItemsId",
                table: "BasketItemCustomerBasket",
                column: "GetItemsId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItemCustomerBasket1_customerBasketsCustomerId",
                table: "BasketItemCustomerBasket1",
                column: "customerBasketsCustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BasketItemCustomerBasket");

            migrationBuilder.DropTable(
                name: "BasketItemCustomerBasket1");

            migrationBuilder.DropTable(
                name: "basketItems");

            migrationBuilder.DropTable(
                name: "customerBaskets");
        }
    }
}
