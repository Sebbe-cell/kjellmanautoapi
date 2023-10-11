using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kjellmanautoapi.Migrations
{
    /// <inheritdoc />
    public partial class Facts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Facts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FactsInventory",
                columns: table => new
                {
                    FactsId = table.Column<int>(type: "int", nullable: false),
                    InventoriesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FactsInventory", x => new { x.FactsId, x.InventoriesId });
                    table.ForeignKey(
                        name: "FK_FactsInventory_Facts_FactsId",
                        column: x => x.FactsId,
                        principalTable: "Facts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FactsInventory_Inventories_InventoriesId",
                        column: x => x.InventoriesId,
                        principalTable: "Inventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FactsInventory_InventoriesId",
                table: "FactsInventory",
                column: "InventoriesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FactsInventory");

            migrationBuilder.DropTable(
                name: "Facts");
        }
    }
}
