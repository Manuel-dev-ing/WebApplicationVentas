using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationVentas.Migrations
{
    /// <inheritdoc />
    public partial class AgregarCampoEsActivoCliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "almacenes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descripcion = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    es_activo = table.Column<bool>(type: "bit", nullable: false),
                    fecha_registro = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_almacenes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "categorias",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descripcion = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    es_activo = table.Column<bool>(type: "bit", nullable: false),
                    fecha_registro = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categorias", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "clientes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    apellidos = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    telefono = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    calle = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    colonia = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    codigo_postal_ciudad = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    EsActivo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clientes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "marcas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descripcion = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    es_activo = table.Column<bool>(type: "bit", nullable: false),
                    fecha_registro = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_marcas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "negocio",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    logotipo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    telefono = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    correo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    calle = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    colonia = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    codigo_postal_ciudad = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    pais = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_negocio", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "rol",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descripcion = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    es_activo = table.Column<bool>(type: "bit", nullable: false),
                    fecha_registro = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rol", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "rubros",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descripcion = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    es_activo = table.Column<bool>(type: "bit", nullable: false),
                    fecha_registro = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rubros", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tipoDocumentoVenta",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descripcion = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    es_activo = table.Column<bool>(type: "bit", nullable: true),
                    fecha_registro = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipoDocumentoVenta", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tipos_documentos_prov_cliente",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descripcion = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    es_activo = table.Column<bool>(type: "bit", nullable: false),
                    fecha_registro = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipos_documentos_prov_cliente", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "productos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_marca = table.Column<int>(type: "int", nullable: false),
                    id_categoria = table.Column<int>(type: "int", nullable: false),
                    codigo_barras = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    descripcion = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    stock_minimo = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    stock_maximo = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    imagen = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    precio = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    es_activo = table.Column<bool>(type: "bit", nullable: false),
                    fecha_registro = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productos", x => x.id);
                    table.ForeignKey(
                        name: "FK_productos_categorias",
                        column: x => x.id_categoria,
                        principalTable: "categorias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_productos_marcas",
                        column: x => x.id_marca,
                        principalTable: "marcas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_rol = table.Column<int>(type: "int", nullable: false),
                    nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    apellidos = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    correo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    telefono = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    foto = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    password = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    es_activo = table.Column<bool>(type: "bit", nullable: false),
                    fecha_registro = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.id);
                    table.ForeignKey(
                        name: "FK_usuarios_rol",
                        column: x => x.id_rol,
                        principalTable: "rol",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "proveedores",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_tipo_documento = table.Column<int>(type: "int", nullable: false),
                    id_rubro = table.Column<int>(type: "int", nullable: false),
                    nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    apellidos = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    telefono = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    calle = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    colonia = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    codigo_postal_ciudad = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    es_activo = table.Column<bool>(type: "bit", nullable: false),
                    fecha_registro = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_proveedores", x => x.id);
                    table.ForeignKey(
                        name: "FK_proveedores_rubros",
                        column: x => x.id_rubro,
                        principalTable: "rubros",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_proveedores_tipos_documentos_prov_cliente",
                        column: x => x.id_tipo_documento,
                        principalTable: "tipos_documentos_prov_cliente",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "detalle_entrada_productos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    id_producto = table.Column<int>(type: "int", nullable: false),
                    cantidad = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    precio = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    total = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_detalle_entrada_productos", x => x.id);
                    table.ForeignKey(
                        name: "FK_detalle_entrada_productos_productos",
                        column: x => x.id_producto,
                        principalTable: "productos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "stock_productos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_producto = table.Column<int>(type: "int", nullable: false),
                    id_almacen = table.Column<int>(type: "int", nullable: false),
                    stock_actual = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    precio = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stock_productos", x => x.id);
                    table.ForeignKey(
                        name: "FK_stock_productos_almacenes",
                        column: x => x.id_almacen,
                        principalTable: "almacenes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_stock_productos_productos",
                        column: x => x.id_producto,
                        principalTable: "productos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ventas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_tipo_documento_venta = table.Column<int>(type: "int", nullable: false),
                    id_usuario = table.Column<int>(type: "int", nullable: false),
                    nombre_cliente = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    sub_total = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    impuesto = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    total = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    fecha_registro = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ventas", x => x.id);
                    table.ForeignKey(
                        name: "FK_ventas_tipoDocumentoVenta",
                        column: x => x.id_tipo_documento_venta,
                        principalTable: "tipoDocumentoVenta",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ventas_usuarios",
                        column: x => x.id_usuario,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "entrada_productos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_proveedor = table.Column<int>(type: "int", nullable: false),
                    id_almacen = table.Column<int>(type: "int", nullable: false),
                    fecha_entrada_producto = table.Column<DateTime>(type: "datetime", nullable: false),
                    sub_total = table.Column<decimal>(type: "decimal(10,0)", nullable: false),
                    total = table.Column<decimal>(type: "decimal(10,0)", nullable: false),
                    fecha_registro = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entrada_productos", x => x.id);
                    table.ForeignKey(
                        name: "FK_entrada_productos_almacenes",
                        column: x => x.id_almacen,
                        principalTable: "almacenes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_entrada_productos_proveedores",
                        column: x => x.id_proveedor,
                        principalTable: "proveedores",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "detalle_ventas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_venta = table.Column<int>(type: "int", nullable: false),
                    id_producto = table.Column<int>(type: "int", nullable: false),
                    id_marca = table.Column<int>(type: "int", nullable: false),
                    id_categoria = table.Column<int>(type: "int", nullable: false),
                    descripcion_producto = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    cantidad = table.Column<int>(type: "int", nullable: false),
                    precio = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    total = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_detalle_ventas", x => x.id);
                    table.ForeignKey(
                        name: "FK_detalle_ventas_categorias",
                        column: x => x.id_categoria,
                        principalTable: "categorias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_detalle_ventas_marcas",
                        column: x => x.id_marca,
                        principalTable: "marcas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_detalle_ventas_productos",
                        column: x => x.id_producto,
                        principalTable: "productos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_detalle_ventas_ventas",
                        column: x => x.id_venta,
                        principalTable: "ventas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_detalle_entrada_productos_id_producto",
                table: "detalle_entrada_productos",
                column: "id_producto");

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

            migrationBuilder.CreateIndex(
                name: "IX_detalle_ventas_id_venta",
                table: "detalle_ventas",
                column: "id_venta");

            migrationBuilder.CreateIndex(
                name: "IX_entrada_productos_id_almacen",
                table: "entrada_productos",
                column: "id_almacen");

            migrationBuilder.CreateIndex(
                name: "IX_entrada_productos_id_proveedor",
                table: "entrada_productos",
                column: "id_proveedor");

            migrationBuilder.CreateIndex(
                name: "IX_productos_id_categoria",
                table: "productos",
                column: "id_categoria");

            migrationBuilder.CreateIndex(
                name: "IX_productos_id_marca",
                table: "productos",
                column: "id_marca");

            migrationBuilder.CreateIndex(
                name: "IX_proveedores_id_rubro",
                table: "proveedores",
                column: "id_rubro");

            migrationBuilder.CreateIndex(
                name: "IX_proveedores_id_tipo_documento",
                table: "proveedores",
                column: "id_tipo_documento");

            migrationBuilder.CreateIndex(
                name: "IX_stock_productos_id_almacen",
                table: "stock_productos",
                column: "id_almacen");

            migrationBuilder.CreateIndex(
                name: "IX_stock_productos_id_producto",
                table: "stock_productos",
                column: "id_producto");

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_id_rol",
                table: "usuarios",
                column: "id_rol");

            migrationBuilder.CreateIndex(
                name: "IX_ventas_id_tipo_documento_venta",
                table: "ventas",
                column: "id_tipo_documento_venta");

            migrationBuilder.CreateIndex(
                name: "IX_ventas_id_usuario",
                table: "ventas",
                column: "id_usuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "clientes");

            migrationBuilder.DropTable(
                name: "detalle_entrada_productos");

            migrationBuilder.DropTable(
                name: "detalle_ventas");

            migrationBuilder.DropTable(
                name: "entrada_productos");

            migrationBuilder.DropTable(
                name: "negocio");

            migrationBuilder.DropTable(
                name: "stock_productos");

            migrationBuilder.DropTable(
                name: "ventas");

            migrationBuilder.DropTable(
                name: "proveedores");

            migrationBuilder.DropTable(
                name: "almacenes");

            migrationBuilder.DropTable(
                name: "productos");

            migrationBuilder.DropTable(
                name: "tipoDocumentoVenta");

            migrationBuilder.DropTable(
                name: "usuarios");

            migrationBuilder.DropTable(
                name: "rubros");

            migrationBuilder.DropTable(
                name: "tipos_documentos_prov_cliente");

            migrationBuilder.DropTable(
                name: "categorias");

            migrationBuilder.DropTable(
                name: "marcas");

            migrationBuilder.DropTable(
                name: "rol");
        }
    }
}
