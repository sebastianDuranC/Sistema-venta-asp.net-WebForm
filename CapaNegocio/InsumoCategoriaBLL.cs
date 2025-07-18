using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class InsumoCategoriaBLL
    {
        InsumoCategoriaDAL insumoCategoriaDAL = new InsumoCategoriaDAL();
        public List<InsumoCategoria> ObtenerInsumoCategorias()
        {
            return insumoCategoriaDAL.ObtenerTodos();
        }

        public bool RegistrarInsumoCategoria(InsumoCategoria insumoCategoria)
        {
            if (insumoCategoria == null)
            {
                throw new ArgumentNullException("El objeto InsumoCategoria no puede ser nulo.");
            }
            if (string.IsNullOrWhiteSpace(insumoCategoria.Nombre))
            {
                throw new ArgumentException("El nombre de la categoría no puede estar vacío.");
            }
            return insumoCategoriaDAL.RegistrarInsumoCategoria(insumoCategoria);
        }

        public InsumoCategoria obtenerInsumoCategoriaPorId(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID debe ser un número positivo.");
            }
            return insumoCategoriaDAL.ObtenerInsumoCategoriaPorId(id);
        }

        public bool EditarInsumoCategoria(InsumoCategoria insumoCategoria)
        {
            if (insumoCategoria == null)
            {
                throw new ArgumentNullException("El objeto InsumoCategoria no puede ser nulo.");
            }
            if (insumoCategoria.Id <= 0)
            {
                throw new ArgumentException("El ID debe ser un número positivo.");
            }
            if (string.IsNullOrWhiteSpace(insumoCategoria.Nombre))
            {
                throw new ArgumentException("El nombre de la categoría no puede estar vacío.");
            }
            return insumoCategoriaDAL.EditarInsumoCategoria(insumoCategoria);
        }

        public bool EliminarInsumoCategoria(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID debe ser un número positivo.");
            }
            return insumoCategoriaDAL.EliminarInsumoCategoria(id);
        }
    }
}
