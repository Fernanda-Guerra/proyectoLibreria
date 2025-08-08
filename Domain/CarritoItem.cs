namespace Domain
{
    public class CarritoItem
    {
        public int Id { get; set; }
        public int LibroId { get; set; }
        public int UsuarioId { get; set; }
        public int Cantidad { get; set; }

        // --- Propiedades de navegaci�n ---
        // Permiten a EF Core cargar autom�ticamente los datos relacionados.
        public Libro? Libro { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
