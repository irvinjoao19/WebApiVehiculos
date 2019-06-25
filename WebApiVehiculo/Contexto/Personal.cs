using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contexto
{
    public class Personal
    {
        public int personalId { get; set; }
        public int empresaId { get; set; }
        public string nroDocumento { get; set; }
        public int tipoDoc { get; set; }
        public string apellidos { get; set; }
        public string nombres { get; set; }
        public int cargoId { get; set; }

    }
}
