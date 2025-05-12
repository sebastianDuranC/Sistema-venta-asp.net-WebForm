using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Conexion
    {
        //1.- cadena de conexión
        string cadena =  ConfigurationManager.ConnectionStrings["conexionSql"].ConnectionString;

        // agregar el using System.Data.SqlClient; si sale error agregar el NUget
        public SqlConnection conectarBd = new SqlConnection();

        public CD_Conexion()
        {
            conectarBd.ConnectionString = cadena;
        }

        public SqlConnection AbrirBd()
        {
            if (conectarBd.State == ConnectionState.Closed)
            {
                conectarBd.Open();
            }
            return conectarBd;
        }

        public SqlConnection CerrarBd()
        {
            if (conectarBd.State == ConnectionState.Open)
            {
                conectarBd.Close();
            }
            return conectarBd;
        }

    }
}
