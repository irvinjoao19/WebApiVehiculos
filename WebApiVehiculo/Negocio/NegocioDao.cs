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

        public static Usuario GetLogin(Filtro f)
        {
            try
            {
                Usuario u = null;
                using (SqlConnection con = new SqlConnection(db))
                {
                    con.Open();

                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Movil_Login";
                    cmd.Parameters.Add("@usuario", SqlDbType.VarChar).Value = f.login;

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            u = new Usuario();

                            if (f.pass == dr.GetString(1))
                            {
                                u.login = dr.GetString(0);
                                u.pass = dr.GetString(1);
                                u.usuarioId = dr.GetInt32(2);
                                u.nroDocumento = dr.GetString(3);
                                u.apellidos = dr.GetString(4);
                                u.nombre = dr.GetString(5);
                                u.email = dr.GetString(6);
                                u.perfilId = dr.GetInt32(7);
                                u.fotoUrl = dr.GetString(8);
                                u.estado = dr.GetInt32(9);

                                SqlCommand cmdE = con.CreateCommand();
                                cmdE.CommandTimeout = 0;
                                cmdE.CommandType = CommandType.StoredProcedure;
                                cmdE.CommandText = "Dsige_Login_Acceso";
                                cmdE.Parameters.Add("@usuario", SqlDbType.VarChar).Value = f.login;

                                SqlDataReader drE = cmdE.ExecuteReader();
                                if (drE.HasRows)
                                {
                                    List<Empresa> empresa = new List<Empresa>();

                                    while (drE.Read())
                                    {
                                        Empresa e = new Empresa();
                                        e.usuarioId = drE.GetInt32(0);
                                        e.empresaId = drE.GetInt32(1);
                                        e.ruc = drE.GetString(2);
                                        e.nombreEmpresa = drE.GetString(3);
                                        e.logoEmpresa = drE.GetString(4);

                                        empresa.Add(e);
                                    }

                                    u.empresas = empresa;
                                }
                            }
                            else
                            {
                                u.pass = "Error";
                            }
                        }
                    }
                    con.Close();
                }

                return u;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static Migracion GetMigracion(Filtro f)
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
                    cmd.Parameters.Add("@Empresa", SqlDbType.Int).Value = f.empresaId;
                    cmd.Parameters.Add("@Estado", SqlDbType.Int).Value = f.estado;
                    cmd.Parameters.Add("@Usuario", SqlDbType.Int).Value = f.usuarioId;

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        List<Vehiculo> vehiculos = new List<Vehiculo>();

                        while (dr.Read())
                        {
                            Vehiculo v = new Vehiculo();

                            v.vehiculoId = dr.GetInt32(0);
                            v.empresaId = dr.GetInt32(1);
                            v.nroPlaca = dr.GetString(2);
                            v.color = dr.GetString(3);
                            v.nroMotor = dr.GetString(4);
                            v.nroChasis = dr.GetString(5);
                            v.anioVehiculo = Convert.ToDecimal(dr.GetDecimal(6));
                            v.cilidraje = Convert.ToDecimal(dr.GetDecimal(7));
                            v.kmInicial = Convert.ToDecimal(dr.GetDecimal(8));
                            v.kmMant = Convert.ToDecimal(dr.GetDecimal(9));
                            v.dni = dr.GetString(10);
                            v.ruc = dr.GetString(11);
                            v.estado = dr.GetInt32(12);
                            v.tipo = dr.GetInt32(17);
                            v.marcaId = dr.GetInt32(18);
                            v.modeloId = dr.GetInt32(19);
                            v.categoriaId = dr.GetInt32(20);
                            v.carroceriaId = dr.GetInt32(21);
                            v.combustibleId = dr.GetInt32(22);
                            v.localId = dr.GetInt32(23);
                            v.nombreTipoVehiculo = dr.GetString(24);
                            v.nombreMarca = dr.GetString(25);
                            v.imagenMarca = dr.GetString(26);
                            v.nombreModelo = dr.GetString(27);
                            v.nombreCategoria = dr.GetString(28);
                            v.nombreCarroceria = dr.GetString(29);
                            v.nombreCombustible = dr.GetString(30);
                            v.estadoAuto = dr.GetString(31);
                            v.colorEstado = dr.GetString(32);
                            v.nombreConductor = dr.GetString(33);

                            SqlCommand cmdCabecera = con.CreateCommand();
                            cmdCabecera.CommandTimeout = 0;
                            cmdCabecera.CommandType = CommandType.StoredProcedure;
                            cmdCabecera.CommandText = "Movil_List_CheckList_Cab";
                            cmdCabecera.Parameters.Add("@usuario", SqlDbType.Int).Value = f.usuarioId;
                            cmdCabecera.Parameters.Add("@empresa", SqlDbType.Int).Value = f.empresaId;
                            cmdCabecera.Parameters.Add("@vehiculo", SqlDbType.Int).Value = v.vehiculoId;

                            SqlDataReader head = cmdCabecera.ExecuteReader();
                            if (head.HasRows)
                            {
                                List<CheckListCabecera> checkList = new List<CheckListCabecera>();

                                while (head.Read())
                                {

                                    CheckListCabecera c = new CheckListCabecera();

                                    c.checkListId = head.GetInt32(0);
                                    c.numeroCheckList = head.GetString(1);
                                    c.nroPlaca = head.GetString(2);
                                    c.fecha = head.GetDateTime(3).ToString("dd/MM/yyyy HH:mm:ss");
                                    c.turnoId = head.GetInt32(4);

                                    SqlCommand cmdDetalle = con.CreateCommand();
                                    cmdDetalle.CommandTimeout = 0;
                                    cmdDetalle.CommandType = CommandType.StoredProcedure;
                                    cmdDetalle.CommandText = "Movil_List_CheckList_Detalle";
                                    cmdDetalle.Parameters.Add("@checkListId", SqlDbType.Int).Value = c.checkListId;

                                    SqlDataReader body = cmdDetalle.ExecuteReader();
                                    if (body.HasRows)
                                    {
                                        List<CheckListDetalle> detalles = new List<CheckListDetalle>();

                                        while (body.Read())
                                        {

                                            CheckListDetalle d = new CheckListDetalle();

                                            d.checkListDetalleId = body.GetInt32(0);
                                            d.checkListId = body.GetInt32(1);
                                            d.numeroCheckList = body.GetString(2);
                                            d.fechaCheckList = body.GetDateTime(3).ToString("dd/MM/yyyy HH:mm:ss");
                                            d.nroPlacaCheckList = body.GetString(4);
                                            d.tipoVista = body.GetString(5);
                                            d.nombreLado = body.GetString(6);
                                            d.nombrePartes = body.GetString(7);
                                            d.otrosCheckList = body.GetString(8);
                                            d.observacionesCheckList = body.GetString(9);
                                            d.fotoCheckList = body.GetString(10);                                             

                                            detalles.Add(d);
                                        }

                                        c.detalles = detalles;
                                    }
                                    checkList.Add(c);
                                }

                                v.checkListCabeceras = checkList;
                            }

                            vehiculos.Add(v);
                        }
                        migracion.vehiculos = vehiculos;
                    }

                    SqlCommand cmdC = con.CreateCommand();
                    cmdC.CommandTimeout = 0;
                    cmdC.CommandType = CommandType.StoredProcedure;
                    cmdC.CommandText = "Movil_List_Combustible";
                    cmdC.Parameters.Add("@empresaId", SqlDbType.Int).Value = f.empresaId;

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
                    cmdT.Parameters.Add("@empresaId", SqlDbType.Int).Value = f.empresaId;

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
                    cmdMa.Parameters.Add("@empresaId", SqlDbType.Int).Value = f.empresaId;

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


                    SqlCommand cmdTur = con.CreateCommand();
                    cmdTur.CommandTimeout = 0;
                    cmdTur.CommandType = CommandType.StoredProcedure;
                    cmdTur.CommandText = "Movil_List_Turnos";
                    cmdTur.Parameters.Add("@empresaId", SqlDbType.Int).Value = f.empresaId;

                    SqlDataReader drTur = cmdTur.ExecuteReader();
                    if (drTur.HasRows)
                    {
                        List<Turno> turnos = new List<Turno>();
                        while (drTur.Read())
                        {
                            turnos.Add(new Turno()
                            {
                                turnoId = drTur.GetInt32(0),
                                empresaId = drTur.GetInt32(1),
                                nombreTurno = drTur.GetString(2),
                                estado = drTur.GetInt32(3)
                            });
                        }
                        migracion.turnos = turnos;
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
                            cmd.Parameters.Add("@fechaAtencion_Registro_Combustible", SqlDbType.VarChar).Value = r.fechaAtencion;
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
                                    m.codigoBase = r.id;
                                    m.codigoRetorno = dr.GetInt32(0);
                                    m.mensaje = "Guardado";
                                }
                            }
                        }
                        else if (r.tipoRegistro != 5 || r.tipoRegistro != 4)
                        {
                            SqlCommand cmd = con.CreateCommand();
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "Movil_InsertarEditar_Registro_Labores";
                            cmd.Parameters.Add("@usuario", SqlDbType.Int).Value = r.usuarioId;
                            cmd.Parameters.Add("@Empresa", SqlDbType.Int).Value = r.empresaId;
                            cmd.Parameters.Add("@Estado", SqlDbType.Int).Value = r.estado;
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
                                    m.codigoBase = r.id;
                                    m.codigoRetorno = dr.GetInt32(0);
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

        public static List<BandejaRegistro> GetBandejaRegistrosPlaca(Filtro f)
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
                    cmd.CommandText = "Movil_Bandeja_Registro_Combustible_Placa";
                    cmd.Parameters.Add("@Empresa", SqlDbType.Int).Value = f.empresaId;
                    cmd.Parameters.Add("@Usuario", SqlDbType.Int).Value = f.usuarioId;
                    cmd.Parameters.Add("@Estado", SqlDbType.Int).Value = f.estado;
                    cmd.Parameters.Add("@pageIndex", SqlDbType.Int).Value = f.pageIndex;
                    cmd.Parameters.Add("@pageSize", SqlDbType.Int).Value = f.pageSize;
                    cmd.Parameters.Add("@id_vehiculo", SqlDbType.Int).Value = f.vehiculoId;
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

        public static Mensaje UpdateEstado(Filtro f)
        {
            try
            {
                Mensaje m = null;

                using (SqlConnection con = new SqlConnection(db))
                {
                    con.Open();

                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Movil_Registro_Combustible_ActualizarEstado";
                    cmd.Parameters.Add("@usuario", SqlDbType.Int).Value = f.usuarioId;
                    cmd.Parameters.Add("@Empresa", SqlDbType.Int).Value = f.empresaId;
                    cmd.Parameters.Add("@vehiculo", SqlDbType.Int).Value = f.vehiculoId;
                    cmd.Parameters.Add("@estado", SqlDbType.Int).Value = f.estado;
                    cmd.Parameters.Add("@id_Registro_Combustible", SqlDbType.Int).Value = f.registroId;

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        m = new Mensaje();
                        while (dr.Read())
                        {
                            m.codigoRetorno = dr.GetInt32(0);
                            m.mensaje = "Enviado";
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

        public static Mensaje RegistroCombustible(RegistroLaboral r)
        {
            try
            {
                Mensaje m = null;

                using (SqlConnection con = new SqlConnection(db))
                {
                    con.Open();

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
                    cmd.Parameters.Add("@empresa", SqlDbType.Int).Value = r.empresaId;
                    cmd.Parameters.Add("@fecha_creacion_voucher", SqlDbType.VarChar).Value = r.fecha;

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        m = new Mensaje();
                        while (dr.Read())
                        {
                            m.codigoBase = r.id;
                            m.codigoRetorno = dr.GetInt32(0);
                            m.mensaje = "Guardado";
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

        public static List<Historial> GetHistorial(Filtro f)
        {
            try
            {
                List<Historial> historial = null;
                using (SqlConnection con = new SqlConnection(db))
                {
                    con.Open();

                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Movil_Registro_Combustible_HistoricoXplaca";
                    cmd.Parameters.Add("@Empresa", SqlDbType.Int).Value = f.empresaId;
                    cmd.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = f.usuarioId;
                    cmd.Parameters.Add("@Estado", SqlDbType.VarChar).Value = f.estado;
                    cmd.Parameters.Add("@id_vehiculo", SqlDbType.Decimal).Value = f.vehiculoId;


                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        historial = new List<Historial>();
                        while (dr.Read())
                        {
                            Historial h = new Historial();
                            h.historialId = dr.GetInt32(0);
                            h.fecha = dr.GetString(1);
                            h.km = Convert.ToDecimal(dr.GetDecimal(2));
                            h.combustible = dr.GetString(3);
                            h.cantidad = Convert.ToDecimal(dr.GetDecimal(4));
                            h.precio = Convert.ToDecimal(dr.GetDecimal(5));
                            h.total = Convert.ToDecimal(dr.GetDecimal(6));
                            historial.Add(h);
                        }
                    }

                    con.Close();
                }

                return historial;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public static List<InicioFinLabores> GetInicioFinLabores(Filtro f)
        {
            try
            {

                List<InicioFinLabores> inicioFin = null;
                using (SqlConnection con = new SqlConnection(db))
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Movil_Listar_InicioTemino_LaboresXFiltros";
                    cmd.Parameters.Add("@Empresa", SqlDbType.Int).Value = f.empresaId;
                    cmd.Parameters.Add("@Usuario", SqlDbType.Int).Value = f.usuarioId;
                    cmd.Parameters.Add("@Fecha", SqlDbType.VarChar).Value = f.fecha;
                    cmd.Parameters.Add("@Filtro", SqlDbType.VarChar).Value = f.filtro;
                    cmd.Parameters.Add("@TipoData", SqlDbType.VarChar).Value = "Cabecera";

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        inicioFin = new List<InicioFinLabores>();
                        while (dr.Read())
                        {
                            InicioFinLabores i = new InicioFinLabores();

                            i.vehiculoId = dr.GetInt32(0);
                            i.nroPlaca = dr.GetString(1);
                            i.anioVehiculo = Convert.ToDecimal(dr.GetDecimal(2));
                            i.nombreMarca = dr.GetString(3);
                            i.imagenMarca = dr.GetString(4);
                            i.nombreModelo = dr.GetString(5);
                            i.totalKm = Convert.ToDecimal(dr.GetDecimal(6));
                            i.rendimiento = Convert.ToDecimal(dr.GetDecimal(7));

                            SqlCommand cmdD = con.CreateCommand();
                            cmdD.CommandTimeout = 0;
                            cmdD.CommandType = CommandType.StoredProcedure;
                            cmdD.CommandText = "Movil_Listar_InicioTemino_LaboresXFiltros";
                            cmdD.Parameters.Add("@Empresa", SqlDbType.Int).Value = f.empresaId;
                            cmdD.Parameters.Add("@Usuario", SqlDbType.Int).Value = f.usuarioId;
                            cmdD.Parameters.Add("@Fecha", SqlDbType.VarChar).Value = f.fecha;
                            cmdD.Parameters.Add("@Filtro", SqlDbType.VarChar).Value = f.filtro;
                            cmdD.Parameters.Add("@TipoData", SqlDbType.VarChar).Value = "Detalle";
                            SqlDataReader drD = cmdD.ExecuteReader();

                            if (drD.HasRows)
                            {
                                List<InicioLaboresDetalle> detalle = new List<InicioLaboresDetalle>();
                                while (drD.Read())
                                {
                                    InicioLaboresDetalle d = new InicioLaboresDetalle();
                                    d.vehiculoId = drD.GetInt32(0);
                                    d.registroId = drD.GetInt32(1);
                                    d.nombre = drD.GetString(2);
                                    d.fecha = drD.GetDateTime(3).ToString("dd/MM/yyyy");
                                    d.km = Convert.ToDecimal(drD.GetDecimal(4));
                                    detalle.Add(d);
                                }
                                i.detalles = detalle;
                            }

                            inicioFin.Add(i);
                        }
                    }
                    con.Close();
                }

                return inicioFin;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

    }
}
