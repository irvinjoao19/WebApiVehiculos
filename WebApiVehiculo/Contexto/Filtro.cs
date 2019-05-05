using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contexto
{
    public class Filtro
    {
        public int usuarioId { get; set; }
        public int empresaId { get; set; }
        public int estado { get; set; }
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
        public string search { get; set; }

    }
}
