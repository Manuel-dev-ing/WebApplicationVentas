using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplicationVentas.Entidades;
using WebApplicationVentas.Servicios;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(opciones => opciones.UseSqlServer("name=DefaultConnection"));

builder.Services.AddTransient<IRepositorioAlmacenes, RepositoriosAlmacenes>();
builder.Services.AddTransient<IRepositorioCategorias, RepositorioCategorias>();
builder.Services.AddTransient<IRepositorioMarcas, RepositorioMarcas>();
builder.Services.AddTransient<IRepositorioRubros, RepositorioRubros>();
builder.Services.AddTransient<IRepositorioProductos, RepositorioProductos>();
builder.Services.AddTransient<IRepositorioProveedores, RepositorioProveedores>();
builder.Services.AddTransient<IRepositorioTiposDocumentosProvCliente, RepositorioTiposDocumentosProvCliente>();
builder.Services.AddTransient<IRepositorioRol, RepositorioRol>();


builder.Services.AddTransient<IRepositorioVentas, RepositorioVentas>();
builder.Services.AddTransient<IRepositorioCompras, RepositorioCompras>();
builder.Services.AddTransient<IRepositorioStockProductos, RepositorioStockProductos>();

builder.Services.AddTransient<IRepositorioStockProductos, RepositorioStockProductos>();
builder.Services.AddTransient<IRepositorioNegocio, RepositorioNegocio>();

builder.Services.AddTransient<IRepositorioUsuarios, RepositorioUsuarios>();
builder.Services.AddTransient<IUserStore<Usuario>, UsuarioStore>();
builder.Services.AddTransient<IUserClaimStore<Usuario>, UsuarioStore>();

builder.Services.AddTransient<SignInManager<Usuario>>();

builder.Services.AddIdentityCore<Usuario>(opciones =>
{
    opciones.Password.RequireDigit = false;
    opciones.Password.RequireLowercase = false;
    opciones.Password.RequireUppercase = false;
    opciones.Password.RequireNonAlphanumeric = false;
    opciones.Password.RequiredLength = 4;
})
.AddSignInManager<SignInManager<Usuario>>()
.AddUserStore<UsuarioStore>()
.AddDefaultTokenProviders();

builder.Services.AddAuthentication(opciones =>
{

    opciones.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
    opciones.DefaultSignOutScheme = IdentityConstants.ApplicationScheme;
    opciones.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;

}).AddCookie(IdentityConstants.ApplicationScheme, options =>
{

    options.LoginPath = "/Usuarios/Login";
    options.AccessDeniedPath = "/Usuarios/Login";

});



builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});


var app = builder.Build();

app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuarios}/{action=Login}/{id?}");

IWebHostEnvironment env = app.Environment;
Rotativa.AspNetCore.RotativaConfiguration.Setup(env.WebRootPath, "../Rotativa/Windows");

app.Run();
