using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contexto
{
    public class Combustible
    {
        public int combustibleId { get; set; }
        public int empresaId { get; set; }
        public string nombre { get; set; }
        public string imagen { get; set; }
        public decimal precioPromedio { get; set; }
        public int estado { get; set; }         
    }
}
