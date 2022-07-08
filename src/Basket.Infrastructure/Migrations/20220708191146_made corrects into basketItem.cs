using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Basket.Infrastructure.Migrations
{
    public partial class madecorrectsintobasketItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CurrentPrice",
                table: "basketItems",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ItemHashCode",
                table: "basketItems",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "OldPrice",
                table: "basketItems",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "basketItems",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentPrice",
                table: "basketItems");

            migrationBuilder.DropColumn(
                name: "ItemHashCode",
                table: "basketItems");

            migrationBuilder.DropColumn(
                name: "OldPrice",
                table: "basketItems");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "basketItems");
        }
    }
}
