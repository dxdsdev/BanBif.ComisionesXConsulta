using BanBif.ComisionesxConsulta.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanBif.ComisionesxConsulta.DA
{
    public class ComisionesxConsultaDA
    {

        #region GLOBALES
        public ObtenerNombreClienteResult ObtenerNombreCliente(ObtenerNombreClienteRequest request)
        {
            using (var db = new panelEntities())
            {
                var cliente = db.tbl_mCOMISIONESXC_CLIENTE.Where(p => p.CODIGOCLIENTE == request.CodigoCliente).FirstOrDefault();
                var result = new ObtenerNombreClienteResult();

                if (cliente != null)
                {
                    result.nombreCliente = cliente.NOMBRE;
                }

                return result;
            }

        }
        #endregion

        #region PANTALLA LOGIN
        public ObtenerLoginResult ObtenerLogin(ObtenerLoginRequest request)
        {
            using (var db = new panelEntities())
            {
                var cliente = db.tbl_mCOMISIONESXC_CLIENTE.Where(p => p.DOCUMENTO == request.NumeroDocumento && p.NROTARJETA == request.NroTarjeta).FirstOrDefault(); //.ToList()
                var result = new ObtenerLoginResult();

                db.SaveChanges();

                if (cliente != null)
                {
                    result.CodigoCliente = cliente.CODIGOCLIENTE;
                    result.Nombre = cliente.NOMBRE;

                }

                return result;
            }

        }

        #endregion
    
        #region CORREO
        public CorreoDataCliente ObtenerDatosCliente(EnviarRequest request)
        {

            using (var db = new panelEntities())
            {
                var result = new CorreoDataCliente();
                var cliente = db.tbl_mCOMISIONESXC_CLIENTE.Where(p => p.CODIGOCLIENTE == request.CodigoCliente).FirstOrDefault();
                if (cliente != null)
                {
                    result.Correo = cliente.CORREO;
                    result.Nombre = cliente.NOMBRE;
                    result.Documento = cliente.DOCUMENTO;
                  
                }

                return result;
            }
        }


        #endregion

        #region TOKEN
        public TokenResponse GuardarToken(TokenRequest request)
        {
            using (var db = new panelEntities())
            {
                var response = new TokenResponse();

                /*DESACTIVAR TOKEN NO VALIDADOS POR DNI*/
                var listaToken = db.tbl_mCOMISIONESXC_TOKEN.Where(p => p.DOCUMENTO == request.Documento && p.ESTADO == 1 && p.VALIDADO == false).ToList();

                foreach (var item in listaToken)
                {
                    item.ESTADO = 0;
                }
                db.SaveChanges();

                /*REGISTRAR NUEVO TOKEN*/
                var token = new tbl_mCOMISIONESXC_TOKEN
                {
                    DOCUMENTO = request.Documento,
                    TOKEN = request.Token,
                    FECHA = DateTime.Now,
                    VALIDADO = false,
                    ESTADO = 1
                };

                try
                {
                    db.tbl_mCOMISIONESXC_TOKEN.Add(token);
                    db.SaveChanges();
                    response.Result = true;

                    return response;
                }
                catch (Exception e)
                {
                    response.Result = false;
                    response.Mensaje = e.InnerException.ToString();
                    return response;
                }
            }
        }

        public ConfirmarTokenResponse ConfirmarToken(ConfirmarTokenRequest request)
        {
            using (var db = new panelEntities())
            {
             
                var response = new ConfirmarTokenResponse();
                var cliente = db.tbl_mCOMISIONESXC_CLIENTE.Where(p => p.CODIGOCLIENTE == request.CodigoCliente).FirstOrDefault();
                var validarToken = request.Token;
                var token = db.tbl_mCOMISIONESXC_TOKEN.Where(p => p.DOCUMENTO == cliente.DOCUMENTO && p.TOKEN == validarToken && p.ESTADO == 1 && p.VALIDADO == false).FirstOrDefault();

                try
                {
                    /// TOKEN INVALIDO RETORNA FALSO Y EL MENSAJE
                    if (token == null)
                {
                    response.Result = false;
                    response.Mensaje = "El token ingresado no es valido.";
                    return response;
                }

                    //ACTUALIZAR EL TOKEN PARA MARCARLO COMO USADO. 
                    token.VALIDADO = true;
                    db.SaveChanges();

                    response.Result = true;
                    return response;
                }
                catch (Exception e)
                {
                    response.Result = false;
                    response.Mensaje = e.InnerException.ToString();
                    return response;
                }
            }
        }

        #endregion

        #region CLIENTES 
        public ObtenerDatosClientesResult ObtenerDatosClientes(ObtenerDatosClientesRequest request)
        {
            using (panelEntities db = new panelEntities())
            {
                ObtenerDatosClientesResult result = new ObtenerDatosClientesResult();
                /*OBTENER DATOS CLIENTE*/
                var cliente = db.tbl_mCOMISIONESXC_CLIENTE.Where(p => p.CODIGOCLIENTE == request.CodigoCliente).FirstOrDefault();

                var arrCorreo = cliente.CORREO.Split('@');

                result.Nombre = cliente.NOMBRE;
                result.Correo = arrCorreo[0].Substring(0, 1) + "********" + "@" + arrCorreo[1];

                return result;
            }

        }

        #endregion

        #region CUENTA CLIENTE
        public ListaNroCuentaResult ListarNroCuenta(ObtenerNroCuentaRequest request)
        {
            using (panelEntities db = new panelEntities())
            {
                ListaNroCuentaResult result = new ListaNroCuentaResult();
                /*OBTENER DATOS CLIENTE*/
                var cliente = db.tbl_mCOMISIONESXC_CLIENTE.Where(p => p.CODIGOCLIENTE == request.CodigoCliente).FirstOrDefault();

                result.Nombre = cliente.NOMBRE;

                /*OBTENER TARJETAS*/
                var listaNroCuenta = db.tbl_mCOMISIONESXC_CLIENTE.Where(p => p.ESTADO == 1 && p.DOCUMENTO == cliente.DOCUMENTO).ToList();

                var listaResult = new List<NroCuenta>();
                foreach (var item in listaNroCuenta)
                {

                    listaResult.Add(new NroCuenta
                    {
                        CodigoCliente = item.CODIGOCLIENTE,
                        NroCuentaCliente = item.TIPO_PRODUCTO + " - ***** ***** ***** " + item.ULTIMOSDIGITOSNROCUENTA
                    });
                }
                result.NroCuenta = listaResult;
                return result;
            }

        }



        #endregion

        #region UBIGEO
        public ListadoDistritoResult ListarDistrito()
        {
            using (panelEntities db = new panelEntities())
            {
                ListadoDistritoResult result = new ListadoDistritoResult();
                var listaDistrito = db.tbl_aCOMISIONESXC_UBIGEO_CIUDAD.Where(p => p.ESTADO == 1).ToList();

                var listaResult = new List<Distrito>();
                foreach (var item in listaDistrito)
                {
                    listaResult.Add(new Distrito
                    {
                        CodigoDistrito = item.ID_CIUDAD,
                        NombreDistrito = item.NOMBRE_CIUDAD
                    });
                }
                result.listadoDistrito = listaResult;
                return result;
            }
        }
        public ListarOficinaResult ListarOficina(ListarOficinaRequest request)
        {
            using (panelEntities db = new panelEntities())
            {
                ListarOficinaResult result = new ListarOficinaResult();
                var listaOficina = db.tbl_aCOMISIONESXC_UBIGEO_OFICINA.Where(p => p.ESTADO == 1 && p.ID_CIUDAD == request.CodigoDistrito).ToList();

                var listaResult = new List<Oficina>();
                foreach (var item in listaOficina)
                {
                    listaResult.Add(new Oficina
                    {
                        CodigoOficina = item.ID_OFICINA,
                        DescripcionOficina = item.NOMBRE_OFICINA
                    });
                }
                result.ListaOficinas = listaResult;
                return result;
            }

        }
        #endregion

        #region ANIOS
        public ObtenerAniosResult ObtenerAnios(ObtenerAniosRequest request)
        {
            using (var db = new panelEntities())
            {
                var listaAnios = db.TBL_ACOMISIONESXC_ANIOS.Where(p =>  p.ESTADO == request.Estado).ToList();
                var result = new ObtenerAniosResult();
                var lista = new List<Anio>();

                foreach (var item in listaAnios) {
                    lista.Add(new Anio {
                     Codigo = item.CODIGO_ANIO,
                     Descripcion = item.ANIO
                    });
                }

                result.Lista = lista;
                return result;
            }

        }

        #endregion

        #region CONSULTA ANIOS
        public ObtenerConsultaAnioResult ObtenerConsultaAnio(ObtenerConsultaAnioRequest request)
        {
            using (var db = new panelEntities())
            {
                var cliente = db.TBL_MCOMISIONESXC_CONSULTA_ANIO.Where(p => p.ID_CONSULTADETALL == request.IdConsultaDet).FirstOrDefault();
                var result = new ObtenerConsultaAnioResult();

                if (cliente != null)
                {
                    result.CodigoConsulta = (int)cliente.CODIGOCONSULTA;
                    result.CodigoAnio = (int)cliente.CODIGO_ANIO;
                }

                return result;
            }

        }

        #endregion

        #region CONSULTA
        public ConsultaResponse GuardarConsulta(ConsultaRequest request)
        {
            using (var db = new panelEntities())
            {
                var response = new ConsultaResponse();
                var response1 = new ConfirmarTokenResponse();
                var cliente = db.tbl_mCOMISIONESXC_CLIENTE.Where(p => p.CODIGOCLIENTE == request.CodigoCliente).FirstOrDefault();
                var distrito = db.tbl_aCOMISIONESXC_UBIGEO_CIUDAD.Where(p => p.ID_CIUDAD == request.CodigoDistrito).FirstOrDefault();
                var oficina = db.tbl_aCOMISIONESXC_UBIGEO_OFICINA.Where(p => p.ID_OFICINA == request.CodigoOficina).FirstOrDefault();
                var validarToken = request.Token;
                var token = db.tbl_mCOMISIONESXC_TOKEN.Where(p => p.DOCUMENTO == cliente.DOCUMENTO && p.TOKEN == validarToken && p.ESTADO == 1 && p.VALIDADO == false).FirstOrDefault();


                var dbproceso = db.tbl_mCOMISIONESXC_CONSULTA.OrderByDescending(p => p.CODIGO_PROCESO).FirstOrDefault();
                var proceso = 0;



                if (dbproceso == null)
                {
                    proceso = 1;
                }
                else
                {
                    proceso = dbproceso.CODIGO_PROCESO.Value + 1;
                }

                try
                {
                    /// TOKEN INVALIDO RETORNA FALSO Y EL MENSAJE
                    if (token == null)
                {
                    response1.Result = false;
                    response1.Mensaje = "El token ingresado no es valido.";
                    return response;
                }
                // SI EL TOKEN ES VALIDO HACE TODO LO SIGUIENTE               

                //ACTUALIZAR EL TOKEN PARA MARCARLO COMO USADO. 
                token.VALIDADO = true;
                db.SaveChanges();

                    var listaConsulta = new tbl_mCOMISIONESXC_CONSULTA
                {
                    ATENDIDO = 0,
                    CARGOTARJETA = request.CargoTarjeta == 1 ? true : false,
                    CHK_TERMINOS_CONDICIONES = request.TerminosCondiciones == 1 ? true : false,
                    CHK_TARIFARIO = request.TerminosCondiciones == 1 ? true : false,
                    CODIGOCLIENTE = request.CodigoCliente,
                    ID_CIUDAD = request.CodigoDistrito,
                    ID_OFICINA = request.CodigoOficina,
                    FECHA_CONSULTA = DateTime.Now,
                    ESTADO = 1,
                    CODIGO_PROCESO = proceso
                };

                    db.tbl_mCOMISIONESXC_CONSULTA.Add(listaConsulta);
                    db.SaveChanges();

                    var aniosSelec = new List<Anio>();
                    foreach (var item in request.ListaAnios)
                    {
                        aniosSelec.Add(new Anio
                        {
                            Codigo = item.Codigo,
                            Descripcion = item.Descripcion
                        });

                        var nuevo2 = new TBL_MCOMISIONESXC_CONSULTA_ANIO
                        {
                            CODIGOCONSULTA = listaConsulta.CODIGOCONSULTA,
                            CODIGO_ANIO = item.Codigo
                        };

                        db.TBL_MCOMISIONESXC_CONSULTA_ANIO.Add(nuevo2);
                        db.SaveChanges();


                       
                    }
                    response.Result = true;

                    /*TRANSACCION RESULT (ENVIO CORREO)*/

                    var consultaResult = new ConsultaResult
                    {
                        CodigoConsulta = listaConsulta.CODIGOCONSULTA,
                        Nombres = cliente.NOMBRE,
                        Correo = cliente.CORREO,
                        CantidadAnios = aniosSelec.Count,
                        DescripcionDistrito = distrito.NOMBRE_CIUDAD,
                        DescripcionOficina = oficina.NOMBRE_OFICINA,
                        TipoProducto = cliente.TIPO_PRODUCTO,
                        NroProducto = cliente.TIPO_PRODUCTO == "AHORROS" ? cliente.NROTARJETA : cliente.NUMEROCUENTA.Substring(cliente.NUMEROCUENTA.Length - 4, 4),
                        AniosSeleccionado = aniosSelec
                    };

                    response.Data = consultaResult;
                    return response;
                }
                catch (Exception e)
                {
                    response.Result = false;
                    response.Mensaje = e.InnerException.ToString();
                    return response;
                }
            }


        }

        #endregion
    }
}
