using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace X_HIJA_SYSTEM.Migrations
{
    /// <inheritdoc />
    public partial class seedCategoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DisplayOrder",
                table: "categories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "CatName",
                table: "categories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "CatID", "CatName", "DisplayOrder" },
                values: new object[,]
                {
                    { 12, "UTPXS", "3" },
                    { 100, "UTP", "1" },
                    { 1000, "Fiber", "2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "CatID",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "CatID",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "CatID",
                keyValue: 1000);

            migrationBuilder.AlterColumn<int>(
                name: "DisplayOrder",
                table: "categories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CatName",
                table: "categories",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
