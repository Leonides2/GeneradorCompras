using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeneradorCompras.Migrations
{
    /// <inheritdoc />
    public partial class newtablesagain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommerceName",
                table: "Productos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CommerceName",
                table: "Productos",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
