using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Atlas.Migrations
{
    /// <inheritdoc />
    public partial class AddProductSkuAndColor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Products",
                type: "character varying(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SKU",
                table: "Products",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SKU",
                table: "Products",
                column: "SKU",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_SKU",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SKU",
                table: "Products");
        }
    }
}
