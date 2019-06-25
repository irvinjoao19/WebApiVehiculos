using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contexto
{
    public class InspeccionDetalle
    {
        public int detalleId { get; set; }
        public int inspeccionDetalleId { get; set; }
        public int inspeccionId { get; set; }
        public int vehiculoId { get; set; }
        public int caracteristicasId { get; set; }
        public int estadoInspeccionId { get; set; }
        public string fechaInspeccionDetalle { get; set; }
        public string observacion { get; set; }
        public int estado { get; set; }
        public int usuario { get; set; } 
    }
}
