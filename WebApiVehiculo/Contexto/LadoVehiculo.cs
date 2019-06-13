using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contexto
{
    public class LadoVehiculo
    {
        public int ladoVehiculoId { get; set; }
        public int empresaId { get; set; }
        public string tipoVista { get; set; }
        public string nombreLado { get; set; }
        public int estado { get; set; }
        public List<LadoParteVehiculo> parteVehiculos { get; set; }
    }
}
