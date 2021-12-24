using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Proyecto1.model
{
    class Ubicacion
    {


        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        [MaxLength(20)]
        public double latitude { get; set; }
        [MaxLength(20)]
        public double longitude { get; set; }
        [MaxLength(600)]
        public string descripcion_ubicacion { get; set; }
        [MaxLength(100)]
        public string descripcion_corta { get; set; }

        public byte[] foto { get; set; }

    }
}