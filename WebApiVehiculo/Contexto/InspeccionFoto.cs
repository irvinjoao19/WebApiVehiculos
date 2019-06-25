using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contexto
{
    public class InspeccionFoto
    {
        public int fotoId { get; set; }
        public int inspeccionFotoId { get; set; }
        public int inspeccionId { get; set; }
        public string nombreFoto { get; set; }
        public int estado { get; set; }
        public int usuario { get; set; }

    }
}
