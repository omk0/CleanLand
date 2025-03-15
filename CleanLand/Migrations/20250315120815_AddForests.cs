using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanLand.Migrations
{
    /// <inheritdoc />
    public partial class AddForests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Forests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    XLocation = table.Column<double>(type: "float", nullable: false),
                    YLocation = table.Column<double>(type: "float", nullable: false),
                    NGO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsProtectedByLaw = table.Column<bool>(type: "bit", nullable: false),
                    TreesAmount = table.Column<long>(type: "bigint", nullable: true),
                    TonsOfSequesteredToDate = table.Column<double>(type: "float", nullable: false),
                    TonsOfSequesteredPotential = table.Column<double>(type: "float", nullable: false),
                    AverageYearTemperature = table.Column<double>(type: "float", nullable: false),
                    AverageYearHumidity = table.Column<double>(type: "float", nullable: false),
                    FireIncidentsAmount = table.Column<int>(type: "int", nullable: false),
                    CriticalityScore = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TreeSpecies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScientificName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommonName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxonomicClassification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsEndemic = table.Column<bool>(type: "bit", nullable: false),
                    IsInvasive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreeSpecies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeforestationDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Area = table.Column<double>(type: "float", nullable: false),
                    ForestId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeforestationDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeforestationDatas_Forests_ForestId",
                        column: x => x.ForestId,
                        principalTable: "Forests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ForestTreeSpecie",
                columns: table => new
                {
                    ForestId = table.Column<int>(type: "int", nullable: false),
                    TreeSpeciesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForestTreeSpecie", x => new { x.ForestId, x.TreeSpeciesId });
                    table.ForeignKey(
                        name: "FK_ForestTreeSpecie_Forests_ForestId",
                        column: x => x.ForestId,
                        principalTable: "Forests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ForestTreeSpecie_TreeSpecies_TreeSpeciesId",
                        column: x => x.TreeSpeciesId,
                        principalTable: "TreeSpecies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Forests",
                columns: new[] { "Id", "AverageYearHumidity", "AverageYearTemperature", "CriticalityScore", "Description", "FireIncidentsAmount", "IsProtectedByLaw", "NGO", "Name", "TonsOfSequesteredPotential", "TonsOfSequesteredToDate", "TreesAmount", "XLocation", "YLocation" },
                values: new object[] { 1, 0.0, 27.5, 8.5, "One of the world's largest and most diverse forests.", 50, true, "Amazon Conservation Association", "Amazon Rainforest", 5000000000.0, 1000000000.0, 390000000000L, -3.4653, -62.215899999999998 });

            migrationBuilder.CreateIndex(
                name: "IX_DeforestationDatas_ForestId",
                table: "DeforestationDatas",
                column: "ForestId");

            migrationBuilder.CreateIndex(
                name: "IX_ForestTreeSpecie_TreeSpeciesId",
                table: "ForestTreeSpecie",
                column: "TreeSpeciesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeforestationDatas");

            migrationBuilder.DropTable(
                name: "ForestTreeSpecie");

            migrationBuilder.DropTable(
                name: "Forests");

            migrationBuilder.DropTable(
                name: "TreeSpecies");
        }
    }
}
