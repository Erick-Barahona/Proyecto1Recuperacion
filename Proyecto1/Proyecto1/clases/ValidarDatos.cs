using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1.clases
{
    public class ValidarDatos
    {
        bool respuesta;

        public bool validarDescripcionCorta(string descripcion_corta)
        {

            respuesta = (descripcion_corta != null) ? false : true;

            return respuesta;
        }
        public bool validarUbicacion(string descripcion_larga)
        {

            respuesta = (descripcion_larga != null) ? false : true;

            return respuesta;
        }



    }
}