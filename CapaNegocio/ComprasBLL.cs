using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class ComprasBLL
    {
        ComprasDAL comprasDAL = new ComprasDAL();

        public List<Compra> ObtenerCompras()
        {
            return comprasDAL.ObtenerCompras();
        }

        public Compra ObtenerCompraPorId(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID de la compra debe ser mayor que cero");
            }
            return comprasDAL.ObtenerCompraPorId(id);
        }

        public bool RegistrarCompra(Compra compra)
        {
            if (compra == null)
            {
                throw new ArgumentNullException("La compra no puede ser nula");
            }
            if (compra.DetallesCompra == null)
            {
                throw new ArgumentException("La compra debe tener al menos un insumo");
            }
            return comprasDAL.RegistrarCompra(compra);
        }
    }
}
