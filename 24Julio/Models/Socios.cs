using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _24Julio.Models
{
    public class Socios
    {
        public Socios()
        {
            Cuenta = new HashSet<Cuentas>();
        }

        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public int Estado { get; set; }

        public virtual ICollection<Cuentas> Cuenta { get; set; }
    }
}
