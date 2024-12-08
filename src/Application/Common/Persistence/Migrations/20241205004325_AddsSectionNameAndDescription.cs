using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Common.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddsSectionNameAndDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "section_description",
                table: "template_sections",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "section_name",
                table: "template_sections",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "section_description",
                table: "template_sections");

            migrationBuilder.DropColumn(
                name: "section_name",
                table: "template_sections");
        }
    }
}
