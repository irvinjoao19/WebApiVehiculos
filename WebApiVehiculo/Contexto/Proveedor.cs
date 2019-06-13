using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contexto
{
    public class Proveedor
    {
        public int proveedorId { get; set; }
        public int empresaId { get; set; }
        public string ruc { get; set; }
        public string razonSocial { get; set; }
    }
}
