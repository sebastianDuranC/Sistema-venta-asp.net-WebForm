using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class DashboardBLL
    {
        private DashboardDAL objcd_dashboard = new DashboardDAL();

        public decimal ObtenerVentasTotalesMes() => objcd_dashboard.ObtenerVentasTotalesMes();
        public int ObtenerProductosVendidosMes() => objcd_dashboard.ObtenerProductosVendidosMes();
        public int ObtenerTotalItemsInventario() => objcd_dashboard.ObtenerTotalItemsInventario();
        public List<Dictionary<string, object>> ObtenerVentasSemana() => objcd_dashboard.ObtenerVentasSemana();
        public List<Dictionary<string, object>> ObtenerTopProductosVendidos() => objcd_dashboard.ObtenerTopProductosVendidos();
    }
}
