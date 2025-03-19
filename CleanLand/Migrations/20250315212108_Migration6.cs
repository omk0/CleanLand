using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanLand.Migrations
{
    /// <inheritdoc />
    public partial class Migration6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "Ponds",
                newName: "Description");

            migrationBuilder.AddColumn<string>(
                name: "District",
                table: "Forests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Forests",
                keyColumn: "Id",
                keyValue: 1,
                column: "District",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "District",
                table: "Forests");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Ponds",
                newName: "Notes");
        }
    }
}
