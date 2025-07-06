using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class UnidadMedidaBLL
    {
        UnidadMedidaDAL unidadMedidaDAL = new UnidadMedidaDAL();

        public List<UnidadesMedida> ObtenerUnidadesMedida()
        {
            return unidadMedidaDAL.ObtenerUnidadesMedida();
        }

        public UnidadesMedida ObtenerUnidadMedidaPorId(int id)
        {
            if (id < 0)
            {
                throw new ArgumentException("El id debe ser mayor a 0");
            }
            return unidadMedidaDAL.ObtenerUnidadMedidaPorId(id);
        }

        public bool RegistrarUnidadMedida(UnidadesMedida unidadMedida)
        {
            if (unidadMedida == null)
            {
                throw new ArgumentNullException("La unidad de medida no puede ser nula");
            }
            if (string.IsNullOrWhiteSpace(unidadMedida.Nombre))
            {
                throw new ArgumentException("El nombre de la unidad de medida no puede estar vacío");
            }
            return unidadMedidaDAL.RegistrarUnidadMedida(unidadMedida);
        }

        public bool EditarUnidadMedida(UnidadesMedida unidadMedida)
        {
            if (unidadMedida == null)
            {
                throw new ArgumentNullException("La unidad de medida no puede ser nula");
            }
            if (unidadMedida.Id <= 0)
            {
                throw new ArgumentException("El id de la unidad de medida debe ser mayor a 0");
            }
            if (string.IsNullOrWhiteSpace(unidadMedida.Nombre))
            {
                throw new ArgumentException("El nombre de la unidad de medida no puede estar vacío");
            }
            return unidadMedidaDAL.EditarUnidadMedida(unidadMedida);
        }

        public bool EliminarUnidadMedida(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El id de la unidad de medida debe ser mayor a 0");
            }
            return unidadMedidaDAL.EliminarUnidadMedida(id);
        }
    }
}
