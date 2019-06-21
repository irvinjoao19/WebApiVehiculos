using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contexto
{
    public class CheckListCabecera
    {
        public int checkListCabeceraId { get; set; }
        public int checkListId { get; set; }
        public int vehiculoId { get; set; }
        public string numeroCheckList { get; set; }
        public string nroPlaca { get; set; }
        public string fecha { get; set; }
        public int turnoId { get; set; }
        public string nombreTurno { get; set; }
        public int empresaId { get; set; }
        public int usuarioId { get; set; }
        public int estado { get; set; }
        public List<CheckListDetalle> detalles { get; set; }
    }
}
