using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exchange.DAL.Migrations
{
    /// <inheritdoc />
    public partial class updatedrelationshi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeOffers_AspNetUsers_UserId",
                table: "ExchangeOffers");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_UserId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Products",
                newName: "ProductCreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Products_UserId",
                table: "Products",
                newName: "IX_Products_ProductCreatedById");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ExchangeOffers",
                newName: "ExchangeOfferByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ExchangeOffers_UserId",
                table: "ExchangeOffers",
                newName: "IX_ExchangeOffers_ExchangeOfferByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeOffers_AspNetUsers_ExchangeOfferByUserId",
                table: "ExchangeOffers",
                column: "ExchangeOfferByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_ProductCreatedById",
                table: "Products",
                column: "ProductCreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeOffers_AspNetUsers_ExchangeOfferByUserId",
                table: "ExchangeOffers");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_ProductCreatedById",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "ProductCreatedById",
                table: "Products",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ProductCreatedById",
                table: "Products",
                newName: "IX_Products_UserId");

            migrationBuilder.RenameColumn(
                name: "ExchangeOfferByUserId",
                table: "ExchangeOffers",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ExchangeOffers_ExchangeOfferByUserId",
                table: "ExchangeOffers",
                newName: "IX_ExchangeOffers_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeOffers_AspNetUsers_UserId",
                table: "ExchangeOffers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_UserId",
                table: "Products",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
