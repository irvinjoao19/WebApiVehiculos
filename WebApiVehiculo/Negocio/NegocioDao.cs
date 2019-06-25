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

                            SqlCommand cmdRL = con.CreateCommand();
                            cmdRL.CommandTimeout = 0;
                            cmdRL.CommandType = CommandType.StoredProcedure;
                            cmdRL.CommandText = "Movil_List_Registros";
                            cmdRL.Parameters.Add("@usuario", SqlDbType.Int).Value = f.usuarioId;
                            cmdRL.Parameters.Add("@empresa", SqlDbType.Int).Value = f.empresaId;
                            cmdRL.Parameters.Add("@vehiculoId", SqlDbType.Int).Value = v.vehiculoId;

                            SqlDataReader drl = cmdRL.ExecuteReader();
                            if (drl.HasRows)
                            {
                                List<RegistroLaboral> registros = new List<RegistroLaboral>();

                                while (drl.Read())
                                {
                                    RegistroLaboral r = new RegistroLaboral();
                                    r.id = drl.GetInt32(0);
                                    r.registroId = drl.GetInt32(0);
                                    r.empresaId = drl.GetInt32(1);
                                    r.vehiculoId = drl.GetInt32(2);
                                    r.estado = drl.GetInt32(3);
                                    r.usuarioId = drl.GetInt32(4);
                                    r.tipoRegistro = drl.GetInt32(5);
                                    r.km = Convert.ToDecimal(drl.GetDecimal(6));
                                    r.fecha = drl.GetDateTime(7).ToString("dd/MM/yyyy HH:mm");
                                    r.fechaAtencion = drl.GetDateTime(8).ToString("dd/MM/yyyy HH:mm");
                                    r.tipoCombustibleId = drl.GetInt32(9);
                                    r.cantidadGalones = Convert.ToDecimal(drl.GetDecimal(10));
                                    r.full = drl.GetInt32(11);
                                    r.observaciones = drl.GetString(12);
                                    r.fotoRegistro = drl.GetString(13);
                                    r.nroOrdenRegistro = drl.GetString(14);
                                    r.nombreTipoRegistro = drl.GetString(15);
                                    r.nombreCombustible = drl.GetString(16);
                                    r.nroVoucher = drl.GetString(17);
                                    r.fechaDocumento = drl.GetString(18);
                                    registros.Add(r);
                                }

                                v.registros = registros;
                            }

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
                                    c.checkListCabeceraId = head.GetInt32(0);
                                    c.checkListId = head.GetInt32(0);
                                    c.vehiculoId = head.GetInt32(1);
                                    c.numeroCheckList = head.GetString(2);
                                    c.nroPlaca = head.GetString(3);
                                    c.fecha = head.GetDateTime(4).ToString("dd/MM/yyyy HH:mm:ss");
                                    c.turnoId = head.GetInt32(5);
                                    c.nombreTurno = head.GetString(6);
                                    c.empresaId = head.GetInt32(7);
                                    c.usuarioId = head.GetInt32(8);
                                    c.estado = head.GetInt32(9);

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

                                            d.detalleId = body.GetInt32(0);
                                            d.checkListDetalleId = body.GetInt32(0);
                                            d.checkListCabeceraId = body.GetInt32(1);
                                            d.checkListId = body.GetInt32(1);
                                            d.numeroCheckList = body.GetString(2);
                                            //d.fechaCheckList = body.GetDateTime(3).ToString("dd/MM/yyyy HH:mm:ss");
                                            d.fechaCheckList = body.GetDateTime(3).ToString("dd/MM/yyyy");
                                            d.nroPlacaCheckList = body.GetString(4);
                                            d.tipoVista = body.GetString(5);
                                            d.nombreLado = body.GetString(6);
                                            d.nombrePartes = body.GetString(7);
                                            d.otrosCheckList = body.GetString(8);
                                            d.observacionesCheckList = body.GetString(9);
                                            d.fotoCheckList = body.GetString(10);
                                            d.estadoIdCheckList = body.GetInt32(11);
                                            d.estadoCheckList = body.GetString(12);
                                            d.ladoVehiculoId = body.GetInt32(13);
                                            d.ladoParteVehiculoId = body.GetInt32(14);
                                            d.empresaId = c.empresaId;
                                            detalles.Add(d);
                                        }

                                        c.detalles = detalles;
                                    }
                                    checkList.Add(c);
                                }

                                v.checkListCabeceras = checkList;
                            }

                            SqlCommand cmdMant = con.CreateCommand();
                            cmdMant.CommandTimeout = 0;
                            cmdMant.CommandType = CommandType.StoredProcedure;
                            cmdMant.CommandText = "Movil_List_MantenimientoCab";
                            cmdMant.Parameters.Add("@vehiculoId", SqlDbType.Int).Value = v.vehiculoId;
                            cmdMant.Parameters.Add("@usuarioId", SqlDbType.Int).Value = f.usuarioId;
                            cmdMant.Parameters.Add("@empresaId", SqlDbType.Int).Value = f.empresaId;


                            SqlDataReader mant = cmdMant.ExecuteReader();
                            if (mant.HasRows)
                            {
                                List<MantenimientoGeneral> mantGeneral = new List<MantenimientoGeneral>();

                                while (mant.Read())
                                {
                                    MantenimientoGeneral m = new MantenimientoGeneral();
                                    m.generalId = mant.GetInt32(0);
                                    m.mantenimientoId = mant.GetInt32(0);
                                    m.empresaId = mant.GetInt32(1);
                                    m.tipoIE = mant.GetInt32(2);
                                    m.tipoMantenimientoId = mant.GetInt32(3);
                                    m.vehiculoId = mant.GetInt32(4);
                                    m.numeroMantenimiento = mant.GetString(5);
                                    m.fechaMantenimiento = mant.GetDateTime(6).ToString("dd/MM/yyyy");
                                    m.solicitadoPor = mant.GetString(7);
                                    m.trabajoMantenimiento = mant.GetString(8);
                                    m.ruc = mant.GetString(9);
                                    m.razonSocial = mant.GetString(10);
                                    m.fechaInicial = mant.GetDateTime(11).ToString("dd/MM/yyyy");
                                    m.fechaFinal = mant.GetDateTime(12).ToString("dd/MM/yyyy");
                                    m.km = Convert.ToDecimal(mant.GetDecimal(13));
                                    m.totalSoles = Convert.ToDecimal(mant.GetDecimal(14));
                                    m.totalDolares = Convert.ToDecimal(mant.GetDecimal(15));
                                    m.estado = mant.GetInt32(16);
                                    m.usuario = mant.GetInt32(17);

                                    SqlCommand cmdMantDet = con.CreateCommand();
                                    cmdMantDet.CommandTimeout = 0;
                                    cmdMantDet.CommandType = CommandType.StoredProcedure;
                                    cmdMantDet.CommandText = "Movil_List_MantenimientoDet";
                                    cmdMantDet.Parameters.Add("@mantenimientoId", SqlDbType.Int).Value = m.generalId;

                                    SqlDataReader dDet = cmdMantDet.ExecuteReader();
                                    if (dDet.HasRows)
                                    {
                                        List<MantenimientoDetalle> detalles = new List<MantenimientoDetalle>();

                                        while (dDet.Read())
                                        {
                                            MantenimientoDetalle d = new MantenimientoDetalle();

                                            d.detalleId = dDet.GetInt32(0);
                                            d.mantenimientoDetalleId = dDet.GetInt32(0);
                                            d.generalId = dDet.GetInt32(1);
                                            d.tipoDocumentoId = dDet.GetInt32(2);
                                            d.nombreDocumento = dDet.GetString(3);
                                            d.numeroDocumento = dDet.GetString(4);
                                            d.fechaMantenimientoDetalle = dDet.GetString(5);
                                            d.tipoMonedaId = dDet.GetInt32(6);
                                            d.nombreMoneda = dDet.GetString(7);
                                            d.tipoCambio = Convert.ToDecimal(dDet.GetDecimal(8));
                                            d.descripcionRespuesta = dDet.GetString(9);
                                            d.cantidad = Convert.ToDecimal(dDet.GetDecimal(10));
                                            d.precio = Convert.ToDecimal(dDet.GetDecimal(11));
                                            d.total = Convert.ToDecimal(dDet.GetDecimal(12));
                                            d.descripcionTrabajo = dDet.GetString(13);
                                            d.estado = dDet.GetInt32(14);
                                            d.usuario = dDet.GetInt32(15);
                                            detalles.Add(d);
                                        }
                                        m.detalles = detalles;
                                    }
                                    mantGeneral.Add(m);
                                }
                                v.mantenimientos = mantGeneral;
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

                    SqlCommand cmdEstado = con.CreateCommand();
                    cmdEstado.CommandTimeout = 0;
                    cmdEstado.CommandType = CommandType.StoredProcedure;
                    cmdEstado.CommandText = "Movil_List_EstadoCheckList";
                    cmdEstado.Parameters.Add("@empresaId", SqlDbType.Int).Value = f.empresaId;

                    SqlDataReader drEstado = cmdEstado.ExecuteReader();
                    if (drEstado.HasRows)
                    {
                        List<CheckListEstado> estados = new List<CheckListEstado>();
                        while (drEstado.Read())
                        {
                            estados.Add(new CheckListEstado()
                            {
                                estadoCheckListId = drEstado.GetInt32(0),
                                empresaId = drEstado.GetInt32(1),
                                nombreCheckList = drEstado.GetString(2),
                                solicitudComentario = drEstado.GetInt32(3),
                                estado = drEstado.GetInt32(4)
                            });
                        }
                        migracion.estados = estados;
                    }

                    SqlCommand cmdLado = con.CreateCommand();
                    cmdLado.CommandTimeout = 0;
                    cmdLado.CommandType = CommandType.StoredProcedure;
                    cmdLado.CommandText = "Movil_List_LadoVehiculo";
                    cmdLado.Parameters.Add("@empresaId", SqlDbType.Int).Value = f.empresaId;

                    SqlDataReader drLado = cmdLado.ExecuteReader();
                    if (drLado.HasRows)
                    {
                        List<LadoVehiculo> lados = new List<LadoVehiculo>();
                        while (drLado.Read())
                        {

                            LadoVehiculo l = new LadoVehiculo();
                            l.ladoVehiculoId = drLado.GetInt32(0);
                            l.empresaId = drLado.GetInt32(1);
                            l.tipoVista = drLado.GetString(2);
                            l.nombreLado = drLado.GetString(3);
                            l.estado = drLado.GetInt32(4);

                            SqlCommand cmdParte = con.CreateCommand();
                            cmdParte.CommandTimeout = 0;
                            cmdParte.CommandType = CommandType.StoredProcedure;
                            cmdParte.CommandText = "Movil_List_ParteVehiculo";
                            cmdParte.Parameters.Add("@ladoVehiculoId", SqlDbType.Int).Value = l.ladoVehiculoId;

                            SqlDataReader drParte = cmdParte.ExecuteReader();
                            if (drParte.HasRows)
                            {
                                List<LadoParteVehiculo> partes = new List<LadoParteVehiculo>();
                                while (drParte.Read())
                                {
                                    partes.Add(new LadoParteVehiculo()
                                    {
                                        ladoparteVehiculoId = drParte.GetInt32(0),
                                        ladoVehiculoId = drParte.GetInt32(1),
                                        nombreLado = drParte.GetString(2),
                                        partesVehiculoId = drParte.GetInt32(3),
                                        nombrePartesVehiculo = drParte.GetString(4)
                                    });
                                }

                                l.parteVehiculos = partes;
                            }

                            lados.Add(l);
                        }
                        migracion.ladoVehiculos = lados;
                    }

                    SqlCommand cmdProveedor = con.CreateCommand();
                    cmdProveedor.CommandTimeout = 0;
                    cmdProveedor.CommandType = CommandType.StoredProcedure;
                    cmdProveedor.CommandText = "Movil_List_Proveedor";
                    cmdProveedor.Parameters.Add("@empresaId", SqlDbType.Int).Value = f.empresaId;
                    SqlDataReader drProveedor = cmdProveedor.ExecuteReader();
                    if (drProveedor.HasRows)
                    {
                        List<Proveedor> proveedores = new List<Proveedor>();

                        while (drProveedor.Read())
                        {
                            proveedores.Add(new Proveedor()
                            {
                                proveedorId = drProveedor.GetInt32(0),
                                empresaId = drProveedor.GetInt32(1),
                                ruc = drProveedor.GetString(2),
                                razonSocial = drProveedor.GetString(3)
                            });
                        }
                        migracion.proveedores = proveedores;
                    }

                    SqlCommand cmdDocumentos = con.CreateCommand();
                    cmdDocumentos.CommandTimeout = 0;
                    cmdDocumentos.CommandType = CommandType.StoredProcedure;
                    cmdDocumentos.CommandText = "Movil_List_TipoDocumento";
                    SqlDataReader drDoc = cmdDocumentos.ExecuteReader();
                    if (drDoc.HasRows)
                    {
                        List<TipoDocumento> documentos = new List<TipoDocumento>();

                        while (drDoc.Read())
                        {
                            documentos.Add(new TipoDocumento()
                            {
                                tipoDocumentoId = drDoc.GetInt32(0),
                                abreviatura = drDoc.GetString(1),
                                nombre = drDoc.GetString(2)
                            });
                        }
                        migracion.documentos = documentos;
                    }

                    SqlCommand cmdMoneda = con.CreateCommand();
                    cmdMoneda.CommandTimeout = 0;
                    cmdMoneda.CommandType = CommandType.StoredProcedure;
                    cmdMoneda.CommandText = "Movil_List_Monedas";
                    SqlDataReader drMon = cmdMoneda.ExecuteReader();
                    if (drMon.HasRows)
                    {
                        List<Moneda> monedas = new List<Moneda>();

                        while (drMon.Read())
                        {
                            monedas.Add(new Moneda()
                            {

                                monedaId = drMon.GetInt32(0),
                                nombre = drMon.GetString(1),
                                simbolo = drMon.GetString(2)

                            });
                        }
                        migracion.monedas = monedas;
                    }

                    SqlCommand cmdConductor = con.CreateCommand();
                    cmdConductor.CommandTimeout = 0;
                    cmdConductor.CommandType = CommandType.StoredProcedure;
                    cmdConductor.CommandText = "Movil_List_Personal";
                    cmdConductor.Parameters.Add("@empresaId", SqlDbType.Int).Value = f.empresaId;
                    SqlDataReader drCon = cmdConductor.ExecuteReader();
                    if (drCon.HasRows)
                    {
                        List<Personal> personals = new List<Personal>();

                        while (drCon.Read())
                        {
                            personals.Add(new Personal()
                            {

                                personalId = drCon.GetInt32(0),
                                empresaId = drCon.GetInt32(1),
                                nroDocumento = drCon.GetString(2),
                                tipoDoc = drCon.GetInt32(3),
                                apellidos = drCon.GetString(4),
                                nombres = drCon.GetString(5),
                                cargoId = drCon.GetInt32(6)

                            });
                        }
                        migracion.conductores = personals;
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

        public static List<Mensaje> SaveRegistroLaboral(Vehiculo v)
        {
            try
            {
                List<Mensaje> m = null;

                using (SqlConnection con = new SqlConnection(db))
                {
                    con.Open();

                    m = new List<Mensaje>();

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
                                while (dr.Read())
                                {
                                    m.Add(new Mensaje()
                                    {
                                        codigoBase = r.id,
                                        codigoRetorno = dr.GetInt32(0),
                                        mensaje = "Guardado"
                                    });
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
                                while (dr.Read())
                                {
                                    m.Add(new Mensaje()
                                    {
                                        codigoBase = r.id,
                                        codigoRetorno = dr.GetInt32(0),
                                        mensaje = "Guardado"
                                    });
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

        public static Mensaje RegistroCheckList(CheckListCabecera c)
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
                    cmd.CommandText = "Movil_InsertarEditar_CheckList_Cab";
                    cmd.Parameters.Add("@id_CheckList", SqlDbType.Int).Value = c.checkListId;
                    cmd.Parameters.Add("@numero_checkList", SqlDbType.VarChar).Value = c.numeroCheckList;
                    cmd.Parameters.Add("@fecha_CheckList", SqlDbType.VarChar).Value = c.fecha;
                    cmd.Parameters.Add("@turno", SqlDbType.Int).Value = c.turnoId;
                    cmd.Parameters.Add("@empresa", SqlDbType.Int).Value = c.empresaId;
                    cmd.Parameters.Add("@vehiculo", SqlDbType.Int).Value = c.vehiculoId;
                    cmd.Parameters.Add("@usuario", SqlDbType.Int).Value = c.usuarioId;
                    cmd.Parameters.Add("@estado", SqlDbType.Int).Value = 1;

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        m = new Mensaje();
                        while (dr.Read())
                        {
                            m.codigoBase = c.checkListCabeceraId;
                            m.codigoRetorno = dr.GetInt32(0);
                            m.mensaje = "Guardado";

                            List<MensajeDetalle> detalle = new List<MensajeDetalle>();

                            foreach (var d in c.detalles)
                            {

                                SqlCommand cmdD = con.CreateCommand();
                                cmdD.CommandTimeout = 0;
                                cmdD.CommandType = CommandType.StoredProcedure;
                                cmdD.CommandText = "Movil_InsertarEditar_CheckList_Det";
                                cmdD.Parameters.Add("@id_CheckList", SqlDbType.Int).Value = m.codigoRetorno;
                                cmdD.Parameters.Add("@id_checklist_det", SqlDbType.Int).Value = d.checkListDetalleId;
                                cmdD.Parameters.Add("@lado_Vehiculo", SqlDbType.Int).Value = d.ladoVehiculoId;
                                cmdD.Parameters.Add("@partesLado_Vehiculo", SqlDbType.Int).Value = d.ladoParteVehiculoId;
                                cmdD.Parameters.Add("@estado", SqlDbType.Int).Value = d.estadoIdCheckList;
                                cmdD.Parameters.Add("@otros_checklist", SqlDbType.VarChar).Value = d.otrosCheckList;
                                cmdD.Parameters.Add("@observacion_checklist_det", SqlDbType.VarChar).Value = d.observacionesCheckList;
                                cmdD.Parameters.Add("@foto_checklist_det", SqlDbType.VarChar).Value = d.fotoCheckList;
                                cmdD.Parameters.Add("@usuario", SqlDbType.Int).Value = c.usuarioId;

                                SqlDataReader drD = cmdD.ExecuteReader();
                                if (drD.HasRows)
                                {
                                    while (drD.Read())
                                    {
                                        detalle.Add(new MensajeDetalle()
                                        {
                                            detalleId = d.detalleId,
                                            detalleRetornoId = drD.GetInt32(0)
                                        });
                                    }
                                }
                            }
                            m.detalle = detalle;
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

        public static Mensaje SaveMantenimiento(MantenimientoGeneral m)
        {
            try
            {
                Mensaje mensaje = null;

                using (SqlConnection con = new SqlConnection(db))
                {
                    con.Open();

                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Movil_InsertarEditar_Mantenimiento_Cab";
                    cmd.Parameters.Add("@id_Mantenimiento", SqlDbType.Int).Value = m.mantenimientoId;
                    cmd.Parameters.Add("@id_empresa", SqlDbType.Int).Value = m.empresaId;
                    cmd.Parameters.Add("@id_tipoie", SqlDbType.Int).Value = m.tipoIE;
                    cmd.Parameters.Add("@id_tipomantenimiento", SqlDbType.Int).Value = m.tipoMantenimientoId;
                    cmd.Parameters.Add("@id_vehiculo", SqlDbType.Int).Value = m.vehiculoId;
                    cmd.Parameters.Add("@numero_mant", SqlDbType.VarChar).Value = m.numeroMantenimiento;
                    cmd.Parameters.Add("@fecha_mant", SqlDbType.VarChar).Value = m.fechaMantenimiento;
                    cmd.Parameters.Add("@solicitadopor_mant", SqlDbType.VarChar).Value = m.solicitadoPor;
                    cmd.Parameters.Add("@trabajo_mant", SqlDbType.VarChar).Value = m.trabajoMantenimiento;
                    cmd.Parameters.Add("@ruc_proveedor_mant", SqlDbType.VarChar).Value = m.ruc;
                    cmd.Parameters.Add("@fechainicial_mant", SqlDbType.VarChar).Value = m.fechaInicial;
                    cmd.Parameters.Add("@fechafinal_mant", SqlDbType.VarChar).Value = m.fechaFinal;
                    cmd.Parameters.Add("@kmmant", SqlDbType.Decimal).Value = m.km;
                    cmd.Parameters.Add("@totalsoles_mant", SqlDbType.Decimal).Value = m.totalSoles;
                    cmd.Parameters.Add("@totaldolares_mant", SqlDbType.Decimal).Value = m.totalDolares;
                    cmd.Parameters.Add("@estado", SqlDbType.Int).Value = m.estado;
                    cmd.Parameters.Add("@usuario", SqlDbType.Int).Value = m.usuario;

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        mensaje = new Mensaje();
                        while (dr.Read())
                        {
                            mensaje.codigoRetorno = dr.GetInt32(0);
                            mensaje.mensaje = "Enviado";

                            List<MensajeDetalle> detalle = new List<MensajeDetalle>();

                            foreach (var d in m.detalles)
                            {

                                SqlCommand cmdD = con.CreateCommand();
                                cmdD.CommandTimeout = 0;
                                cmdD.CommandType = CommandType.StoredProcedure;
                                cmdD.CommandText = "Movil_InsertarEditar_Mantenimiento_Det";
                                cmdD.Parameters.Add("@id_Mantenimiento_Det", SqlDbType.Int).Value = d.mantenimientoDetalleId;
                                cmdD.Parameters.Add("@id_Mantenimiento", SqlDbType.Int).Value = mensaje.codigoRetorno;
                                cmdD.Parameters.Add("@id_tipodoc", SqlDbType.Int).Value = d.tipoDocumentoId;
                                cmdD.Parameters.Add("@numerodoc_mant_det", SqlDbType.VarChar).Value = d.numeroDocumento;
                                cmdD.Parameters.Add("@fecha_mant_det", SqlDbType.VarChar).Value = d.fechaMantenimientoDetalle;
                                cmdD.Parameters.Add("@id_tipomoneda", SqlDbType.Int).Value = d.tipoMonedaId;
                                cmdD.Parameters.Add("@tipocambio", SqlDbType.Decimal).Value = d.tipoCambio;
                                cmdD.Parameters.Add("@descripcion_mant_det", SqlDbType.VarChar).Value = d.descripcionRespuesta;
                                cmdD.Parameters.Add("@cantidad_mant_det", SqlDbType.Decimal).Value = d.cantidad;
                                cmdD.Parameters.Add("@precio_mant_det", SqlDbType.Decimal).Value = d.precio;
                                cmdD.Parameters.Add("@total_mant_det", SqlDbType.Decimal).Value = d.total;
                                cmdD.Parameters.Add("@descripcion_trabajo_mant_det", SqlDbType.VarChar).Value = d.descripcionTrabajo;
                                cmdD.Parameters.Add("@estado", SqlDbType.Int).Value = d.estado;
                                cmdD.Parameters.Add("@usuario", SqlDbType.Int).Value = d.usuario;

                                SqlDataReader drD = cmdD.ExecuteReader();
                                if (drD.HasRows)
                                {
                                    while (drD.Read())
                                    {
                                        detalle.Add(new MensajeDetalle()
                                        {
                                            detalleId = d.detalleId,
                                            detalleRetornoId = drD.GetInt32(0)
                                        });
                                    }
                                }
                            }
                            mensaje.detalle = detalle;
                        }
                    }

                    con.Close();
                }

                return mensaje;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static Mensaje SaveInspeccion(Inspeccion i)
        {
            try
            {
                Mensaje mensaje = null;

                using (SqlConnection con = new SqlConnection(db))
                {
                    con.Open();

                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Movil_InsertarEditar_Inspeccion_Cab";
                    cmd.Parameters.Add("@id_inspeccion", SqlDbType.Int).Value = i.inspeccionCabeceraId;
                    cmd.Parameters.Add("@id_empresa", SqlDbType.Int).Value = i.empresaId;
                    cmd.Parameters.Add("@id_vehiculo", SqlDbType.Int).Value = i.vehiculoId;
                    cmd.Parameters.Add("@usuario", SqlDbType.Int).Value = i.usuarioId;
                    cmd.Parameters.Add("@nro_inspeccion", SqlDbType.VarChar).Value = i.nroInspeccion;
                    cmd.Parameters.Add("@id_empresa_inspeccionar", SqlDbType.Int).Value = i.empresaInspeccionId;
                    cmd.Parameters.Add("@fecha_inspeccion", SqlDbType.VarChar).Value = i.fechaInspeccion;
                    cmd.Parameters.Add("@sede_inspeccion", SqlDbType.VarChar).Value = i.sedeInspeccion;
                    cmd.Parameters.Add("@fecha_rec_ope", SqlDbType.VarChar).Value = i.fechaRecibidoOpe;
                    cmd.Parameters.Add("@km_rec_ope", SqlDbType.Decimal).Value = i.kmRecibidoOpe;
                    cmd.Parameters.Add("@id_conductor_rec_ope", SqlDbType.Int).Value = i.conductorRecibidoOpe;
                    cmd.Parameters.Add("@id_conbustible_rec_ope", SqlDbType.Int).Value = i.combustibleRecibidoOpe;
                    cmd.Parameters.Add("@fecha_env_provee", SqlDbType.VarChar).Value = i.fechaEnvioProvee;
                    cmd.Parameters.Add("@km_env_provee decimal", SqlDbType.Decimal).Value = i.kmEnvioProvee;
                    cmd.Parameters.Add("@id_conductor_env_provee", SqlDbType.Int).Value = i.conductorEnvioProvee;
                    cmd.Parameters.Add("@id_combustible_env_provee", SqlDbType.Int).Value = i.combustibleEnvioProvee;
                    cmd.Parameters.Add("@fecha_ret_trans", SqlDbType.VarChar).Value = i.fechaRetiroTrans;
                    cmd.Parameters.Add("@km_ret_trans", SqlDbType.Decimal).Value = i.kmRetiroTrans;
                    cmd.Parameters.Add("@id_conductor_ret_trans", SqlDbType.Int).Value = i.conductorRetiroTrans;
                    cmd.Parameters.Add("@id_conbustible_ret_trans", SqlDbType.Int).Value = i.combustibleRetiroTrans;
                    cmd.Parameters.Add("@fecha_ent_ope", SqlDbType.VarChar).Value = i.fechaEntregaOpe;
                    cmd.Parameters.Add("@km_ent_ope decimal", SqlDbType.Decimal).Value = i.kmEntregaOpe;
                    cmd.Parameters.Add("@id_conductor_ent_ope", SqlDbType.Int).Value = i.conductorEntregaOpe;
                    cmd.Parameters.Add("@id_conbustible_ent_ope", SqlDbType.Int).Value = i.combustibleEntregaOpe;

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        mensaje = new Mensaje();
                        while (dr.Read())
                        {
                            mensaje.codigoRetorno = dr.GetInt32(0);
                            mensaje.mensaje = "Enviado";

                            List<MensajeDetalle> detalle = new List<MensajeDetalle>();

                            foreach (var d in i.detalles)
                            {
                                SqlCommand cmdD = con.CreateCommand();
                                cmdD.CommandTimeout = 0;
                                cmdD.CommandType = CommandType.StoredProcedure;
                                cmdD.CommandText = "Movil_InsertarEditar_Inspeccion_Detalle";
                                cmdD.Parameters.Add("@id_inspeccion_det", SqlDbType.Int).Value = d.inspeccionDetalleId;
                                cmdD.Parameters.Add("@id_inspeccion", SqlDbType.Int).Value = mensaje.codigoRetorno;
                                cmdD.Parameters.Add("@id_vehiculo", SqlDbType.Int).Value = d.vehiculoId;
                                cmdD.Parameters.Add("@id_caracteristica", SqlDbType.Int).Value = d.caracteristicasId;
                                cmdD.Parameters.Add("@id_estado_inspeccion", SqlDbType.Int).Value = d.estadoInspeccionId;
                                cmdD.Parameters.Add("@fecha_inspeccion_det", SqlDbType.VarChar).Value = d.fechaInspeccionDetalle;
                                cmdD.Parameters.Add("@obs_inspeccion_det", SqlDbType.VarChar).Value = d.observacion;
                                cmdD.Parameters.Add("@estado", SqlDbType.Int).Value = d.estado;
                                cmdD.Parameters.Add("@usuario", SqlDbType.Int).Value = d.usuario;

                                SqlDataReader drD = cmdD.ExecuteReader();
                                if (drD.HasRows)
                                {
                                    while (drD.Read())
                                    {
                                        detalle.Add(new MensajeDetalle()
                                        {
                                            detalleId = d.detalleId,
                                            detalleRetornoId = drD.GetInt32(0),
                                            tipo = "Detalle"
                                        });
                                    }
                                }
                            }

                            foreach (var d in i.hurts)
                            {
                                SqlCommand cmdD = con.CreateCommand();
                                cmdD.CommandTimeout = 0;
                                cmdD.CommandType = CommandType.StoredProcedure;
                                cmdD.CommandText = "Movil_InsertarEditar_Inspeccion_Control_Danio";
                                cmdD.Parameters.Add("@controlDanioInspeccionId", SqlDbType.Int).Value = d.controlInspeccionId;
                                cmdD.Parameters.Add("@id_inspeccion", SqlDbType.Int).Value = mensaje.codigoRetorno;
                                cmdD.Parameters.Add("@id_tipo_ctroldanio", SqlDbType.VarChar).Value = d.tipoControlId;
                                cmdD.Parameters.Add("@id_estado_danio", SqlDbType.Int).Value = d.estadoControlId;
                                cmdD.Parameters.Add("@lugar_ctroldanio", SqlDbType.VarChar).Value = d.lugarControl;
                                cmdD.Parameters.Add("@obs_ctroldanio", SqlDbType.VarChar).Value = d.observacion;
                                cmdD.Parameters.Add("@estado", SqlDbType.Int).Value = d.estado;
                                cmdD.Parameters.Add("@usuario", SqlDbType.Int).Value = d.usuario;

                                SqlDataReader drD = cmdD.ExecuteReader();
                                if (drD.HasRows)
                                {
                                    while (drD.Read())
                                    {
                                        detalle.Add(new MensajeDetalle()
                                        {
                                            detalleId = d.controlId,
                                            detalleRetornoId = drD.GetInt32(0),
                                            tipo = "Control"
                                        });
                                    }
                                }
                            }

                            foreach (var d in i.fotos)
                            {
                                SqlCommand cmdD = con.CreateCommand();
                                cmdD.CommandTimeout = 0;
                                cmdD.CommandType = CommandType.StoredProcedure;
                                cmdD.CommandText = "Movil_InsertarEditar_Inspeccion_Foto";
                                cmdD.Parameters.Add("@id_inspeccion_foto", SqlDbType.Int).Value = d.inspeccionFotoId;
                                cmdD.Parameters.Add("@id_inspeccion", SqlDbType.Int).Value = mensaje.codigoRetorno;
                                cmdD.Parameters.Add("@nombre_foto", SqlDbType.VarChar).Value = d.nombreFoto;
                                cmdD.Parameters.Add("@estado", SqlDbType.Int).Value = d.estado;
                                cmdD.Parameters.Add("@usuario", SqlDbType.Int).Value = d.usuario;

                                SqlDataReader drD = cmdD.ExecuteReader();
                                if (drD.HasRows)
                                {
                                    while (drD.Read())
                                    {
                                        detalle.Add(new MensajeDetalle()
                                        {
                                            detalleId = d.fotoId,
                                            detalleRetornoId = drD.GetInt32(0),
                                            tipo = "Foto"
                                        });
                                    }
                                }
                            }

                            mensaje.detalle = detalle;
                        }
                    }

                    con.Close();
                }

                return mensaje;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}