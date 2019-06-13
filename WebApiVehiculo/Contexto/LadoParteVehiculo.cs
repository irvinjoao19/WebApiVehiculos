using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contexto
{
    public class LadoParteVehiculo
    {
        public int ladoparteVehiculoId { get; set; }
        public int ladoVehiculoId { get; set; }
        public string nombreLado { get; set; }
        public int partesVehiculoId { get; set; }
        public string nombrePartesVehiculo { get; set; }
    }
}
