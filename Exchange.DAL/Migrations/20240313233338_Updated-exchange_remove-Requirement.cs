using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exchange.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Updatedexchange_removeRequirement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Category_CategoryId",
                schema: "Product",
                table: "Product");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                schema: "Product",
                table: "Product",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AcceptedOfferId",
                schema: "Product",
                table: "Product",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Category_CategoryId",
                schema: "Product",
                table: "Product",
                column: "CategoryId",
                principalSchema: "Product",
                principalTable: "Category",
                principalColumn: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Category_CategoryId",
                schema: "Product",
                table: "Product");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                schema: "Product",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AcceptedOfferId",
                schema: "Product",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Category_CategoryId",
                schema: "Product",
                table: "Product",
                column: "CategoryId",
                principalSchema: "Product",
                principalTable: "Category",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
