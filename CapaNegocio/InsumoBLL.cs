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
        InsumoDAL insumoDAL = new InsumoDAL();
        public List<Insumo> ObtenerInsumos()
        {
            List<Insumo> listaInsumos = insumoDAL.ObtenerTodos();
            return listaInsumos;
        }

        public Insumo ObtenerInsumoPorId(int id)
        {
            return insumoDAL.ObtenerInsumoPorId(id);
        }

        public bool RegistrarInsumo(Insumo insumo)
        {
            if (insumo == null)
            {
                throw new ArgumentException("Debe completar todos los datos para crear un insumo");
            }
            if (string.IsNullOrWhiteSpace(insumo.Nombre) || insumo.InsumoCategoriaId <= 0 || insumo.ProveedorId <= 0 || insumo.UnidadesMedidaId <= 0)
            {
                throw new ArgumentException("Debe completar todos los datos para crear un insumo");
            }
            return insumoDAL.RegistrarInsumo(insumo);
        }

        public bool EditarInsumo(Insumo insumo)
        {
            if (insumo == null || insumo.Id <= 0)
            {
                throw new ArgumentException("Debe completar todos los datos para editar un insumo");
            }
            if (string.IsNullOrWhiteSpace(insumo.Nombre) || insumo.Costo < 0 || insumo.InsumoCategoriaId <= 0 || insumo.ProveedorId <= 0 || insumo.UnidadesMedidaId <= 0)
            {
                throw new ArgumentException("Debe completar todos los datos para editar un insumo");
            }
            return insumoDAL.EditarInsumo(insumo);
        }

        public bool EliminarInsumo(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Debe proporcionar un ID válido para eliminar un insumo");
            }
            return insumoDAL.EliminarInsumo(id);
        }
    }
}
