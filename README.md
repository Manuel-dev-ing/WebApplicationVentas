# Punto de Ventas (POS)

## Descripcion
Este es un sistema de Punto de Venta (POS) desarrollado con ASP.NET Core, SQL Server y Entity Framework Core, que implementa los patrones de diseño Repository y Unit of Work para una arquitectura limpia y mantenible. El frontend está construido con JavaScript y Bootstrap, brindando una interfaz moderna y responsiva.

## 🚀 Funcionalidades

### 🔐 Autenticación y Autorización
- Inicio de sesión seguro con control de acceso basado en roles.
- Gestión de perfiles de usuario.

### 🧾 Módulo de Ventas
- Registro de ventas con generación de factura o ticket.
- Selección rápida de productos y clientes.

### 📦 Módulo de Compras
- Registro de compras con proveedores.
- Actualización automática del inventario al recibir productos.

### 🗃️ Inventario
- Consulta el stock por producto y almacén.

### 📄 Facturación y Tickets
- Generación de documentos de venta (factura o ticket) en formato imprimible.

### 📊 Dashboard
- Panel principal con métricas clave: ventas, compras, productos en stock.

### 🧑 Gestión de Clientes
- Registro y edición de clientes con sus datos de contacto y dirección.

### 🛍️ Gestión de Productos
- CRUD de productos con control de stock, precios y detalles asociados.
- Asociación de productos con marca, categoría y almacén.

### 🏪 Gestión de Almacenes
- Creación y administración de múltiples almacenes.

### 🏷️ Gestión de Marcas y Categorías
- Organización de productos por marcas y categorías personalizadas.

### 🚚 Gestión de Proveedores
- Registro de proveedores y sus datos de contacto.
  
### 🚨 Notificaciones de Stock Bajo
- Alertas automáticas de productos con stock por debajo del mínimo.
- Visualización de productos críticos desde el dashboard.

## 🛠️ Tecnologías Usadas

### Backend
- **ASP.NET Core**
- **Entity Framework Core**
- **SQL Server** 
- **Patrones de diseño**:
  - Repository
  - Unit of Work

### Frontend
- **JavaScript**
- **Bootstrap**

### Otros
- **SweetAlert2**
- **Chart.js**
## ⚙️ Instalación

Sigue estos pasos para ejecutar el proyecto en tu entorno local utilizando **Visual Studio**:

### 1. Clonar el repositorio

Clona este repositorio en tu equipo utilizando Git o descarga el proyecto como archivo `.zip`:

```
git clone https://github.com/Manuel-dev-ing/WebApplicationVentas.git

```
### 2. Navegar al directorio del proyecto:
- Abre Visual Studio 2022 o superior.
- Haz clic en "Abrir un proyecto o una solución".
- Selecciona el archivo .sln del proyecto.
  
### 3. Configurar la base de datos
- Asegúrate de tener SQL Server instalado y en ejecución.
- Crea una nueva base de datos o usa una existente.
- Abre el archivo appsettings.json y edita la cadena de conexión:
```
"ConnectionStrings": {
  "DefaultConnection": "Server=TU_SERVIDOR;Database=TU_BASE_DE_DATOS;Trusted_Connection=True;MultipleActiveResultSets=true"
}

```
### 4. Aplicar migraciones (si no estás usando Database First)
Abre la Consola del Administrador de Paquetes (Menu: Herramientas > Administrador de paquetes NuGet > Consola).

Ejecuta:
```
Update-Database

```
Si estás usando enfoque Database First, asegúrate de que los modelos estén actualizados mediante:
```
Scaffold-DbContext "TU_CADENA_DE_CONEXION" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Force

```
### 5. Ejecutar el proyecto
En la parte superior de Visual Studio, selecciona el proyecto como proyecto de inicio.
- Elige el perfil de ejecución (IIS Express o Proyecto).
- Presiona F5 o haz clic en "Iniciar depuración".
- La aplicación se abrirá automáticamente en tu navegador en https://localhost:xxxx o http://localhost:xxxx.

## Licencia

Punto de Ventas es [MIT licensed](./LICENSE).

## Contacto
**Nombre:** Manuel Tamayo Montero.

**Correo:** manueltamayo9765@gmail.com
