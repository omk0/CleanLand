using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanLand.Migrations
{
    /// <inheritdoc />
    public partial class Migration5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ponds_LeaseAgreements_LeaseAgreementId",
                table: "Ponds");

            migrationBuilder.DropForeignKey(
                name: "FK_Ponds_Lessees_LesseeId",
                table: "Ponds");

            migrationBuilder.DropForeignKey(
                name: "FK_Ponds_WaterUsagePermits_WaterUsagePermitId",
                table: "Ponds");

            migrationBuilder.DropColumn(
                name: "Coordinates",
                table: "Ponds");

            migrationBuilder.AlterColumn<int>(
                name: "WaterUsagePermitId",
                table: "Ponds",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "LesseeId",
                table: "Ponds",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "LeaseAgreementId",
                table: "Ponds",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<double>(
                name: "XLocation",
                table: "Ponds",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "YLocation",
                table: "Ponds",
                type: "float",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ponds_LeaseAgreements_LeaseAgreementId",
                table: "Ponds",
                column: "LeaseAgreementId",
                principalTable: "LeaseAgreements",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ponds_Lessees_LesseeId",
                table: "Ponds",
                column: "LesseeId",
                principalTable: "Lessees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ponds_WaterUsagePermits_WaterUsagePermitId",
                table: "Ponds",
                column: "WaterUsagePermitId",
                principalTable: "WaterUsagePermits",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ponds_LeaseAgreements_LeaseAgreementId",
                table: "Ponds");

            migrationBuilder.DropForeignKey(
                name: "FK_Ponds_Lessees_LesseeId",
                table: "Ponds");

            migrationBuilder.DropForeignKey(
                name: "FK_Ponds_WaterUsagePermits_WaterUsagePermitId",
                table: "Ponds");

            migrationBuilder.DropColumn(
                name: "XLocation",
                table: "Ponds");

            migrationBuilder.DropColumn(
                name: "YLocation",
                table: "Ponds");

            migrationBuilder.AlterColumn<int>(
                name: "WaterUsagePermitId",
                table: "Ponds",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LesseeId",
                table: "Ponds",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LeaseAgreementId",
                table: "Ponds",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Coordinates",
                table: "Ponds",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ponds_LeaseAgreements_LeaseAgreementId",
                table: "Ponds",
                column: "LeaseAgreementId",
                principalTable: "LeaseAgreements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ponds_Lessees_LesseeId",
                table: "Ponds",
                column: "LesseeId",
                principalTable: "Lessees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ponds_WaterUsagePermits_WaterUsagePermitId",
                table: "Ponds",
                column: "WaterUsagePermitId",
                principalTable: "WaterUsagePermits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
