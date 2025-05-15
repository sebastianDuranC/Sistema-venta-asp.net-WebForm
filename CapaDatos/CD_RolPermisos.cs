using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
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
                conexion.CerrarBd();
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
                esPermitido = Convert.ToBoolean(leer["Estado"]);
            }
            conexion.CerrarBd();
            return esPermitido;
        }

        public bool UsuarioTienePermisoForm(string currentUsuario, string currentNombreForm)
        {
            CD_Login cD_Usuario = new CD_Login();
            int rolId = cD_Usuario.ObtenerRolIdNombre(currentUsuario);
            CD_Form cD_Form = new CD_Form();
            int formularioId = cD_Form.ObtenerFormularioIdNombre(currentNombreForm);
            if (rolId != -1 && formularioId != -1)
            {
                comandoQuery.Connection = conexion.AbrirBd();
                comandoQuery.CommandText = "sp_ObtenerNumeroPermisos";
                comandoQuery.CommandType = CommandType.StoredProcedure;
                comandoQuery.Parameters.Clear();
                // Comparamos si el rolID tiene permisos en el formularioID
                comandoQuery.Parameters.AddWithValue("@RolId", rolId);
                comandoQuery.Parameters.AddWithValue("@FormularioId", formularioId);
                int tienePermiso = Convert.ToInt32(comandoQuery.ExecuteScalar()); //Si es 1 el rol tiene permisos al formulario si es 0 entonces no
                conexion.CerrarBd();
                return tienePermiso > 0;
            }
            return false;
        }
    }
}
