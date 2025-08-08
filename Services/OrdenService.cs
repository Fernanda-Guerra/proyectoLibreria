using Domain;
using Microsoft.EntityFrameworkCore;
using PruebaBlazor.Data;
using PruebaBlazor.Models; // Necesitamos este using

namespace PruebaBlazor.Services
{
    public interface IOrdenService
    {
        Task<List<Orden>> GetOrdenesAsync(int usuarioId);
        Task<Orden?> GetOrdenAsync(int id);
        // La firma del método cambia para aceptar los artículos del carrito
        Task<int> CrearOrdenAsync(int usuarioId, List<CarritoItemModel> items);
        //Task<List<Orden>> GetOrdenesByUsuarioAsync(int usuarioId);
    }

    public class OrdenService : IOrdenService
    {
        private readonly LibreriaDbContext _context;
        public OrdenService(LibreriaDbContext context) => _context = context;

        public async Task<List<Orden>> GetOrdenesAsync(int usuarioId) =>
            await _context.Ordenes.Where(o => o.UsuarioId == usuarioId).ToListAsync();

        public async Task<Orden?> GetOrdenAsync(int id) => 
            await _context.Ordenes.Include(o => o.OrdenItems).ThenInclude(oi => oi.Libro).FirstOrDefaultAsync(o => o.Id == id);

        // Implementación actualizada para crear la orden y sus detalles
        public async Task<int> CrearOrdenAsync(int usuarioId, List<CarritoItemModel> items)
        {
            var orden = new Orden
            {
                UsuarioId = usuarioId,
                Fecha = DateTime.Now,
                Total = items.Sum(item => item.Subtotal),
                OrdenItems = items.Select(item => new OrdenItem
                {
                    LibroId = item.LibroId,
                    Cantidad = item.Cantidad,
                    // Guardamos el precio de ese momento
                    Precio = item.Precio
                }).ToList()
            };

            _context.Ordenes.Add(orden);
            await _context.SaveChangesAsync();
            return orden.Id;
        }
    }
}
