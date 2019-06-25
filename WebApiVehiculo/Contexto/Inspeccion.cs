using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contexto
{
    public class Inspeccion
    {
        public int inspeccionId { get; set; }
        public int inspeccionCabeceraId { get; set; }
        public string nroInspeccion { get; set; }
        public int empresaInspeccionId { get; set; }
        public string nombreEmpresaInspeccionId { get; set; }
        public string fechaInspeccion { get; set; }
        public string sedeInspeccion { get; set; }
        public string fechaRecibidoOpe { get; set; }
        public decimal kmRecibidoOpe { get; set; }
        public int conductorRecibidoOpe { get; set; }
        public string nombreConductorRecibidoOpe { get; set; }
        public int combustibleRecibidoOpe { get; set; }
        public string nombreConbustibleRecibidoOpe { get; set; }
        public string fechaEnvioProvee { get; set; }
        public decimal kmEnvioProvee { get; set; }
        public int conductorEnvioProvee { get; set; }
        public string nombreConductorEnvioProvee { get; set; }
        public int combustibleEnvioProvee { get; set; }
        public string nombreCombustibleEnvioProvee { get; set; }
        public string fechaRetiroTrans { get; set; }
        public decimal kmRetiroTrans { get; set; }
        public int conductorRetiroTrans { get; set; }
        public string nombreConductorRetiroTrans { get; set; }
        public int combustibleRetiroTrans { get; set; }
        public string nombreCombustibleRetiroTrans { get; set; }
        public string fechaEntregaOpe { get; set; }
        public decimal kmEntregaOpe { get; set; }
        public int conductorEntregaOpe { get; set; }
        public string nombreConductorEntregaOpe { get; set; }
        public int combustibleEntregaOpe { get; set; }
        public string nombreCombustibleEntregaOpe { get; set; }
        public int usuarioId { get; set; }
        public int vehiculoId { get; set; }
        public int empresaId { get; set; }
        public List<InspeccionDetalle> detalles { get; set; }
        public List<InspeccionHurt> hurts { get; set; }
        public List<InspeccionFoto> fotos { get; set; }

    }
}
