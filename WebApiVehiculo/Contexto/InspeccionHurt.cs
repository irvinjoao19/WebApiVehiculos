using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contexto
{
    public class InspeccionHurt
    {
        public int controlId { get; set; }
        public int controlInspeccionId { get; set; }
        public int inspeccionId { get; set; }
        public string tipoControlId { get; set; }
        public int estadoControlId { get; set; }
        public string lugarControl { get; set; }
        public string observacion { get; set; }
        public int estado { get; set; }
        public int usuario { get; set; }         
    }
}
