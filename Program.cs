using PruebaBlazor.Components;
using Microsoft.EntityFrameworkCore;
using PruebaBlazor.Data;
using PruebaBlazor.Services;

namespace PruebaBlazor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // SE AÑADEN SERVICIOS DEL CONTENEDOR 
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            // SE CONFIGURA DBCONTEXT CON SQLITE
            builder.Services.AddDbContext<LibreriaDbContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=libreria.db"));

            // SE REGISTRA SERVICIO DE AUTENTICACIÓN DENTRO DE MAIN
            builder.Services.AddScoped<IAuthService, AuthService>();
            // SE REGISTRA SERVICIO DE ESTADO DEL USUARIO
            builder.Services.AddScoped<UserStateService>();

            //SE AÑADEN SERVICIOS DE INTEGRACIÓN CON LIBRO, CARRITO Y ORDEN
            builder.Services.AddScoped<ILibroService, LibroService>();
            builder.Services.AddScoped<ICarritoService, CarritoService>();
            builder.Services.AddScoped<IOrdenService, OrdenService>();
            builder.Services.AddScoped<NotificationService>();

            //SE CONTRUYE LA APLICACIÓN
            var app = builder.Build();

            // SE CONFIGURA EL PIPELINE DE LA APLICACIÓN POR HTTPS
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
