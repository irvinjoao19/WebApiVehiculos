using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contexto
{
    public class Caracteristicas
    {

        public int caracteristicaId { get; set; }
        public int empresaId { get; set; }
        public int formatoId { get; set; }
        public int titulo { get; set; }
        public string codigoCaracteristica { get; set; }
        public string descripcionCaracteristica { get; set; }
        public int orden { get; set; }
        public int aplicaFecha { get; set; }
        public int aplicaObservacion { get; set; }
        public int estado { get; set; }

    }
}
