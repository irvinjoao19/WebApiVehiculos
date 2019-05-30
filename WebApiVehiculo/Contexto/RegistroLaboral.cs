using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contexto
{
    public class RegistroLaboral
    {
        public int id { get; set; }
        public int registroId { get; set; }
        public int usuarioId { get; set; }
        public int empresaId { get; set; }
        public int tipoRegistro { get; set; }
        public string nombreTipoRegistro { get; set; }
        public int vehiculoId { get; set; }
        public string fecha { get; set; }
        public double km { get; set; }
        public string fotoRegistro { get; set; }
        public string nroOrdenRegistro { get; set; }
        public string observaciones { get; set; }
        public int estado { get; set; }
        public string fechaAtencion { get; set; }
        public int tipoCombustibleId { get; set; }
        public string nombreCombustible { get; set; }
        public double cantidadGalones { get; set; }
        public int full { get; set; }
        public string nroVoucher { get; set; }
        public double precio { get; set; }
        public double total { get; set; }
        public string fechaDocumento { get; set; }

    }
}
