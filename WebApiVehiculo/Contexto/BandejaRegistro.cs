using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contexto
{
    public class BandejaRegistro
    {
        public int registroCombustibleId { get; set; }
        public int vehiculoId { get; set; }
        public int empresaId { get; set; }
        public string nroPlacaVehiculo { get; set; }
        public decimal anioVehiculo { get; set; }
        public string nombreMarca { get; set; }
        public string imagenMarca { get; set; }
        public string nombreModelo { get; set; }
        public string fechaAtencion { get; set; }
        public decimal cantidadRegistro { get; set; }
        public string fullRegistro { get; set; }
        public string  nombreCombustible { get; set; }
        public int estado { get; set; }
        public string abreviaturaEstado { get; set; }
        public string colorEstado { get; set; }
        public string nroVoucher { get; set; }
        public string fechaEmision { get; set; }
        public decimal km { get; set; }
        public decimal cantidadGalones { get; set; }
        public decimal precioVoucher { get; set; }
        public string fotoVoucher { get; set; }
         
    }
}
