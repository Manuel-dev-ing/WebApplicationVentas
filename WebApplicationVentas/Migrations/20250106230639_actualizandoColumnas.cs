using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationVentas.Migrations
{
    /// <inheritdoc />
    public partial class actualizandoColumnas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_detalle_entrada_productos_productos",
                table: "detalle_entrada_productos");

            migrationBuilder.DropForeignKey(
                name: "FK_detalle_ventas_categorias",
                table: "detalle_ventas");

            migrationBuilder.DropForeignKey(
                name: "FK_detalle_ventas_marcas",
                table: "detalle_ventas");

            migrationBuilder.DropForeignKey(
                name: "FK_detalle_ventas_productos",
                table: "detalle_ventas");

            migrationBuilder.DropForeignKey(
                name: "FK_entrada_productos_almacenes",
                table: "entrada_productos");

            migrationBuilder.DropForeignKey(
                name: "FK_entrada_productos_proveedores",
                table: "entrada_productos");

            migrationBuilder.DropForeignKey(
                name: "FK_stock_productos_almacenes",
                table: "stock_productos");

            migrationBuilder.DropForeignKey(
                name: "FK_stock_productos_productos",
                table: "stock_productos");

            migrationBuilder.DropIndex(
                name: "IX_detalle_ventas_id_categoria",
                table: "detalle_ventas");

            migrationBuilder.DropIndex(
                name: "IX_detalle_ventas_id_marca",
                table: "detalle_ventas");

            migrationBuilder.DropIndex(
                name: "IX_detalle_ventas_id_producto",
                table: "detalle_ventas");

            migrationBuilder.DropColumn(
                name: "impuesto",
                table: "ventas");

            migrationBuilder.DropColumn(
                name: "foto",
                table: "usuarios");

            migrationBuilder.DropColumn(
                name: "id_categoria",
                table: "detalle_ventas");

            migrationBuilder.DropColumn(
                name: "id_marca",
                table: "detalle_ventas");

            migrationBuilder.RenameColumn(
                name: "EsActivo",
                table: "clientes",
                newName: "es_activo");

            migrationBuilder.AlterColumn<DateTime>(
                name: "fecha_registro",
                table: "ventas",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "fecha_registro",
                table: "tipoDocumentoVenta",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "es_activo",
                table: "tipoDocumentoVenta",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "stock_actual",
                table: "stock_productos",
                type: "decimal(10,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "precio",
                table: "stock_productos",
                type: "decimal(10,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<int>(
                name: "id_producto",
                table: "stock_productos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "id_almacen",
                table: "stock_productos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "stock_minimo",
                table: "productos",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<int>(
                name: "stock_maximo",
                table: "productos",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "total",
                table: "entrada_productos",
                type: "decimal(10,0)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,0)");

            migrationBuilder.AlterColumn<decimal>(
                name: "sub_total",
                table: "entrada_productos",
                type: "decimal(10,0)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,0)");

            migrationBuilder.AlterColumn<int>(
                name: "id_proveedor",
                table: "entrada_productos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "id_almacen",
                table: "entrada_productos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "fecha_registro",
                table: "entrada_productos",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "fecha_entrada_producto",
                table: "entrada_productos",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AddColumn<int>(
                name: "CategoriaId",
                table: "detalle_ventas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MarcaId",
                table: "detalle_ventas",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "total",
                table: "detalle_entrada_productos",
                type: "decimal(10,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "precio",
                table: "detalle_entrada_productos",
                type: "decimal(10,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<int>(
                name: "id_producto",
                table: "detalle_entrada_productos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "cantidad",
                table: "detalle_entrada_productos",
                type: "decimal(10,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.CreateIndex(
                name: "IX_detalle_ventas_CategoriaId",
                table: "detalle_ventas",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_detalle_ventas_MarcaId",
                table: "detalle_ventas",
                column: "MarcaId");

            migrationBuilder.AddForeignKey(
                name: "FK_detalle_entrada_productos_productos",
                table: "detalle_entrada_productos",
                column: "id_producto",
                principalTable: "productos",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_detalle_ventas_categorias_CategoriaId",
                table: "detalle_ventas",
                column: "CategoriaId",
                principalTable: "categorias",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_detalle_ventas_marcas_MarcaId",
                table: "detalle_ventas",
                column: "MarcaId",
                principalTable: "marcas",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_entrada_productos_almacenes",
                table: "entrada_productos",
                column: "id_almacen",
                principalTable: "almacenes",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_entrada_productos_proveedores",
                table: "entrada_productos",
                column: "id_proveedor",
                principalTable: "proveedores",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_stock_productos_almacenes",
                table: "stock_productos",
                column: "id_almacen",
                principalTable: "almacenes",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_stock_productos_productos",
                table: "stock_productos",
                column: "id_producto",
                principalTable: "productos",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_detalle_entrada_productos_productos",
                table: "detalle_entrada_productos");

            migrationBuilder.DropForeignKey(
                name: "FK_detalle_ventas_categorias_CategoriaId",
                table: "detalle_ventas");

            migrationBuilder.DropForeignKey(
                name: "FK_detalle_ventas_marcas_MarcaId",
                table: "detalle_ventas");

            migrationBuilder.DropForeignKey(
                name: "FK_entrada_productos_almacenes",
                table: "entrada_productos");

            migrationBuilder.DropForeignKey(
                name: "FK_entrada_productos_proveedores",
                table: "entrada_productos");

            migrationBuilder.DropForeignKey(
                name: "FK_stock_productos_almacenes",
                table: "stock_productos");

            migrationBuilder.DropForeignKey(
                name: "FK_stock_productos_productos",
                table: "stock_productos");

            migrationBuilder.DropIndex(
                name: "IX_detalle_ventas_CategoriaId",
                table: "detalle_ventas");

            migrationBuilder.DropIndex(
                name: "IX_detalle_ventas_MarcaId",
                table: "detalle_ventas");

            migrationBuilder.DropColumn(
                name: "CategoriaId",
                table: "detalle_ventas");

            migrationBuilder.DropColumn(
                name: "MarcaId",
                table: "detalle_ventas");

            migrationBuilder.RenameColumn(
                name: "es_activo",
                table: "clientes",
                newName: "EsActivo");

            migrationBuilder.AlterColumn<DateTime>(
                name: "fecha_registro",
                table: "ventas",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AddColumn<decimal>(
                name: "impuesto",
                table: "ventas",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "foto",
                table: "usuarios",
                type: "varchar(500)",
                unicode: false,
                maxLength: 500,
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "fecha_registro",
                table: "tipoDocumentoVenta",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<bool>(
                name: "es_activo",
                table: "tipoDocumentoVenta",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<decimal>(
                name: "stock_actual",
                table: "stock_productos",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "precio",
                table: "stock_productos",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "id_producto",
                table: "stock_productos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "id_almacen",
                table: "stock_productos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "stock_minimo",
                table: "productos",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "stock_maximo",
                table: "productos",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "total",
                table: "entrada_productos",
                type: "decimal(10,0)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,0)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "sub_total",
                table: "entrada_productos",
                type: "decimal(10,0)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,0)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "id_proveedor",
                table: "entrada_productos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "id_almacen",
                table: "entrada_productos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "fecha_registro",
                table: "entrada_productos",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "fecha_entrada_producto",
                table: "entrada_productos",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "id_categoria",
                table: "detalle_ventas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "id_marca",
                table: "detalle_ventas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "total",
                table: "detalle_entrada_productos",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "precio",
                table: "detalle_entrada_productos",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "id_producto",
                table: "detalle_entrada_productos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "cantidad",
                table: "detalle_entrada_productos",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_detalle_ventas_id_categoria",
                table: "detalle_ventas",
                column: "id_categoria");

            migrationBuilder.CreateIndex(
                name: "IX_detalle_ventas_id_marca",
                table: "detalle_ventas",
                column: "id_marca");

            migrationBuilder.CreateIndex(
                name: "IX_detalle_ventas_id_producto",
                table: "detalle_ventas",
                column: "id_producto");

            migrationBuilder.AddForeignKey(
                name: "FK_detalle_entrada_productos_productos",
                table: "detalle_entrada_productos",
                column: "id_producto",
                principalTable: "productos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_detalle_ventas_categorias",
                table: "detalle_ventas",
                column: "id_categoria",
                principalTable: "categorias",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_detalle_ventas_marcas",
                table: "detalle_ventas",
                column: "id_marca",
                principalTable: "marcas",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_detalle_ventas_productos",
                table: "detalle_ventas",
                column: "id_producto",
                principalTable: "productos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_entrada_productos_almacenes",
                table: "entrada_productos",
                column: "id_almacen",
                principalTable: "almacenes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_entrada_productos_proveedores",
                table: "entrada_productos",
                column: "id_proveedor",
                principalTable: "proveedores",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_stock_productos_almacenes",
                table: "stock_productos",
                column: "id_almacen",
                principalTable: "almacenes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_stock_productos_productos",
                table: "stock_productos",
                column: "id_producto",
                principalTable: "productos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
