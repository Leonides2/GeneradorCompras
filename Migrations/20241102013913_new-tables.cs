using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeneradorCompras.Migrations
{
    /// <inheritdoc />
    public partial class newtables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Detail",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "Compras");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "Compras");

            migrationBuilder.RenameColumn(
                name: "CreditCard_N",
                table: "Compras",
                newName: "UserID");

            migrationBuilder.AddColumn<int>(
                name: "Canton",
                table: "Usuarios",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreditCard",
                table: "Usuarios",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Provincia",
                table: "Usuarios",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "State",
                table: "Tarjetas",
                type: "INTEGER",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CardNumber",
                table: "Tarjetas",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "CVV",
                table: "Tarjetas",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Productos",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CommerceID",
                table: "Productos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CommerceName",
                table: "Productos",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CompraID",
                table: "Productos",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Productos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Productos",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Time",
                table: "Compras",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "Total",
                table: "Compras",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_Productos_CompraID",
                table: "Productos",
                column: "CompraID");

            migrationBuilder.CreateIndex(
                name: "IX_Compras_UserID",
                table: "Compras",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Compras_Usuarios_UserID",
                table: "Compras",
                column: "UserID",
                principalTable: "Usuarios",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Compras_CompraID",
                table: "Productos",
                column: "CompraID",
                principalTable: "Compras",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compras_Usuarios_UserID",
                table: "Compras");

            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Compras_CompraID",
                table: "Productos");

            migrationBuilder.DropIndex(
                name: "IX_Productos_CompraID",
                table: "Productos");

            migrationBuilder.DropIndex(
                name: "IX_Compras_UserID",
                table: "Compras");

            migrationBuilder.DropColumn(
                name: "Canton",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "CreditCard",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Provincia",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "CommerceID",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "CommerceName",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "CompraID",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Compras");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "Compras");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Compras",
                newName: "CreditCard_N");

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "Tarjetas",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "CardNumber",
                table: "Tarjetas",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "CVV",
                table: "Tarjetas",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "Detail",
                table: "Productos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Compras",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "Compras",
                type: "TEXT",
                nullable: true);
        }
    }
}
