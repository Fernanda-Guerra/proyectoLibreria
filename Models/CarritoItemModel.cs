namespace PruebaBlazor.Models
{
    // Se combinan CarritoItem y del Libro
    public class CarritoItemModel
    {
        public int CarritoItemId { get; set; }
        public int LibroId { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public string ImagenUrl { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal Subtotal => Precio * Cantidad;
    }
}
