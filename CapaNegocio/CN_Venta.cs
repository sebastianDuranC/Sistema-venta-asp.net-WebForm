using System;
using System.Collections.Generic;
using System.Data;
using CapaDatos;
using CapaEntidades;

namespace CapaNegocio
{
    public class CN_Venta
    {
        public DataTable ListarVentas()
        {
            try
            {
                CD_Ventas datos = new CD_Ventas();
                return datos.ObtenerVentas();
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la capa de negocio al listar ventas: " + ex.Message);
            }
        }

        public bool RegistrarVentas(int enLocal, int clienteId, int usuarioId, int metodoPagoId, List<DetalleVenta> listaDetalles)
        {
            if (listaDetalles == null || listaDetalles.Count == 0)
                return false;

            DataTable dtDetalle = new DataTable();
            dtDetalle.Columns.Add("ProductoId", typeof(int));
            dtDetalle.Columns.Add("Cantidad", typeof(decimal));
            dtDetalle.Columns.Add("SubTotal", typeof(decimal));

            foreach (var detalle in listaDetalles)
            {
                dtDetalle.Rows.Add(detalle.ProductoId, detalle.Cantidad, detalle.SubTotal);
            }

            CD_Ventas datos = new CD_Ventas();
            return datos.RegistrarVentas(enLocal, clienteId, usuarioId, metodoPagoId, dtDetalle);
        }

        public int ObtenerIdUsuario(string nombreUsuario)
        {
            try
            {
                CD_Login datos = new CD_Login();
                return datos.ObtenerRolIdNombre(nombreUsuario);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la capa de negocio al obtener el ID de usuario: " + ex.Message);
            }
        }

    }
}
