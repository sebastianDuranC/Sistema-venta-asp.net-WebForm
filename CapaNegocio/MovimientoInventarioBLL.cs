using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class MovimientoInventarioBLL
    {
        MovimientoInventarioDAL movimientoInventarioDAL = new MovimientoInventarioDAL();

        public List<MovimientoInventario> ObtenerMovimientoInventario()
        {
            return movimientoInventarioDAL.ObtenerMovimientoInventario();
        }
    }
}
