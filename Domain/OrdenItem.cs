namespace Domain
{
    public class OrdenItem
    {
        public int Id { get; set; }
        public int OrdenId { get; set; }
        public int LibroId { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; } // Guardamos el precio al momento de la compra

        // Propiedades de navegación
        public Orden? Orden { get; set; }
        public Libro? Libro { get; set; }
    }
}
