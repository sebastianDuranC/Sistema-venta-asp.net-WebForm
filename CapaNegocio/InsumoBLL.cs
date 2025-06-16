using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class InsumoBLL
    {
        public List<Insumo> ObtenerInsumos()
        {
            InsumoDAL insumoDAL = new InsumoDAL();
            List<Insumo> listaInsumos = insumoDAL.ObtenerTodos();
            return listaInsumos;
        }
    }
}
