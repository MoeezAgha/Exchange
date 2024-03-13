using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exchange.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Updatedexchange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAcceptedOffer",
                schema: "Product",
                table: "Product");

            migrationBuilder.AddColumn<int>(
                name: "AcceptedOfferExchangeOfferId",
                schema: "Product",
                table: "Product",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AcceptedOfferId",
                schema: "Product",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ExchangeOffer",
                schema: "Product",
                columns: table => new
                {
                    ExchangeOfferId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfferDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ExchangeOfferByUserId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeOffer", x => x.ExchangeOfferId);
                    table.ForeignKey(
                        name: "FK_ExchangeOffer_AspNetUsers_ExchangeOfferByUserId",
                        column: x => x.ExchangeOfferByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExchangeOffer_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Product",
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_AcceptedOfferExchangeOfferId",
                schema: "Product",
                table: "Product",
                column: "AcceptedOfferExchangeOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeOffer_ExchangeOfferByUserId",
                schema: "Product",
                table: "ExchangeOffer",
                column: "ExchangeOfferByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeOffer_ProductId",
                schema: "Product",
                table: "ExchangeOffer",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ExchangeOffer_AcceptedOfferExchangeOfferId",
                schema: "Product",
                table: "Product",
                column: "AcceptedOfferExchangeOfferId",
                principalSchema: "Product",
                principalTable: "ExchangeOffer",
                principalColumn: "ExchangeOfferId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ExchangeOffer_AcceptedOfferExchangeOfferId",
                schema: "Product",
                table: "Product");

            migrationBuilder.DropTable(
                name: "ExchangeOffer",
                schema: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_AcceptedOfferExchangeOfferId",
                schema: "Product",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "AcceptedOfferExchangeOfferId",
                schema: "Product",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "AcceptedOfferId",
                schema: "Product",
                table: "Product");

            migrationBuilder.AddColumn<bool>(
                name: "IsAcceptedOffer",
                schema: "Product",
                table: "Product",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
