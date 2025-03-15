using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanLand.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LeaseAgreements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TermInYears = table.Column<int>(type: "int", nullable: false),
                    EconomicActivities = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaseAgreements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lessees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdentificationCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WaterUsagePermits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TermInYears = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WaterUsagePermits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ponds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TerritorialCommunity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Settlement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Coordinates = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Length = table.Column<double>(type: "float", nullable: false),
                    Width = table.Column<double>(type: "float", nullable: false),
                    Depth = table.Column<double>(type: "float", nullable: false),
                    WaterLevel = table.Column<double>(type: "float", nullable: false),
                    Volume = table.Column<double>(type: "float", nullable: false),
                    LeasedArea = table.Column<double>(type: "float", nullable: false),
                    CadastralNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WaterSurfaceArea = table.Column<double>(type: "float", nullable: false),
                    IsDrainable = table.Column<bool>(type: "bit", nullable: false),
                    HasHydraulicStructure = table.Column<bool>(type: "bit", nullable: false),
                    HydraulicStructureOwner = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LesseeId = table.Column<int>(type: "int", nullable: false),
                    LeaseAgreementId = table.Column<int>(type: "int", nullable: false),
                    WaterUsagePermitId = table.Column<int>(type: "int", nullable: false),
                    River = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Basin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Issues = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ponds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ponds_LeaseAgreements_LeaseAgreementId",
                        column: x => x.LeaseAgreementId,
                        principalTable: "LeaseAgreements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ponds_Lessees_LesseeId",
                        column: x => x.LesseeId,
                        principalTable: "Lessees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ponds_WaterUsagePermits_WaterUsagePermitId",
                        column: x => x.WaterUsagePermitId,
                        principalTable: "WaterUsagePermits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ponds_LeaseAgreementId",
                table: "Ponds",
                column: "LeaseAgreementId");

            migrationBuilder.CreateIndex(
                name: "IX_Ponds_LesseeId",
                table: "Ponds",
                column: "LesseeId");

            migrationBuilder.CreateIndex(
                name: "IX_Ponds_WaterUsagePermitId",
                table: "Ponds",
                column: "WaterUsagePermitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ponds");

            migrationBuilder.DropTable(
                name: "LeaseAgreements");

            migrationBuilder.DropTable(
                name: "Lessees");

            migrationBuilder.DropTable(
                name: "WaterUsagePermits");
        }
    }
}
