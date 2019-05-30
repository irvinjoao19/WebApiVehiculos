using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contexto
{
    public class InicioFinLabores
    {

        public int vehiculoId { get; set; }
        public string nroPlaca { get; set; }
        public decimal anioVehiculo { get; set; }
        public string nombreMarca { get; set; }
        public string imagenMarca { get; set; }
        public string nombreModelo { get; set; }
        public decimal totalKm { get; set; }
        public decimal rendimiento { get; set; }
        public List<InicioLaboresDetalle> detalles { get; set; }

    }
}
