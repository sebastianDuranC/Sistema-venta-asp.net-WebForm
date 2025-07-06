using System;
using System.Collections.Generic;
using System.Data;
using CapaDatos;
using CapaEntidades;

namespace CapaNegocio
{
    public class VentaBLL
    {
        public DataTable ListarVentas()
        {
            try
            {
                VentaDAL datos = new VentaDAL();
                return datos.ObtenerVentas();
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la capa de negocio al listar ventas: " + ex.Message);
            }
        }

        public bool RegistrarVentas(Venta venta, List<DetalleVenta> listaDetalles)
        {
            //VALIDAR VENTA
            if (listaDetalles == null || listaDetalles.Count == 0) { 
                throw new ArgumentException("Debe ingresar al menos un producto en la venta");
            }
            if (venta.UsuarioId == 0)
            {
                throw new ArgumentException("Debe tener un usuario activo par la venta");
            }
            if (venta.ClienteId == 0)
            {
                throw new ArgumentException("Debe seleccionar un cliente par la venta");
            }
            if (venta.MetodoPagoId == 0)
            {
                throw new ArgumentException("Debe seleccionar un metodo de pago par la venta");
            }
            if (venta.MontoRecibido <= 0)
            {
                throw new ArgumentException("Debe seleccionar un monto mayor a la venta");
            }

            //DETALLEVENTA
            DataTable dtDetalle = new DataTable();
            dtDetalle.Columns.Add("ProductoId", typeof(int));
            dtDetalle.Columns.Add("Cantidad", typeof(decimal));
            dtDetalle.Columns.Add("SubTotal", typeof(decimal));

            foreach (var detalle in listaDetalles)
            {
                dtDetalle.Rows.Add(detalle.ProductoId, detalle.Cantidad, detalle.SubTotal);
            }

            VentaDAL datos = new VentaDAL();
            return datos.RegistrarVentas(venta, dtDetalle);
        }

        public int ObtenerIdUsuario(string nombreUsuario)
        {
            try
            {
                UsuarioDAL datos = new UsuarioDAL();
                return datos.ObtenerRolIdNombre(nombreUsuario);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la capa de negocio al obtener el ID de usuario: " + ex.Message);
            }
        }

        public DataTable ObtenerVentaPorId(int ventaId)
        {
            VentaDAL cD_Ventas = new VentaDAL();
            return cD_Ventas.ObtenerVentaId(ventaId);
        }

        public DataTable ObtenerDetallesVentaPorVentaId(int ventaId)
        {
            DetalleVentaDAL cD_Ventas = new DetalleVentaDAL();
            return cD_Ventas.ObtenerDetalleVenta(ventaId);
        }

        public void EditarVenta(Venta venta, List<DetalleVenta> detalles)
        {
            VentaDAL cD_Ventas = new VentaDAL();
            cD_Ventas.EditarVenta(venta, detalles);
        }

        public bool EliminarVenta(int ventaId)
        {
            VentaDAL cd_ventas = new VentaDAL();
            try
            {
                return cd_ventas.EliminarVenta(ventaId);
            }
            catch (ApplicationException ex)
            {
                // Errores controlados (venta no existe o ya anulada)
                throw ex;  // Propagar el mensaje
            }
        }
    }
}
