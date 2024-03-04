using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exchange.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AccessRoleWithIdentit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessRoleId",
                schema: "Menu",
                table: "NavMenu");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                schema: "Menu",
                table: "NavMenu",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_NavMenu_Id",
                schema: "Menu",
                table: "NavMenu",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NavMenu_AspNetRoles_Id",
                schema: "Menu",
                table: "NavMenu",
                column: "Id",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NavMenu_AspNetRoles_Id",
                schema: "Menu",
                table: "NavMenu");

            migrationBuilder.DropIndex(
                name: "IX_NavMenu_Id",
                schema: "Menu",
                table: "NavMenu");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "Menu",
                table: "NavMenu");

            migrationBuilder.AddColumn<int>(
                name: "AccessRoleId",
                schema: "Menu",
                table: "NavMenu",
                type: "int",
                nullable: true);
        }
    }
}
