using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gcr.Construccion.API.Migrations
{
    /// <inheritdoc />
    public partial class AddMedidaToCompraMaterial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compras_Proveedores_ProveedorId",
                table: "Compras");

            migrationBuilder.AlterColumn<int>(
                name: "ProveedorId",
                table: "Compras",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Medida",
                table: "Compras",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Compras_Proveedores_ProveedorId",
                table: "Compras",
                column: "ProveedorId",
                principalTable: "Proveedores",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compras_Proveedores_ProveedorId",
                table: "Compras");

            migrationBuilder.DropColumn(
                name: "Medida",
                table: "Compras");

            migrationBuilder.AlterColumn<int>(
                name: "ProveedorId",
                table: "Compras",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Compras_Proveedores_ProveedorId",
                table: "Compras",
                column: "ProveedorId",
                principalTable: "Proveedores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
