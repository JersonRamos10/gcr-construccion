using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gcr.Construccion.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCategoriaMaterial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compras_Categorias_CategoriaId",
                table: "Compras");

            migrationBuilder.RenameColumn(
                name: "CategoriaId",
                table: "Compras",
                newName: "CategoriaMaterialId");

            migrationBuilder.RenameIndex(
                name: "IX_Compras_CategoriaId",
                table: "Compras",
                newName: "IX_Compras_CategoriaMaterialId");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Categorias",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_Compras_Categorias_CategoriaMaterialId",
                table: "Compras",
                column: "CategoriaMaterialId",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compras_Categorias_CategoriaMaterialId",
                table: "Compras");

            migrationBuilder.RenameColumn(
                name: "CategoriaMaterialId",
                table: "Compras",
                newName: "CategoriaId");

            migrationBuilder.RenameIndex(
                name: "IX_Compras_CategoriaMaterialId",
                table: "Compras",
                newName: "IX_Compras_CategoriaId");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Categorias",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Compras_Categorias_CategoriaId",
                table: "Compras",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
