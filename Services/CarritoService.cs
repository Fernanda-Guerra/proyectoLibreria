using Domain;
using Microsoft.EntityFrameworkCore;
using PruebaBlazor.Data;
using PruebaBlazor.Models;

namespace PruebaBlazor.Services
{
    public interface ICarritoService
    {
        // Se actualiza para que devuelva el modelo con detalles del libro
        Task<List<CarritoItemModel>> GetCarritoAsync(int usuarioId);
        Task AddToCarritoAsync(int usuarioId, int libroId, int cantidad);
        Task RemoveFromCarritoAsync(int carritoItemId);
        Task ClearCarritoAsync(int usuarioId);
        // Nuevo método para calcular el total
        Task<decimal> GetTotalCarritoAsync(int usuarioId);

        // Este método se puede usar para obtener el número total de artículos en el carrito
        Task<int> GetItemsCountAsync(int usuarioId);
    }

    public class CarritoService : ICarritoService
    {
        private readonly LibreriaDbContext _context;
        public CarritoService(LibreriaDbContext context) => _context = context;

        // Implementación actualizada de GetCarritoAsync
        public async Task<List<CarritoItemModel>> GetCarritoAsync(int usuarioId)
        {
            // Se usa Include() para cargar los datos del libro relacionado y Select() para proyectar al nuevo modelo.
            // Se agrega un filtro para asegurar que Libro no sea null antes de acceder a sus propiedades.
            return await _context.CarritoItems
                .Where(c => c.UsuarioId == usuarioId && c.Libro != null)
                .Include(c => c.Libro)
                .Select(c => new CarritoItemModel
                {
                    CarritoItemId = c.Id,
                    LibroId = c.LibroId,
                    Titulo = c.Libro!.Titulo,
                    Precio = c.Libro.Precio,
                    ImagenUrl = c.Libro.ImagenUrl,
                    Cantidad = c.Cantidad
                }).ToListAsync();
        }

        public async Task AddToCarritoAsync(int usuarioId, int libroId, int cantidad)
        {
            var item = await _context.CarritoItems.FirstOrDefaultAsync(c => c.UsuarioId == usuarioId && c.LibroId == libroId);
            if (item != null)
            {
                item.Cantidad += cantidad;
            }
            else
            {
                _context.CarritoItems.Add(new CarritoItem { UsuarioId = usuarioId, LibroId = libroId, Cantidad = cantidad });
            }
            await _context.SaveChangesAsync();
        }

        public async Task RemoveFromCarritoAsync(int carritoItemId)
        {
            var item = await _context.CarritoItems.FindAsync(carritoItemId);
            if (item != null)
            {
                _context.CarritoItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task ClearCarritoAsync(int usuarioId)
        {
            var items = _context.CarritoItems.Where(c => c.UsuarioId == usuarioId);
            _context.CarritoItems.RemoveRange(items);
            await _context.SaveChangesAsync();
        }

        // Método modificado para evitar la desreferencia de una referencia posiblemente NULL
        public async Task<decimal> GetTotalCarritoAsync(int usuarioId)
        {
            // Se filtran los items cuyo Libro no es null antes de calcular el total
            return await _context.CarritoItems
                .Where(c => c.UsuarioId == usuarioId && c.Libro != null)
                .Include(c => c.Libro)
                .SumAsync(c => c.Libro!.Precio * c.Cantidad);
        }

        public async Task<int> GetItemsCountAsync(int usuarioId)
        {
            return await _context.CarritoItems
                .Where(c => c.UsuarioId == usuarioId)
                .SumAsync(c => c.Cantidad);
        }
    }
}
