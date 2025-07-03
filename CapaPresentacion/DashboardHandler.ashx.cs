using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace CapaPresentacion
{
    /// <summary>
    /// Descripción breve de DashboardHandler
    /// </summary>
    public class DashboardHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            // Leemos el parámetro 'grafica' para saber qué datos devolver
            string grafica = context.Request.QueryString["grafica"];
            string jsonResponse = "";

            switch (grafica)
            {
                case "ventasSemana":
                    jsonResponse = ObtenerDatosVentasSemana();
                    break;
                case "topProductos":
                    jsonResponse = ObtenerDatosTopProductos();
                    break;
            }

            // Devolvemos la respuesta como JSON
            context.Response.ContentType = "application/json";
            context.Response.Write(jsonResponse);
        }

        private string ObtenerDatosVentasSemana()
        {
            DashboardBLL cnDashboard = new DashboardBLL();
            var ventasSemanaList = cnDashboard.ObtenerVentasSemana();
            var ventasSemanaLabels = new List<string>();
            var ventasSemanaData = new List<decimal>();

            foreach (var item in ventasSemanaList)
            {
                ventasSemanaLabels.Add(item["Dia"].ToString());
                ventasSemanaData.Add(Convert.ToDecimal(item["Total"]));
            }

            var chartData = new { Labels = ventasSemanaLabels, Data = ventasSemanaData };
            return new JavaScriptSerializer().Serialize(chartData);
        }

        private string ObtenerDatosTopProductos()
        {
            DashboardBLL cnDashboard = new DashboardBLL();
            var topProductosList = cnDashboard.ObtenerTopProductosVendidos();
            var topProductosLabels = new List<string>();
            var topProductosData = new List<int>();

            foreach (var item in topProductosList)
            {
                topProductosLabels.Add(item["Producto"].ToString());
                topProductosData.Add(Convert.ToInt32(item["Cantidad"]));
            }

            var chartData = new { Labels = topProductosLabels, Data = topProductosData };
            return new JavaScriptSerializer().Serialize(chartData);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}