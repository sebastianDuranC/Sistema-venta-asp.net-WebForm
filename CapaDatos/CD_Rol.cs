using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidades;

namespace CapaDatos
{
    public class CD_Rol
    {
        private CD_Conexion conexion = new CD_Conexion();
        SqlDataReader leer;
        SqlCommand comandoQuery = new SqlCommand(); //Obj para utilizar las sentencias sql (insert, update, delete, select, procedure stored)

        public List<Rol> obtenerRol()
        {
            List<Rol> lista = new List<Rol>();
            comandoQuery.Connection = conexion.AbrirBd(); //abrir la conexion
            comandoQuery.CommandText = "sp_ObtenerRoles"; //nombre del procedimiento almacenado
            comandoQuery.CommandType = CommandType.StoredProcedure; //tipo de comando
            leer = comandoQuery.ExecuteReader(); //ejecutar el comando y almacenar el resultado en leer
            while (leer.Read())
            {
                lista.Add(new Rol {
                    Id = Convert.ToInt32(leer["Id"]),
                    Nombre = leer["Nombre"].ToString()
                });
            }
            conexion.CerrarBd(); //cerrar la conexion
            return lista; //retornar la lista con los roles
        }
    }
}
