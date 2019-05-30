using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contexto
{
    public class Historial
    {
        public int historialId { get; set; }
        public string fecha { get; set; }
        public decimal km { get; set; }
        public string combustible { get; set; }
        public decimal cantidad { get; set; }
        public decimal precio { get; set; }
        public decimal total { get; set; }
    }
}
