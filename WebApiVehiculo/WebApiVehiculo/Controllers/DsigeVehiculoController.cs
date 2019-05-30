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
        [Route("api/Vehiculo/Login")]
        public IHttpActionResult Usuario(Filtro f)
        {
            try
            {
                Usuario u = NegocioDao.GetLogin(f);
                if (u != null)
                {
                    if (u.pass == "Error")
                    {
                        return BadRequest("Contraseña Incorrecta");
                    }
                    else
                    {
                        return Ok(u);
                    }

                }
                else return BadRequest("Usuario no existe");
            }
            catch (Exception e)
            {
                throw e;
            }
        }



        [HttpPost]
        [Route("api/Vehiculo/Migracion")]
        public IHttpActionResult VehiculoList(Filtro f)
        {
            try
            {
                Migracion v = NegocioDao.GetMigracion(f);
                if (v != null)
                {
                    return Ok(v);
                }
                else return BadRequest("No hay datos para esa empresa");
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
                    string path = HttpContext.Current.Server.MapPath($"~/fotos/{p.empresaId}/");

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

        [HttpPost]
        [Route("api/Vehiculo/BandejaRegistroPlaca")]
        public IHttpActionResult BandejaRegistroPlaca(Filtro f)
        {
            try
            {
                List<BandejaRegistro> bandeja = NegocioDao.GetBandejaRegistrosPlaca(f);
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

        [HttpPost]
        [Route("api/Vehiculo/UpdateEstado")]
        public IHttpActionResult UpdateEstado(Filtro f)
        {
            try
            {
                Mensaje m = NegocioDao.UpdateEstado(f);
                if (m != null)
                {
                    return Ok(m);
                }
                else return BadRequest("Error");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        [Route("api/Vehiculo/RegistroCombustible")]
        public IHttpActionResult RegistroCombustible()
        {          
            try
            {
                var fotos = HttpContext.Current.Request.Files;
                var json = HttpContext.Current.Request.Form["model"];
                RegistroLaboral b = JsonConvert.DeserializeObject<RegistroLaboral>(json);
                Mensaje m = NegocioDao.RegistroCombustible(b);
                if (m != null)
                {
                    string path = HttpContext.Current.Server.MapPath($"~/fotos/{b.empresaId}/");

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
        [Route("api/Vehiculo/Historial")]
        public IHttpActionResult Historial(Filtro f)
        {
            try
            {
                List<Historial> h = NegocioDao.GetHistorial(f);
                if (h != null)
                {
                    return Ok(h);
                }
                else return BadRequest("No hay Historial.");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        [Route("api/Vehiculo/InicioFinLabores")]
        public IHttpActionResult InicioFinLabores(Filtro f)
        {
            try
            {
                List<InicioFinLabores> i = NegocioDao.GetInicioFinLabores(f);
                if (i != null)
                {
                    return Ok(i);
                }
                else return BadRequest("No hay datos.");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
