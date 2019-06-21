using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contexto
{
    public class MantenimientoDetalle
    {
        public int detalleId { get; set; }
        public int generalId { get; set; }
        public int mantenimientoDetalleId { get; set; }
        public int tipoDocumentoId { get; set; }
        public string numeroDocumento { get; set; }
        public string fechaMantenimientoDetalle { get; set; }
        public int tipoMonedaId { get; set; }
        public decimal tipoCambio { get; set; }
        public string descripcion { get; set; }
        public decimal cantidad { get; set; }
        public decimal precio { get; set; }
        public decimal total { get; set; }
        public string descripcionTrabajo { get; set; }
        public int estado { get; set; }
        public int usuario { get; set; } 
    }
}
