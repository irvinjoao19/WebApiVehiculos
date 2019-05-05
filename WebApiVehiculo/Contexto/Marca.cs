using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contexto
{
    public class Marca
    {
        public int marcaId { get; set; }
        public int empresaId { get; set; }
        public string nombre { get; set; }
        public string imagen { get; set; }
        public int estado { get; set; }
        public List<Modelo> modelos { get; set; }
    }
}
