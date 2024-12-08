using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Common.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ModifiestUniqueConstraintTemplateSection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_section_products_order_product_id",
                table: "section_products");

            migrationBuilder.CreateIndex(
                name: "ix_section_products_order_product_id_template_section_id",
                table: "section_products",
                columns: new[] { "order", "product_id", "template_section_id" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_section_products_order_product_id_template_section_id",
                table: "section_products");

            migrationBuilder.CreateIndex(
                name: "ix_section_products_order_product_id",
                table: "section_products",
                columns: new[] { "order", "product_id" },
                unique: true);
        }
    }
}
