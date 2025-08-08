namespace PruebaBlazor.Models
{
    public class OrdenModel
    {
        public int OrdenId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public List<OrdenItemModel> Items { get; set; } = new();

        public class OrdenItemModel
        {
            public int LibroId { get; set; }
            public string Titulo { get; set; } = string.Empty;
            public decimal Precio { get; set; }
            public int Cantidad { get; set; }
            public decimal Subtotal => Precio * Cantidad;
        }
    }
}
