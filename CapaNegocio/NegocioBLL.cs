using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class NegocioBLL
    {
        public List<Negocio> ObtenerNegocio()
        {
            try
            {
                CapaDatos.NegocioDAL negocioDAL = new CapaDatos.NegocioDAL();
                return negocioDAL.ObtenerTodos();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener dato del negocio.", ex);
            }
        }

        public Negocio ObtenerNegocioPorId(int id)
        {
            try
            {
                CapaDatos.NegocioDAL negocioDAL = new CapaDatos.NegocioDAL();
                return negocioDAL.ObtenerPorId(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener dato del negocio por ID.", ex);
            }
        }

        public bool EditarNegocio(Negocio negocio)
        {
            try
            {
                CapaDatos.NegocioDAL negocioDAL = new CapaDatos.NegocioDAL();
                return negocioDAL.EditaNegocio(negocio);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al editar el negocio.", ex);
            }
        }
    }
}
