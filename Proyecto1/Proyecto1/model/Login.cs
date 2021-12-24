using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1.model
{
    class Login
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        [MaxLength(200)]
        public string nombre { get; set; }
        [MaxLength(200)]
        public string usuario { get; set; }
        [MaxLength(200)]
        public string password { get; set; }
       
   
    }
}
