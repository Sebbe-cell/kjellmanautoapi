using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kjellmanautoapi.Migrations
{
    /// <inheritdoc />
    public partial class InventoryUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GearBox",
                table: "Inventories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ModelYear",
                table: "Inventories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Propellent",
                table: "Inventories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GearBox",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "ModelYear",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "Propellent",
                table: "Inventories");
        }
    }
}
