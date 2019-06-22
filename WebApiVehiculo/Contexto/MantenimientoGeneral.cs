using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contexto
{
    public class MantenimientoGeneral
    {
        public int generalId { get; set; }
        public int mantenimientoId { get; set; }
        public int empresaId { get; set; }
        public int tipoIE { get; set; }
        public int tipoMantenimientoId { get; set; }
        public int vehiculoId { get; set; }
        public string numeroMantenimiento { get; set; }
        public string fechaMantenimiento { get; set; }
        public string solicitadoPor { get; set; }
        public string trabajoMantenimiento { get; set; }
        public string ruc { get; set; }
        public string razonSocial { get; set; }
        public string fechaInicial { get; set; }
        public string fechaFinal { get; set; }
        public decimal km { get; set; }
        public decimal totalSoles { get; set; }
        public decimal totalDolares { get; set; }
        public int estado { get; set; }
        public int usuario { get; set; }
        public List<MantenimientoDetalle> detalles { get; set; }
    }
}
