using System;

namespace CapaEntidad
{
    public class Ventas
    {
        public int VentaId { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public string TipoVenta { get; set; }
        public string Cliente { get; set; }
        public string Vendedor { get; set; }
        public string MetodoPago { get; set; }
        public bool Estado { get; set; }
    }
} 