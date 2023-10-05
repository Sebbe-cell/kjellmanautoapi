using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kjellmanautoapi.Migrations
{
    /// <inheritdoc />
    public partial class EquipmentInventoryDto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EquipmentInventory");

            migrationBuilder.CreateTable(
                name: "EquipmentsInventory",
                columns: table => new
                {
                    EquipmentId = table.Column<int>(type: "int", nullable: false),
                    InventoriesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentsInventory", x => new { x.EquipmentId, x.InventoriesId });
                    table.ForeignKey(
                        name: "FK_EquipmentsInventory_Equipments_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipmentsInventory_Inventories_InventoriesId",
                        column: x => x.InventoriesId,
                        principalTable: "Inventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentsInventory_InventoriesId",
                table: "EquipmentsInventory",
                column: "InventoriesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EquipmentsInventory");

            migrationBuilder.CreateTable(
                name: "EquipmentInventory",
                columns: table => new
                {
                    EquipmentId = table.Column<int>(type: "int", nullable: false),
                    InventoriesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentInventory", x => new { x.EquipmentId, x.InventoriesId });
                    table.ForeignKey(
                        name: "FK_EquipmentInventory_Equipments_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipmentInventory_Inventories_InventoriesId",
                        column: x => x.InventoriesId,
                        principalTable: "Inventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentInventory_InventoriesId",
                table: "EquipmentInventory",
                column: "InventoriesId");
        }
    }
}
