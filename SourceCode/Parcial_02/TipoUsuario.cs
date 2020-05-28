using System;

namespace Parcial_02
{
    public class TipoUsuario
    {
        public string user { get; set; }
        public string password { get; set; }
        public string admin { get; set; }
        
        public DateTime dia = DateTime.Now;

        public TipoUsuario()
        {
            user = "";
            password = "";
            admin = "";
        }
    }
}