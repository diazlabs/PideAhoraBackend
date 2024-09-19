using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Common.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddProductType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "tenant_templates",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "config_type",
                table: "tenant_configs",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "visible",
                table: "tenant_configs",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "product_type",
                table: "products",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name",
                table: "tenant_templates");

            migrationBuilder.DropColumn(
                name: "config_type",
                table: "tenant_configs");

            migrationBuilder.DropColumn(
                name: "visible",
                table: "tenant_configs");

            migrationBuilder.DropColumn(
                name: "product_type",
                table: "products");
        }
    }
}
