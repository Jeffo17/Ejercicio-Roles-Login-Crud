using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _24Julio.Models
{
    public class Cuentas
    {
        public string Numero { get; set; }
        public string SaldoTotal { get; set; }
        public string CodigoSocio { get; set; }
        public int Estado { get; set; }

        public virtual Socios CodigoSocioNavigation { get; set; }
    }
}
