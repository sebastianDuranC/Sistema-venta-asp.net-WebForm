﻿using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class ProductoInsumoBLL
    {
        ProductoInsumoDAL productoInsumoDAL = new ProductoInsumoDAL();

        public DataTable ObtenerRecetaPorProductoId(int productoId)
        {
            return productoInsumoDAL.ObtenerRecetaPorProductoId(productoId);
        }
    }
}
