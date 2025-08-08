using Microsoft.EntityFrameworkCore;
using Domain;

namespace PruebaBlazor.Data
{
    public class LibreriaDbContext : DbContext
    {
        public LibreriaDbContext(DbContextOptions<LibreriaDbContext> options) : base(options) { }

        public DbSet<Libro> Libros { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<CarritoItem> CarritoItems { get; set; }
        public DbSet<Orden> Ordenes { get; set; }
        public DbSet<OrdenItem> OrdenItems { get; set; } 

        // MÉTODO COMPLETO PARA DATA SEEDING
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Libro>().HasData(
                // Libros existentes actualizados con Stock y nueva URL de imagen
                new Libro
                {
                    Id = 1,
                    Titulo = "Cien Años de Soledad",
                    Autor = "Gabriel García Márquez",
                    Precio = 15.99m,
                    Descripcion = "La novela narra la historia de la familia Buendía a lo largo de siete generaciones en el pueblo ficticio de Macondo.",
                    ImagenUrl = "Imagenes/cien-anos.jpeg", // <-- Ruta actualizada
                    Stock = 50 // <-- Nuevo campo
                },
                new Libro
                {
                    Id = 2,
                    Titulo = "Don Quijote de la Mancha",
                    Autor = "Miguel de Cervantes",
                    Precio = 12.50m,
                    Descripcion = "Considerada la obra más destacada de la literatura española y una de las principales de la literatura universal.",
                    ImagenUrl = "Imagenes/donQuijote.jpeg", // <-- Ruta actualizada
                    Stock = 30 // <-- Nuevo campo
                },
                new Libro
                {
                    Id = 3,
                    Titulo = "1984",
                    Autor = "George Orwell",
                    Precio = 10.00m,
                    Descripcion = "Una novela de ficción distópica sobre los peligros del totalitarismo y la vigilancia extrema.",
                    //ImagenUrl = "images/covers/1984.jpg", // <-- Ruta actualizada
                    Stock = 75 // <-- Nuevo campo
                },
                // --- AÑADE AQUÍ TUS NUEVOS LIBROS ---
                new Libro
                {
                    Id = 4,
                    Titulo = "El Señor de los Anillos",
                    Autor = "J.R.R. Tolkien",
                    Precio = 25.00m,
                    Descripcion = "Una épica de alta fantasía sobre la lucha contra el Señor Oscuro Sauron.",
                    //ImagenUrl = "images/covers/senor-anillos.jpg",
                    Stock = 40
                },
                new Libro
                {
                    Id = 5,
                    Titulo = "Fahrenheit 451",
                    Autor = "Ray Bradbury",
                    Precio = 11.50m,
                    Descripcion = "Presenta una sociedad futura donde los libros están prohibidos y son quemados.",
                    //ImagenUrl = "images/covers/fahrenheit-451.jpg",
                    Stock = 60
                },
                new Libro
                {
                    Id = 6,
                    Titulo = "Better than the Movies",
                    Autor = "Lynn Painter",
                    Precio = 15.50m,
                    Descripcion = "La comovedora historia de amor de Wes Bennet y Liz Beauxbaum.",
                    //ImagenUrl = "images/covers/fahrenheit-451.jpg",
                    Stock = 60
                }

            );
        }
    }
}