using Contexto;
using Negocio;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace WebApiVehiculo.Controllers
{
    public class DsigeVehiculoController : ApiController
    {

        [HttpPost]
        [Route("api/Vehiculo/Migracion")]
        public IHttpActionResult VehiculoList()
        {
            try
            {
                Migracion v = NegocioDao.GetMigracion();
                if (v != null)
                {
                    return Ok(v);
                }
                else return BadRequest("No hay vehiculos");
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        [HttpPost]
        [Route("api/Vehiculo/SaveVehiculo")]
        public IHttpActionResult SaveVehiculo()
        {
            try
            {
                var fotos = HttpContext.Current.Request.Files;
                var json = HttpContext.Current.Request.Form["model"];
                Vehiculo p = JsonConvert.DeserializeObject<Vehiculo>(json);
                Mensaje m = NegocioDao.SaveRegistroLaboral(p);
                if (m != null)
                {
                    string path = HttpContext.Current.Server.MapPath($"~/fotos/{p.empresaId}");

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    for (int i = 0; i < fotos.Count; i++)
                    {
                        string fileName = Path.GetFileName(fotos[i].FileName);
                        fotos[i].SaveAs(path + fileName);
                    }

                    return Ok(m);
                }
                else return BadRequest("Error");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("api/Vehiculo/BandejaRegistro")]
        public IHttpActionResult BandejaRegistro(Filtro f)
        {
            try
            {
                List<BandejaRegistro> bandeja = NegocioDao.GetBandejaRegistros(f);
                if (bandeja != null)
                {
                    return Ok(bandeja);
                }
                else return BadRequest("No hay resultados");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
