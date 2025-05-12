using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_RolPermisos
    {
        private CD_Conexion conexion = new CD_Conexion();
        SqlDataReader leer;
        SqlCommand comandoQuery = new SqlCommand(); //Obj para utilizar las sentencias sql (insert, update, delete, select, procedure stored)
        public void actualizarRolPermisos(int rolId, int formId, bool check)
{
    try
    {
        using (var connection = conexion.AbrirBd())
        {
            using (var comandoQuery = new SqlCommand("sp_ActualizarRolPermisos", connection))
            {
                comandoQuery.CommandType = System.Data.CommandType.StoredProcedure;
                comandoQuery.Parameters.AddWithValue("@IsAllowed", check);
                comandoQuery.Parameters.AddWithValue("@RolId", rolId);
                comandoQuery.Parameters.AddWithValue("@FormId", formId);
                comandoQuery.ExecuteNonQuery();
            }
        }
    }
    catch (Exception ex)
    {
        throw new Exception($"Error en actualizarRolPermisos: {ex.Message}", ex);
    }
}

        public bool ObtenerEsPermitidoForm(int rolId, int formId)
        {
            bool esPermitido = false;
            comandoQuery.Connection = conexion.AbrirBd();
            comandoQuery.CommandText = "sp_ObtenerEsPermitidoForm";
            comandoQuery.CommandType = System.Data.CommandType.StoredProcedure;
            comandoQuery.Parameters.AddWithValue("@RolId", rolId);
            comandoQuery.Parameters.AddWithValue("@FormId", formId);
            leer = comandoQuery.ExecuteReader();
            if (leer.Read())
            {
                esPermitido = Convert.ToBoolean(leer["EstadoId"]);
            }
            conexion.CerrarBd();
            return esPermitido;
        }
    }
}
