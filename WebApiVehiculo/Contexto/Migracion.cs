using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contexto
{
  public  class Migracion
    {
        public List<Vehiculo> vehiculos { get; set; }
        public List<Combustible> combustibles { get; set; }
        public List<Tipo> tipos { get; set; }
        public List<Marca> marcas { get; set; }
    }
}
