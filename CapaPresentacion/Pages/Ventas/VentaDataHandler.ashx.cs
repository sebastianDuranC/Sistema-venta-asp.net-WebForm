using CapaEntidades;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace CapaPresentacion.Pages.Ventas
{
    public class VentaDataHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            try
            {
                // Obtener la lista de ventas desde la capa de negocio
                VentaBLL cn_venta = new VentaBLL();
                DataTable ventas = cn_venta.ListarVentas();

                // Convertir DataTable a lista de diccionarios
                var listaVentas = new List<Dictionary<string, object>>();
                foreach (DataRow row in ventas.Rows)
                {
                    var dict = new Dictionary<string, object>();
                    foreach (DataColumn col in ventas.Columns)
                    {
                        dict[col.ColumnName] = row[col];
                    }
                    listaVentas.Add(dict);
                }

                var resultado = new
                {
                    data = listaVentas
                };

                JavaScriptSerializer js = new JavaScriptSerializer();
                string json = js.Serialize(resultado);

                context.Response.Write(json);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                context.Response.Write("{\"error\":\"" + ex.Message + "\"}");
            }
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }
}