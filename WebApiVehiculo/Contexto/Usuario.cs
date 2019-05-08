using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contexto
{
    public class Usuario
    {
        public string login { get; set; }
        public string pass { get; set; }
        public int usuarioId { get; set; }
        public string nroDocumento { get; set; }
        public string apellidos { get; set; }
        public string nombre { get; set; }
        public string email { get; set; }
        public int perfilId { get; set; }
        public string fotoUrl { get; set; }
        public int estado { get; set; }
        public List<Empresa> empresas { get; set; }
    }
}
