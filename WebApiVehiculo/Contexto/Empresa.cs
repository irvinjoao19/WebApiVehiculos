using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contexto
{
    public class Empresa
    {
        public int usuarioId { get; set; }
        public int empresaId { get; set; }   
        public string ruc { get; set; }
        public string nombreEmpresa { get; set; }
        public string logoEmpresa { get; set; }
     }
}
