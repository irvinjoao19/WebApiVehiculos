using Contexto;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class NegocioDao
    {

        private static string db = ConfigurationManager.ConnectionStrings["conexionDsige"].ConnectionString;

        public static Migracion GetMigracion()
        {
            try
            {
                Migracion migracion = null;

                using (SqlConnection con = new SqlConnection(db))
                {
                    con.Open();

                    migracion = new Migracion();

                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "DSIGE_Tbl_Vehiculos_Listar";
                    cmd.Parameters.Add("@Empresa", SqlDbType.Int).Value = 1;
                    cmd.Parameters.Add("@Local", SqlDbType.Int).Value = 1;
                    cmd.Parameters.Add("@Tipo", SqlDbType.Int).Value = 1;
                    cmd.Parameters.Add("@Estado", SqlDbType.Int).Value = 1;
                    cmd.Parameters.Add("@Usuario", SqlDbType.Int).Value = 1;

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        List<Vehiculo> vehiculos = new List<Vehiculo>();

                        while (dr.Read())
                        {
                            vehiculos.Add(new Vehiculo()
                            {
                                vehiculoId = dr.GetInt32(0),
                                empresaId = dr.GetInt32(1),
                                nroPlaca = dr.GetString(2),
                                color = dr.GetString(3),
                                nroMotor = dr.GetString(4),
                                nroChasis = dr.GetString(5),
                                anioVehiculo = Convert.ToDecimal(dr.GetDecimal(6)),
                                cilidraje = Convert.ToDecimal(dr.GetDecimal(7)),
                                kmInicial = Convert.ToDecimal(dr.GetDecimal(8)),
                                kmMant = Convert.ToDecimal(dr.GetDecimal(9)),
                                dni = dr.GetString(10),
                                ruc = dr.GetString(11),
                                estado = dr.GetInt32(12),
                                tipo = dr.GetInt32(17),
                                marcaId = dr.GetInt32(18),
                                modeloId = dr.GetInt32(19),
                                categoriaId = dr.GetInt32(20),
                                carroceriaId = dr.GetInt32(21),
                                combustibleId = dr.GetInt32(22),
                                localId = dr.GetInt32(23),
                                nombreTipoVehiculo = dr.GetString(24),
                                nombreMarca = dr.GetString(25),
                                imagenMarca = dr.GetString(26),
                                nombreModelo = dr.GetString(27),
                                nombreCategoria = dr.GetString(28),
                                nombreCarroceria = dr.GetString(29),
                                nombreCombustible = dr.GetString(30),
                                estadoAuto = dr.GetString(31),
                                colorEstado = dr.GetString(32),
                                nombreConductor = dr.GetString(33)
                            });
                        }
                        migracion.vehiculos = vehiculos;
                    }

                    SqlCommand cmdC = con.CreateCommand();
                    cmdC.CommandTimeout = 0;
                    cmdC.CommandType = CommandType.StoredProcedure;
                    cmdC.CommandText = "Movil_List_Combustible";
                    cmdC.Parameters.Add("@empresaId", SqlDbType.Int).Value = 1;

                    SqlDataReader drC = cmdC.ExecuteReader();
                    if (drC.HasRows)
                    {
                        List<Combustible> combustible = new List<Combustible>();

                        while (drC.Read())
                        {
                            combustible.Add(new Combustible()
                            {
                                combustibleId = drC.GetInt32(0),
                                empresaId = drC.GetInt32(1),
                                nombre = drC.GetString(2),
                                imagen = drC.GetString(3),
                                precioPromedio = Convert.ToDecimal(drC.GetDecimal(4)),
                                estado = drC.GetInt32(5)
                            });
                        }
                        migracion.combustibles = combustible;
                    }

                    SqlCommand cmdT = con.CreateCommand();
                    cmdT.CommandTimeout = 0;
                    cmdT.CommandType = CommandType.StoredProcedure;
                    cmdT.CommandText = "Movil_List_TipoVehiculo";
                    cmdT.Parameters.Add("@empresaId", SqlDbType.Int).Value = 1;

                    SqlDataReader drT = cmdT.ExecuteReader();
                    if (drT.HasRows)
                    {
                        List<Tipo> tipos = new List<Tipo>();

                        while (drT.Read())
                        {
                            Tipo t = new Tipo();
                            t.tipoVehiculoId = drT.GetInt32(0);
                            t.empresaId = drT.GetInt32(1);
                            t.nombre = drT.GetString(2);
                            t.imagen = drT.GetString(3);
                            t.estado = drT.GetInt32(4);

                            SqlCommand cmdCa = con.CreateCommand();
                            cmdCa.CommandTimeout = 0;
                            cmdCa.CommandType = CommandType.StoredProcedure;
                            cmdCa.CommandText = "Movil_List_CategoriaVehiculo";
                            cmdCa.Parameters.Add("@tipoVehiculoId", SqlDbType.Int).Value = t.tipoVehiculoId;

                            SqlDataReader drCa = cmdCa.ExecuteReader();
                            if (drCa.HasRows)
                            {
                                List<Categoria> categorias = new List<Categoria>();

                                while (drCa.Read())
                                {
                                    Categoria c = new Categoria();

                                    c.categoriaId = drCa.GetInt32(0);
                                    c.tipoVehiculoId = drCa.GetInt32(1);
                                    c.nombre = drCa.GetString(2);
                                    c.estado = drCa.GetInt32(3);

                                    SqlCommand cmdCar = con.CreateCommand();
                                    cmdCar.CommandTimeout = 0;
                                    cmdCar.CommandType = CommandType.StoredProcedure;
                                    cmdCar.CommandText = "Movil_List_CarroceriaVehiculo";
                                    cmdCar.Parameters.Add("@categoriaId", SqlDbType.Int).Value = c.categoriaId;

                                    SqlDataReader drCar = cmdCar.ExecuteReader();
                                    if (drCar.HasRows)
                                    {
                                        List<Carroceria> carrocerias = new List<Carroceria>();

                                        while (drCar.Read())
                                        {
                                            carrocerias.Add(new Carroceria()
                                            {
                                                carroceriaId = drCar.GetInt32(0),
                                                categoriaId = drCar.GetInt32(1),
                                                nombre = drCar.GetString(2),
                                                estado = drCar.GetInt32(3)
                                            });
                                        }
                                        c.carrocerias = carrocerias;
                                    }

                                    categorias.Add(c);

                                }
                                t.categorias = categorias;
                            }


                            tipos.Add(t);
                        }
                        migracion.tipos = tipos;
                    }

                    SqlCommand cmdMa = con.CreateCommand();
                    cmdMa.CommandTimeout = 0;
                    cmdMa.CommandType = CommandType.StoredProcedure;
                    cmdMa.CommandText = "Movil_List_MarcaVehiculo";
                    cmdMa.Parameters.Add("@empresaId", SqlDbType.Int).Value = 1;

                    SqlDataReader drMa = cmdMa.ExecuteReader();
                    if (drMa.HasRows)
                    {
                        List<Marca> marcas = new List<Marca>();

                        while (drMa.Read())
                        {

                            Marca m = new Marca();
                            m.marcaId = drMa.GetInt32(0);
                            m.empresaId = drMa.GetInt32(1);
                            m.nombre = drMa.GetString(2);
                            m.imagen = drMa.GetString(3);
                            m.estado = drMa.GetInt32(4);

                            SqlCommand cmdMo = con.CreateCommand();
                            cmdMo.CommandTimeout = 0;
                            cmdMo.CommandType = CommandType.StoredProcedure;
                            cmdMo.CommandText = "Movil_List_ModeloVehiculo";
                            cmdMo.Parameters.Add("@marcaId", SqlDbType.Int).Value = m.marcaId;

                            SqlDataReader drMo = cmdMo.ExecuteReader();
                            if (drMo.HasRows)
                            {
                                List<Modelo> modelos = new List<Modelo>();

                                while (drMo.Read())
                                {
                                    modelos.Add(new Modelo()
                                    {
                                        modeloId = drMo.GetInt32(0),
                                        marcaId = drMo.GetInt32(1),
                                        nombre = drMo.GetString(2),
                                        estado = drMo.GetInt32(3)
                                    });
                                }
                                m.modelos = modelos;
                            }
                            marcas.Add(m);
                        }
                        migracion.marcas = marcas;
                    }

                    con.Close();
                }
                return migracion;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static Mensaje SaveRegistroLaboral(Vehiculo v)
        {
            try
            {
                Mensaje m = null;

                using (SqlConnection con = new SqlConnection(db))
                {
                    con.Open();

                    foreach (var r in v.registros)
                    {

                        if (r.tipoRegistro == 4)
                        {
                            SqlCommand cmd = con.CreateCommand();
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "Movil_InsertarEditar_Registro_Combustible";
                            cmd.Parameters.Add("@usuario", SqlDbType.Int).Value = r.usuarioId;
                            cmd.Parameters.Add("@Empresa", SqlDbType.Int).Value = r.empresaId;
                            cmd.Parameters.Add("@vehiculo", SqlDbType.Int).Value = r.vehiculoId;
                            cmd.Parameters.Add("@estado", SqlDbType.Int).Value = r.estado;
                            cmd.Parameters.Add("@id_Registro_Combustible", SqlDbType.Int).Value = r.registroId;
                            cmd.Parameters.Add("@kmActual_Registro_Combustible", SqlDbType.Decimal).Value = r.km;
                            cmd.Parameters.Add("@fechaemision_registro_combustible", SqlDbType.VarChar).Value = r.fecha;
                            cmd.Parameters.Add("@fechaemision_registro_combustible", SqlDbType.VarChar).Value = r.fechaAtencion;
                            cmd.Parameters.Add("@id_Combustible", SqlDbType.Int).Value = r.tipoCombustibleId;
                            cmd.Parameters.Add("@cantidad_Registro_Combustible", SqlDbType.Int).Value = r.cantidadGalones;
                            cmd.Parameters.Add("@full_Registro_Combustible", SqlDbType.Int).Value = r.full;
                            cmd.Parameters.Add("@comentarios_Registro_Combustible", SqlDbType.VarChar).Value = r.observaciones;
                            cmd.Parameters.Add("@foto_Sustento_Registro_Combustible", SqlDbType.VarChar).Value = r.fotoRegistro;

                            SqlDataReader dr = cmd.ExecuteReader();
                            if (dr.HasRows)
                            {
                                m = new Mensaje();
                                while (dr.Read())
                                {
                                    m.codigo = dr.GetInt32(0);
                                    m.mensaje = "Guardado";
                                }
                            }
                        }
                        else if (r.tipoRegistro == 5)
                        {
                            SqlCommand cmd = con.CreateCommand();
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "Movil_InsertarEditar_Registro_Combustible_Vaucher";
                            cmd.Parameters.Add("@idRegistro", SqlDbType.Int).Value = r.registroId;
                            cmd.Parameters.Add("@nro_voucher", SqlDbType.VarChar).Value = r.nroVoucher;
                            cmd.Parameters.Add("@fecha_Emision_Voucher", SqlDbType.VarChar).Value = r.fechaDocumento;
                            cmd.Parameters.Add("@km_Voucher", SqlDbType.Decimal).Value = r.km;
                            cmd.Parameters.Add("@galones_Voucher", SqlDbType.Decimal).Value = r.cantidadGalones;
                            cmd.Parameters.Add("@precio_Voucher", SqlDbType.Decimal).Value = r.precio;
                            cmd.Parameters.Add("@foto_Voucher", SqlDbType.VarChar).Value = r.fotoRegistro;
                            cmd.Parameters.Add("@usuario", SqlDbType.Int).Value = r.usuarioId;
                            cmd.Parameters.Add("@fecha_creacion_voucher", SqlDbType.VarChar).Value = r.fecha;

                            SqlDataReader dr = cmd.ExecuteReader();
                            if (dr.HasRows)
                            {
                                m = new Mensaje();
                                while (dr.Read())
                                {
                                    m.codigo = dr.GetInt32(0);
                                    m.mensaje = "Guardado";
                                }
                            }

                        }
                        else
                        {
                            SqlCommand cmd = con.CreateCommand();
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "Movil_InsertarEditar_Registro_Labores";
                            cmd.Parameters.Add("@usuario", SqlDbType.Int).Value = r.usuarioId;
                            cmd.Parameters.Add("@Empresa", SqlDbType.Int).Value = r.empresaId;
                            cmd.Parameters.Add("@id_Registro_Labores", SqlDbType.Int).Value = r.registroId;
                            cmd.Parameters.Add("@id_TipoRegistro_Labores", SqlDbType.Int).Value = r.tipoRegistro;
                            cmd.Parameters.Add("@id_Vehiculo", SqlDbType.Int).Value = r.vehiculoId;
                            cmd.Parameters.Add("@fecha_Registro_Labores", SqlDbType.VarChar).Value = r.fecha;
                            cmd.Parameters.Add("@km_Registro_Labores", SqlDbType.Decimal).Value = r.km;
                            cmd.Parameters.Add("@foto_Registro_Labores", SqlDbType.VarChar).Value = r.fotoRegistro;
                            cmd.Parameters.Add("@nroOrdenObra_Registro_Labores", SqlDbType.VarChar).Value = r.nroOrdenRegistro;
                            cmd.Parameters.Add("@observaciones_Registro_Labores", SqlDbType.VarChar).Value = r.observaciones;

                            SqlDataReader dr = cmd.ExecuteReader();
                            if (dr.HasRows)
                            {
                                m = new Mensaje();
                                while (dr.Read())
                                {
                                    m.codigo = dr.GetInt32(0);
                                    m.mensaje = "Guardado";
                                }
                            }

                        }
                    }
                    con.Close();
                }
                return m;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<BandejaRegistro> GetBandejaRegistros(Filtro f)
        {
            try
            {

                List<BandejaRegistro> bandeja = null;
                using (SqlConnection con = new SqlConnection(db))
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Movil_Bandeja_Registro_Combustible";
                    cmd.Parameters.Add("@Empresa", SqlDbType.Int).Value = f.empresaId;
                    cmd.Parameters.Add("@Usuario", SqlDbType.Int).Value = f.usuarioId;
                    cmd.Parameters.Add("@Estado", SqlDbType.Int).Value = f.estado;
                    cmd.Parameters.Add("@pageIndex", SqlDbType.Int).Value = f.pageIndex;
                    cmd.Parameters.Add("@pageSize", SqlDbType.Int).Value = f.pageSize;
                    cmd.Parameters.Add("@search", SqlDbType.VarChar).Value = f.search;
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        bandeja = new List<BandejaRegistro>();
                        while (dr.Read())
                        {
                            BandejaRegistro b = new BandejaRegistro();

                            b.registroCombustibleId = dr.GetInt32(0);
                            b.vehiculoId = dr.GetInt32(1);
                            b.empresaId = dr.GetInt32(2);
                            b.nroPlacaVehiculo = dr.GetString(3);
                            b.anioVehiculo = Convert.ToDecimal(dr.GetDecimal(4));
                            b.nombreMarca = dr.GetString(5);
                            b.imagenMarca = dr.GetString(6);
                            b.nombreModelo = dr.GetString(7);
                            b.fechaAtencion = dr.GetDateTime(8).ToString("dd/MM/yyyy HH:mm:ss");
                            b.cantidadRegistro = Convert.ToDecimal(dr.GetDecimal(9));
                            b.fullRegistro = dr.GetString(10);
                            b.nombreCombustible = dr.GetString(11);
                            b.estado = dr.GetInt32(12);
                            b.abreviaturaEstado = dr.GetString(13);
                            b.colorEstado = dr.GetString(14);
                            b.nroVoucher = dr.GetString(15);
                            b.fechaEmision = dr.GetDateTime(16).ToString("dd/MM/yyyy HH:mm:ss");
                            b.km = Convert.ToDecimal(dr.GetDecimal(17));
                            b.cantidadGalones = Convert.ToDecimal(dr.GetDecimal(18));
                            b.precioVoucher = Convert.ToDecimal(dr.GetDecimal(19));
                            b.fotoVoucher = dr.GetString(20);

                            bandeja.Add(b);
                        }
                    }
                    con.Close();
                }

                return bandeja;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
