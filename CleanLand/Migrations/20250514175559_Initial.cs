using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanLand.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

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
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnvironmentalAssets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssetType = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    XLocation = table.Column<double>(type: "float", nullable: false),
                    YLocation = table.Column<double>(type: "float", nullable: false),
                    CriticalityScore = table.Column<double>(type: "float", nullable: false),
                    NGO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsProtectedByLaw = table.Column<bool>(type: "bit", nullable: true),
                    TreesAmount = table.Column<long>(type: "bigint", nullable: true),
                    TonsOfSequesteredToDate = table.Column<double>(type: "float", nullable: true),
                    TonsOfSequesteredPotential = table.Column<double>(type: "float", nullable: true),
                    AverageYearTemperature = table.Column<double>(type: "float", nullable: true),
                    AverageYearHumidity = table.Column<double>(type: "float", nullable: true),
                    FireIncidentsAmount = table.Column<int>(type: "int", nullable: true),
                    TerritorialCommunity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Settlement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Length = table.Column<double>(type: "float", nullable: true),
                    Width = table.Column<double>(type: "float", nullable: true),
                    Depth = table.Column<double>(type: "float", nullable: true),
                    WaterLevel = table.Column<double>(type: "float", nullable: true),
                    Volume = table.Column<double>(type: "float", nullable: true),
                    LeasedArea = table.Column<double>(type: "float", nullable: true),
                    CadastralNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WaterSurfaceArea = table.Column<double>(type: "float", nullable: true),
                    IsDrainable = table.Column<bool>(type: "bit", nullable: true),
                    HasHydraulicStructure = table.Column<bool>(type: "bit", nullable: true),
                    HydraulicStructureOwner = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LesseeId = table.Column<int>(type: "int", nullable: true),
                    LeaseAgreementId = table.Column<int>(type: "int", nullable: true),
                    WaterUsagePermitId = table.Column<int>(type: "int", nullable: true),
                    River = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Basin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImposedFines = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ImposedDamages = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CollectedFines = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CollectedDamages = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    WaterQualityIndex = table.Column<double>(type: "float", nullable: true),
                    OxygenSaturation = table.Column<double>(type: "float", nullable: true),
                    PollutantConcentration = table.Column<double>(type: "float", nullable: true),
                    IsEutrophicated = table.Column<bool>(type: "bit", nullable: true),
                    AlgalBloomFrequency = table.Column<int>(type: "int", nullable: true),
                    HasAgricultureNearby = table.Column<bool>(type: "bit", nullable: true),
                    HasIndustryNearby = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnvironmentalAssets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnvironmentalAssets_LeaseAgreements_LeaseAgreementId",
                        column: x => x.LeaseAgreementId,
                        principalTable: "LeaseAgreements",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EnvironmentalAssets_Lessees_LesseeId",
                        column: x => x.LesseeId,
                        principalTable: "Lessees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EnvironmentalAssets_WaterUsagePermits_WaterUsagePermitId",
                        column: x => x.WaterUsagePermitId,
                        principalTable: "WaterUsagePermits",
                        principalColumn: "Id");
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
                        name: "FK_DeforestationDatas_EnvironmentalAssets_ForestId",
                        column: x => x.ForestId,
                        principalTable: "EnvironmentalAssets",
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
                        name: "FK_ForestTreeSpecie_EnvironmentalAssets_ForestId",
                        column: x => x.ForestId,
                        principalTable: "EnvironmentalAssets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ForestTreeSpecie_TreeSpecies_TreeSpeciesId",
                        column: x => x.TreeSpeciesId,
                        principalTable: "TreeSpecies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Issues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsResolved = table.Column<bool>(type: "bit", nullable: false),
                    isAccepted = table.Column<bool>(type: "bit", nullable: false),
                    ForestId = table.Column<int>(type: "int", nullable: true),
                    PondId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Issues_EnvironmentalAssets_ForestId",
                        column: x => x.ForestId,
                        principalTable: "EnvironmentalAssets",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Issues_EnvironmentalAssets_PondId",
                        column: x => x.PondId,
                        principalTable: "EnvironmentalAssets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Vacancies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Wage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NeededPeople = table.Column<int>(type: "int", nullable: false),
                    AppliedPeople = table.Column<int>(type: "int", nullable: false),
                    ObjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacancies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vacancies_EnvironmentalAssets_ObjectId",
                        column: x => x.ObjectId,
                        principalTable: "EnvironmentalAssets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Volunteers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AppliedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VacancyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Volunteers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Volunteers_Vacancies_VacancyId",
                        column: x => x.VacancyId,
                        principalTable: "Vacancies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "EnvironmentalAssets",
                columns: new[] { "Id", "AssetType", "AverageYearHumidity", "AverageYearTemperature", "CriticalityScore", "Description", "District", "FireIncidentsAmount", "IsProtectedByLaw", "NGO", "Name", "TonsOfSequesteredPotential", "TonsOfSequesteredToDate", "TreesAmount", "XLocation", "YLocation" },
                values: new object[] { 1, "Forest", 0.0, 27.5, 8.5, "One of the world's largest and most diverse forests.", "Amazons", 50, true, "Amazon Conservation Association", "Amazon Rainforest", 5000000000.0, 1000000000.0, 390000000000L, -3.4653, -62.215899999999998 });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DeforestationDatas_ForestId",
                table: "DeforestationDatas",
                column: "ForestId");

            migrationBuilder.CreateIndex(
                name: "IX_EnvironmentalAssets_LeaseAgreementId",
                table: "EnvironmentalAssets",
                column: "LeaseAgreementId");

            migrationBuilder.CreateIndex(
                name: "IX_EnvironmentalAssets_LesseeId",
                table: "EnvironmentalAssets",
                column: "LesseeId");

            migrationBuilder.CreateIndex(
                name: "IX_EnvironmentalAssets_WaterUsagePermitId",
                table: "EnvironmentalAssets",
                column: "WaterUsagePermitId");

            migrationBuilder.CreateIndex(
                name: "IX_ForestTreeSpecie_TreeSpeciesId",
                table: "ForestTreeSpecie",
                column: "TreeSpeciesId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_ForestId",
                table: "Issues",
                column: "ForestId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_PondId",
                table: "Issues",
                column: "PondId");

            migrationBuilder.CreateIndex(
                name: "IX_Vacancies_ObjectId",
                table: "Vacancies",
                column: "ObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Volunteers_VacancyId",
                table: "Volunteers",
                column: "VacancyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "DeforestationDatas");

            migrationBuilder.DropTable(
                name: "ForestTreeSpecie");

            migrationBuilder.DropTable(
                name: "Issues");

            migrationBuilder.DropTable(
                name: "Volunteers");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "TreeSpecies");

            migrationBuilder.DropTable(
                name: "Vacancies");

            migrationBuilder.DropTable(
                name: "EnvironmentalAssets");

            migrationBuilder.DropTable(
                name: "LeaseAgreements");

            migrationBuilder.DropTable(
                name: "Lessees");

            migrationBuilder.DropTable(
                name: "WaterUsagePermits");
        }
    }
}
