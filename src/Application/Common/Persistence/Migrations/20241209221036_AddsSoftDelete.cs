using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Common.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddsSoftDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "deleted",
                table: "section_configs",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "deleted_at",
                table: "section_configs",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "deleted_by",
                table: "section_configs",
                type: "uuid",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "deleted",
                table: "section_configs");

            migrationBuilder.DropColumn(
                name: "deleted_at",
                table: "section_configs");

            migrationBuilder.DropColumn(
                name: "deleted_by",
                table: "section_configs");
        }
    }
}
