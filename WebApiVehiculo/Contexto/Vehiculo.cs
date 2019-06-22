using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contexto
{
    public class Vehiculo
    {
        public int vehiculoId { get; set; }
        public int empresaId { get; set; }
        public string nroPlaca { get; set; }
        public string color { get; set; }
        public string nroMotor { get; set; }
        public string nroChasis { get; set; }
        public decimal anioVehiculo { get; set; }
        public decimal cilidraje { get; set; }
        public decimal kmInicial { get; set; }
        public decimal kmMant { get; set; }
        public string dni { get; set; }
        public string ruc { get; set; }
        public int estado { get; set; }
        public int tipo { get; set; }
        public int marcaId { get; set; }
        public int modeloId { get; set; }
        public int categoriaId { get; set; }
        public int carroceriaId { get; set; }
        public int combustibleId { get; set; }
        public int localId { get; set; }
        public string nombreTipoVehiculo { get; set; }
        public string nombreMarca { get; set; }
        public string imagenMarca { get; set; }
        public string nombreModelo { get; set; }
        public string nombreCategoria { get; set; }
        public string nombreCarroceria { get; set; }
        public string nombreCombustible { get; set; }
        public string estadoAuto { get; set; }
        public string colorEstado { get; set; }
        public string nombreConductor { get; set; }
        public List<RegistroLaboral> registros { get; set; }
        public List<CheckListCabecera> checkListCabeceras { get; set; }
        public List<MantenimientoGeneral> mantenimientos { get; set; }
    }
}
