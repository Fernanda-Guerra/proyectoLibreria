using System;
using System.Collections.Generic;

namespace Domain
{
    public class Orden
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public List<OrdenItem> OrdenItems { get; set; } = new();
    }
}