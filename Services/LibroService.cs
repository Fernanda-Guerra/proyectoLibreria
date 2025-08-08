using Domain;
using Microsoft.EntityFrameworkCore;
using PruebaBlazor.Data;

namespace PruebaBlazor.Services
{
    public interface ILibroService
    {
        Task<List<Libro>> GetLibrosAsync();
        Task<Libro?> GetLibroAsync(int id);
        Task AddLibroAsync(Libro libro);
        Task UpdateLibroAsync(Libro libro);
        Task DeleteLibroAsync(int id);
    }

    public class LibroService : ILibroService
    {
        private readonly LibreriaDbContext _context;
        public LibroService(LibreriaDbContext context) => _context = context;

        public async Task<List<Libro>> GetLibrosAsync() => await _context.Libros.ToListAsync();
        public async Task<Libro?> GetLibroAsync(int id) => await _context.Libros.FindAsync(id);
        public async Task AddLibroAsync(Libro libro)
        {
            _context.Libros.Add(libro);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateLibroAsync(Libro libro)
        {
            _context.Libros.Update(libro);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteLibroAsync(int id)
        {
            var libro = await _context.Libros.FindAsync(id);
            if (libro != null)
            {
                _context.Libros.Remove(libro);
                await _context.SaveChangesAsync();
            }
        }
    }
}
