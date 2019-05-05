using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contexto
{
    public class Tipo
    {
        public int tipoVehiculoId { get; set; }
        public int empresaId { get; set; }
        public string nombre { get; set; }
        public string imagen { get; set; }
        public int estado { get; set; }
        public List<Categoria> categorias { get; set; }
    }
}
