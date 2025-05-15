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
    public class CD_Form
    {
        private CD_Conexion conexion = new CD_Conexion();
        SqlCommand comandoQuery = new SqlCommand();
        public bool RegistrarForm(string nombreForm, string formRuta)
        {
            comandoQuery.Connection = conexion.AbrirBd();
            comandoQuery.CommandText = "sp_InsertarForm";
            comandoQuery.CommandType = CommandType.StoredProcedure;
            comandoQuery.Parameters.Clear();
            comandoQuery.Parameters.AddWithValue("@FormNombre", nombreForm);
            comandoQuery.Parameters.AddWithValue("@FormRuta", formRuta);
            int filasAfectadas = comandoQuery.ExecuteNonQuery();
            comandoQuery.Connection = conexion.CerrarBd();
            if (filasAfectadas == 0)
            {
                return false;
            }
            return true;
        }

        public List<Permisos> ObtenerForm()
        {
            List<Permisos> values = new List<Permisos>();
            comandoQuery.Connection = conexion.AbrirBd();
            comandoQuery.CommandText = "sp_ObtenerForm";
            comandoQuery.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = comandoQuery.ExecuteReader();
            while (reader.Read())
            {
                values.Add(new Permisos
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    FormNombre = reader["FormNombre"].ToString(),
                    FormRuta = reader["FormRuta"].ToString(),
                    Estado = Convert.ToBoolean(reader["Estado"])
                });
            }
            comandoQuery.Connection = conexion.CerrarBd();
            return values;
        }

        public int ObtenerFormularioIdNombre(string currentNombreForm)
        {
            comandoQuery.Connection = conexion.AbrirBd();
            comandoQuery.CommandText = "sp_ObtenerFormularioIdNombre";
            comandoQuery.CommandType = CommandType.StoredProcedure;
            comandoQuery.Parameters.Clear();

            comandoQuery.Parameters.AddWithValue("@FormNombre", currentNombreForm);
            object resultado = comandoQuery.ExecuteScalar();
            if (resultado != null && resultado != DBNull.Value)
            {
                return Convert.ToInt32(resultado);
            }
            conexion.CerrarBd();
            return -1; // Si no se encuentra el formulario, devolvemos -1
        }
    }
}
