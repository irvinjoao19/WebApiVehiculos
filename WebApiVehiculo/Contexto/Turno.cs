using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contexto
{
    public class Turno
    {
        public int turnoId { get; set; }
        public int empresaId { get; set; }
        public string nombreTurno { get; set; }
        public int estado { get; set; }
        
    }
}
