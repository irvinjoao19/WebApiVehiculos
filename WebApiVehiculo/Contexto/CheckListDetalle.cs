
namespace Contexto
{
    public class CheckListDetalle
    {
        public int detalleId { get; set; }
        public int checkListDetalleId { get; set; }
        public int checkListCabeceraId { get; set; }    
        public int checkListId { get; set; }
        public string numeroCheckList { get; set; }
        public string fechaCheckList { get; set; }
        public string nroPlacaCheckList { get; set; }
        public string tipoVista { get; set; }
        public string nombreLado { get; set; }
        public string nombrePartes { get; set; }
        public string otrosCheckList { get; set; }
        public string observacionesCheckList { get; set; }
        public string fotoCheckList { get; set; }
        public int estadoIdCheckList { get; set; }
        public string estadoCheckList { get; set; }
        public int ladoVehiculoId { get; set; }     
        public int ladoParteVehiculoId { get; set; }
        public int empresaId { get; set; }
    }
}
