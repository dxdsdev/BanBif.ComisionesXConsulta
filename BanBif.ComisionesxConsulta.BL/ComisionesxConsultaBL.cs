using BanBif.ComisionesxConsulta.BE;
using BanBif.ComisionesxConsulta.DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanBif.ComisionesxConsulta.BL
{
    public class ComisionesxConsultaBL
    {
        #region GLOBALES
        public ObtenerNombreClienteResponse ObtenerNombreCliente(ObtenerNombreClienteRequest request)
        {

            var response = new ObtenerNombreClienteResponse();
            try
            {
                var comisionesDA = new ComisionesxConsultaDA();
                var data = comisionesDA.ObtenerNombreCliente(request);
                response.Data = data;

                if (data.nombreCliente.ToString() != "")
                {
                    response.Result = true;
                }
                else
                {
                    response.Mensaje = "Datos No encontrados";
                }
            }
            catch (Exception ex)
            {
                response.Mensaje = ex.InnerException.ToString();
            }

            return response;
        }
        #endregion

        #region PANTALLA LOGIN

        public ObtenerLoginResponse ObtenerLogin(ObtenerLoginRequest request)
        {

            var response = new ObtenerLoginResponse();
            try
            {
                var comisionesDA = new ComisionesxConsultaDA();
                var data = comisionesDA.ObtenerLogin(request);


                if (data.CodigoCliente > 0)
                {
                    response.Data = data;
                    response.Result = true;
                    response.Mensaje = "Número de Tarjeta y Documento Verificado";
                }
                else
                {
                    response.Result = false;
                    response.Mensaje = "Número de Tarjeta y/o Documento no encontrado";
                }
            }
            catch (Exception ex)
            {
                response.Mensaje = ex.InnerException.ToString();
            }

            return response;
        }

        #endregion

        #region CLIENTES
        public ObtenerDatosClientesResponse ObtenerDatosClientes(ObtenerDatosClientesRequest request)
        {
            var response = new ObtenerDatosClientesResponse();
            try
            {
                var comisionesDA = new ComisionesxConsultaDA();
                var resultado = comisionesDA.ObtenerDatosClientes(request);
                if (resultado.Nombre != " " || resultado.Correo != " ")
                {
                    response.Result = true;
                    response.Data = resultado;
                }
                else
                {
                    response.Result = false;
                    response.Mensaje = "No se encontraron registros.";
                }
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Mensaje = ex.InnerException.ToString();
            }
            return response;
        }

        #endregion

        #region CORREOS
        public CorreoResponse EnviarToken(EnviarRequest request)
        {
            /*OBTENER DATA CLIENTE*/
            var comisionesDA = new ComisionesxConsultaDA();
            var BlComun = new ComunBL();
            var Cliente = comisionesDA.ObtenerDatosCliente(request);

            Random random = new Random();
            string Codigo = random.Next(1000, 9999).ToString();

            var correoAsunto = request.Tipo == "A" ? "Código de verificación de identidad" : "Código de solicitud de consulta";

            var CorreoRequest = new CorreoRequest
            {
                From = "solicitudes@banbif.com.pe",
                To = Cliente.Correo,
                Asunto = correoAsunto,
                Mensaje = MensajeToken(Codigo, Cliente.Nombre, request.Tipo)
            };

            var RespuestaCorreo = BlComun.EnviarCorreo(CorreoRequest);
            if (RespuestaCorreo.Enviado == true)
            {
                var Token = new TokenRequest();
                Token.Documento = Cliente.Documento;
                Token.Token = Codigo;
                comisionesDA.GuardarToken(Token);
            }

            return RespuestaCorreo;
        }


        static string MensajeToken(string codigoValidacion, string nombreCliente, string tipo)
        {
            var strHTML = "";
            strHTML += "    <br><br>      <table bgcolor = '#e5e5e5' border = '0' cellpadding = '0' cellspacing = '0' style = 'border-collapse: collapse;font-family:arial;text-align: center;color: #515050;font-size: 16px;' width = '100%' >";
            strHTML += "                     <tr>";
            strHTML += "                         <td>";
            strHTML += "                           <table bgcolor = '#FFFFFF' border = '0' cellpadding = '0' cellspacing = '0' style = 'border-collapse: collapse;font-family:arial;text-align: center; font-size: 16px;width: 640px;' align = 'center' >";
            strHTML += "                                   <tr>";
            strHTML += "                                     <td align='center' valign='middle' style='border:none'><span style ='font-size:28px; font-family:arial;'><strong>";
            strHTML += "¡HOLA!, " + nombreCliente + "</strong></span></td>";
            strHTML += "</tr>";
            strHTML += "<tr>";
            strHTML += "<td height='15'>";
            strHTML += "</td>";
            strHTML += "</tr>";
            strHTML += "<tr>";
            strHTML += "<td width='570' style='font-size: 11px; text-align: justify; color:#303030; padding-left: 30px;' >";

            if (tipo == "A")
            {
                strHTML += "Ingresa el siguiente código para verificar tu identidad: " + codigoValidacion + ".";
            }
            else
            {
                strHTML += "Ingresa el siguiente código para confirmar tu solicitud: " + codigoValidacion + ".";
            }

            strHTML += "<br><br>";
            strHTML += "Para garantizar la seguridad de tu correo electrónico, no respondas a este mensaje.";
            strHTML += "<br><br>";
            strHTML += "Atentamente,";
            strHTML += "<br><br>";
            strHTML += "Tu banco,";
            strHTML += "<br><br>";
            strHTML += "BANBIF";
            strHTML += "<br>";
            strHTML += "</span></td>";
            strHTML += "</tr>";
            strHTML += "<tr>";
            strHTML += "<td height='25'>";
            strHTML += "</td>";
            strHTML += "</tr>";
            strHTML += "<tr width='640'>";
            strHTML += "<td width='640'>";
            strHTML += "<img src='http://csbanbif.embluemail.com.s3.amazonaws.com/diciembre-030119-TC-Experiencia/6.png' width='640' border='0' style='display:block' >";
            strHTML += "</td>";
            strHTML += "</tr>";
            strHTML += "<tr>";
            strHTML += "<td width='640'>";
            strHTML += "<table bgcolor='#FFFFFF' border='0' cellpadding='0' cellspacing='0' width='640' style='border-collapse: collapse;font-family:arial' >";
            strHTML += "<tr>";
            strHTML += "<td width='35'>";
            strHTML += "</td>";
            strHTML += "<td width='570'>";
            strHTML += "<table bgcolor='#FFFFFF' border='0' cellpadding='0' cellspacing='0' width='570' style='border-collapse: collapse;font-family:arial'>";
            strHTML += "<tr>";
            strHTML += "<td width='570' style='font-size: 11px; text-align: justify; color:#303030;' >";
            strHTML += "<br>";
            strHTML += "<p>El contenido de este mensaje es a título parcial y no es un folleto informativo. Prevalecen las definiciones de cada cobertura que se especifican en la póliza del seguro Pet Lover N° 310166440 contratada por CHUBB PERU. En caso de consultas, reclamos y/o siniestros llamar a la Central de Atención al Cliente de Chubb Perú al 417-5000 (Lima), escribe a atencion . seguros @ chubb . com , ingresa a la página web www . chubb . com . pe y/o visita la oficina ubicada en Calle Amador Merino Reyna N° 267 Oficina 402, San Isidro. Aplican exclusiones detalladas en la póliza. El plazo para la atención de consultas y/o reclamos es de 30 días contando desde la presentación del reclamo y/o consulta, sin que ello implique caducidad de su derecho. Para mayor información puedes ingresar a la página web www . BanBif . com . pe y/o www . Chubb. com / pe. Este seguro ofrecido por CHUBB PERU S.A. puede ser adquirido en las oficinas de BanBif. BanBif no se responsabiliza legalmente por la disponibilidad, idoneidad, calidad, condiciones, entrega, exclusiones, daño o perjuicio respecto a los seguros ofrecidos por CHUBB. La presente información es parcial y no constituye las condiciones de la Póliza, prevaleciendo los términos del contrato suscrito ante CHUBB y el adquirente del seguro. BanBif interviene en calidad de comercializador del seguro Pet Lover de CHUBB, conforme al Reglamento de Comercialización de Productos de Seguros Res. SBS N° 1121-2017, Ley Complementaria a la Ley de Protección al Consumidor en Materia de Servicios Financieros Ley N°28587 y sus normas modificatorias, así como el Reglamento de Gestión de Conducta de Mercado del Sistema Financiero aprobado por Res. SBS N°3274-2017 y sus modificatorias.</p>";
            strHTML += "<br>";
            strHTML += "</td>";
            strHTML += "</tr>";
            strHTML += "</table>";
            strHTML += "</td>";
            strHTML += "<td width='35'>";
            strHTML += "</td>";
            strHTML += "</tr>";
            strHTML += "</table>";
            strHTML += "</td>";
            strHTML += "</tr>";
            strHTML += "<tr>";
            strHTML += "<td>";
            strHTML += "<table style='border: 0' align='center' width='100%' cellpadding='0' cellspacing='0' border='0' >";
            strHTML += "<tbody>";
            strHTML += "<tr >";
            strHTML += "<td width='40'></td>";
            strHTML += "<td width='550'>";
            strHTML += "<table style='border: 0' align='center' width='100%' cellpadding='0' cellspacing='0' border='0' >";
            strHTML += "<tbody>";
            strHTML += "<tr>";
            strHTML += "<td width='199' ></td>";
            strHTML += "<td width='260' align='justify'><span style='color:#000000;font-size:10px;text-align:justify;font-family:arial;font-weight:bold;display:block'></span></td>";
            strHTML += "<td width='90'><img src='https://ci4.googleusercontent.com/proxy/VnLNbt5DieoNSZDOvMi-kILz52bs8gecrKzcbReMoBa6CpsA9em4hbVvr1xrOrbPRzIjtHS8a7Vjq-SD5ghWUoK8DcpfFp_auYa8omThgNLOOtPKli129-T4hno=s0-d-e1-ft#http://i.embluejet.com/ImagenesMoxie/4569/images/Campana_seguros/ds-b.jpg' style = 'border:none' class='CToWUd'></td>";
            strHTML += "</tr>";
            strHTML += "</tbody>";
            strHTML += "</table>";
            strHTML += "</td>";
            strHTML += "<td width='50'></td>";
            strHTML += "</tr>";
            strHTML += "</tbody>";
            strHTML += "</table>";
            strHTML += "</td>";
            strHTML += "</tr>";
            strHTML += "<tr>";
            strHTML += "<td width='640' height='10'>";
            strHTML += "</td>";
            strHTML += "</tr>";
            strHTML += "<tr width='640'>";
            strHTML += "<td width='640'>";
            strHTML += "<img src='http://csbanbif.embluemail.com.s3.amazonaws.com/diciembre-030119-TC-Experiencia/6.png' width='640' border='0' style='display:block'>";
            strHTML += "</td>";
            strHTML += "</tr>";
            strHTML += "<tr>";
            strHTML += "<td width='640' height='10'>";
            strHTML += "</td>";
            strHTML += "</tr>";
            strHTML += "</table>";
            strHTML += "</td>";
            strHTML += "</tr>";
            strHTML += "</table>";

            return strHTML;
        }

        public ConfirmarTokenResponse ConfirmarToken(ConfirmarTokenRequest request)
        {
            var comisionesDA = new ComisionesxConsultaDA();
            var Transaccion = comisionesDA.ConfirmarToken(request);
    
            Transaccion.Data = null;

            return Transaccion;
        }
        #endregion

        #region CUENTA CLIENTE
        public ListaNroCuentaResponse ListarNroCuenta(ObtenerNroCuentaRequest request)
        {
            var response = new ListaNroCuentaResponse();
            try
            {
                var comisionesDA = new ComisionesxConsultaDA();
                var resultado = comisionesDA.ListarNroCuenta(request);
                if (resultado.NroCuenta.Count > 0)
                {
                    response.Result = true;
                    response.Data = resultado;
                }
                else
                {
                    response.Result = false;
                    response.Mensaje = "No se encontraron registros.";
                }
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Mensaje = ex.InnerException.ToString();
            }
            return response;
        }


        #endregion

        #region UBIGEO
        public ListadoDistritoResponse ListarDistritos()
        {
            var response = new ListadoDistritoResponse();
            try
            {
                var comisionesDA = new ComisionesxConsultaDA();
                var resultado = comisionesDA.ListarDistrito();
                if (resultado.listadoDistrito.Count > 0)
                {
                    response.Result = true;
                    response.Data = resultado;
                }
                else
                {
                    response.Result = false;
                    response.Mensaje = "No se encontraron registros.";
                }
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Mensaje = ex.InnerException.ToString();
            }
            return response;
        }

        public ListarOficinaResponse ListarOficina(ListarOficinaRequest request)
        {
            var response = new ListarOficinaResponse();
            try
            {
                var comisionesDA = new ComisionesxConsultaDA();
                var resultado = comisionesDA.ListarOficina(request);
                if (resultado.ListaOficinas.Count > 0)
                {
                    response.Result = true;
                    response.Data = resultado;
                }
                else
                {
                    response.Result = false;
                    response.Mensaje = "No se encontraron registros.";
                }
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Mensaje = ex.InnerException.ToString();
            }
            return response;
        }

        #endregion

        #region CONSULTA
        public ConsultaResponse GuardarConsulta(ConsultaRequest request)
        {
            var response = new ConsultaResponse();
       
            var comisionesDA = new ComisionesxConsultaDA();
            var Consulta = comisionesDA.GuardarConsulta(request);
            if (Consulta.Result)
                {
                    /*ENVIO CORREO*/
                    var BlComun = new ComunBL();
                    var CorreoRequest = new CorreoRequest
                    {
                        From = "solicitudes@banbif.com.pe",
                        To = Consulta.Data.Correo,
                        Asunto = "Confirmación de Consultas",
                        Mensaje = MensajeAfiliacion(Consulta.Data)
                    };

                    BlComun.EnviarCorreo(CorreoRequest);
                }
                Consulta.Data = null;

                return Consulta;

        string MensajeAfiliacion(ConsultaResult solicitud)
            {
                var strHTML = "";
                strHTML += "    <br><br> <table bgcolor = '#e5e5e5' border = '0' cellpadding = '0' cellspacing = '0' style = 'border-collapse: collapse;font-family:arial;text-align: center;color: #515050;font-size: 16px;' width = '100%' >";
                strHTML += "                     <tr>";
                strHTML += "                         <td>";
                strHTML += "                           <table bgcolor = '#FFFFFF' border = '0' cellpadding = '0' cellspacing = '0' style = 'border-collapse: collapse;font-family:arial;text-align: center; font-size: 16px;width: 640px;' align = 'center' >";
                strHTML += "                                   <tr>";
                strHTML += "                                     <td align='center' valign='middle' style='border:none'><span style ='font-size:28px; font-family:arial;'><strong>";
                strHTML += "¡HOLA! " + solicitud.Nombres + ",</strong></span></td>";
                strHTML += "</tr>";
                strHTML += "<tr>";
                strHTML += "<td height='15'>";
                strHTML += "</td>";
                strHTML += "</tr>";
                strHTML += "<tr>";
                strHTML += "<td width='570' style='font-size: 11px; text-align: justify; color:#303030; padding-left: 30px;' >";
                strHTML += "Tu solcitiud de consulta de movientos en tu cuenta es exitosa";
                strHTML += "<br><br>";
                strHTML += "Tu cuenta para movientos es:" + " ***** ***** ***** " + solicitud.NroProducto;
                strHTML += "<br><br>";

                strHTML += "La oficina de recojo es: " + solicitud.DescripcionDistrito + " - " + solicitud.DescripcionOficina;
                strHTML += "<br><br>";

                if (solicitud.CantidadAnios > 0)
                {
                    strHTML += "Monto a cobrar: S/ " + 50 * solicitud.CantidadAnios;
                }
                else
                {
                    strHTML += "Monto a cobrar:  S/ " + solicitud.CantidadAnios;
                }

                strHTML += "<br><br>";

                strHTML += "La fecha de solicitud es: " + DateTime.Now.ToString("dd-MM-yyyy : MM:ss");

                strHTML += "<br><br>";
                strHTML += "Para garantizar la seguridad de tu correo electrónico, no respondas a este mensaje.";
                strHTML += "<br><br>";
                strHTML += "Atentamente,";
                strHTML += "<br><br>";
                strHTML += "Tu banco,";
                strHTML += "<br><br>";
                strHTML += "BANBIF";
                strHTML += "<br>";
                strHTML += "</span></td>";
                strHTML += "</tr>";
                strHTML += "<tr>";
                strHTML += "<td height='25'>";
                strHTML += "</td>";
                strHTML += "</tr>";
                strHTML += "<tr width='640'>";
                strHTML += "<td width='640'>";
                strHTML += "<img src='http://csbanbif.embluemail.com.s3.amazonaws.com/diciembre-030119-TC-Experiencia/6.png' width='640' border='0' style='display:block' >";
                strHTML += "</td>";
                strHTML += "</tr>";
                strHTML += "<tr>";
                strHTML += "<td width='640'>";
                strHTML += "<table bgcolor='#FFFFFF' border='0' cellpadding='0' cellspacing='0' width='640' style='border-collapse: collapse;font-family:arial' >";
                strHTML += "<tr>";
                strHTML += "<td width='35'>";
                strHTML += "</td>";
                strHTML += "<td width='570'>";
                strHTML += "<table bgcolor='#FFFFFF' border='0' cellpadding='0' cellspacing='0' width='570' style='border-collapse: collapse;font-family:arial'>";
                strHTML += "<tr>";
                strHTML += "<td width='570' style='font-size: 11px; text-align: justify; color:#303030;' >";
                strHTML += "<br>";
                strHTML += "<p>El contenido de este mensaje es a título parcial y no es un folleto informativo. Prevalecen las definiciones de cada cobertura que se especifican en la póliza del seguro Pet Lover N° 310166440 contratada por CHUBB PERU. En caso de consultas, reclamos y/o siniestros llamar a la Central de Atención al Cliente de Chubb Perú al 417-5000 (Lima), escribe a atencion . seguros @ chubb . com , ingresa a la página web www . chubb . com . pe y/o visita la oficina ubicada en Calle Amador Merino Reyna N° 267 Oficina 402, San Isidro. Aplican exclusiones detalladas en la póliza. El plazo para la atención de consultas y/o reclamos es de 30 días contando desde la presentación del reclamo y/o consulta, sin que ello implique caducidad de su derecho. Para mayor información puedes ingresar a la página web www . BanBif . com . pe y/o www . Chubb. com / pe. Este seguro ofrecido por CHUBB PERU S.A. puede ser adquirido en las oficinas de BanBif. BanBif no se responsabiliza legalmente por la disponibilidad, idoneidad, calidad, condiciones, entrega, exclusiones, daño o perjuicio respecto a los seguros ofrecidos por CHUBB. La presente información es parcial y no constituye las condiciones de la Póliza, prevaleciendo los términos del contrato suscrito ante CHUBB y el adquirente del seguro. BanBif interviene en calidad de comercializador del seguro Pet Lover de CHUBB, conforme al Reglamento de Comercialización de Productos de Seguros Res. SBS N° 1121-2017, Ley Complementaria a la Ley de Protección al Consumidor en Materia de Servicios Financieros Ley N°28587 y sus normas modificatorias, así como el Reglamento de Gestión de Conducta de Mercado del Sistema Financiero aprobado por Res. SBS N°3274-2017 y sus modificatorias.</p>";
                strHTML += "<br>";
                strHTML += "</td>";
                strHTML += "</tr>";
                strHTML += "</table>";
                strHTML += "</td>";
                strHTML += "<td width='35'>";
                strHTML += "</td>";
                strHTML += "</tr>";
                strHTML += "</table>";
                strHTML += "</td>";
                strHTML += "</tr>";
                strHTML += "<tr>";
                strHTML += "<td>";
                strHTML += "<table style='border: 0' align='center' width='100%' cellpadding='0' cellspacing='0' border='0' >";
                strHTML += "<tbody>";
                strHTML += "<tr >";
                strHTML += "<td width='40'></td>";
                strHTML += "<td width='550'>";
                strHTML += "<table style='border: 0' align='center' width='100%' cellpadding='0' cellspacing='0' border='0' >";
                strHTML += "<tbody>";
                strHTML += "<tr>";
                strHTML += "<td width='199' ></td>";
                strHTML += "<td width='260' align='justify'><span style='color:#000000;font-size:10px;text-align:justify;font-family:arial;font-weight:bold;display:block'></span></td>";
                strHTML += "<td width='90'><img src='https://ci4.googleusercontent.com/proxy/VnLNbt5DieoNSZDOvMi-kILz52bs8gecrKzcbReMoBa6CpsA9em4hbVvr1xrOrbPRzIjtHS8a7Vjq-SD5ghWUoK8DcpfFp_auYa8omThgNLOOtPKli129-T4hno=s0-d-e1-ft#http://i.embluejet.com/ImagenesMoxie/4569/images/Campana_seguros/ds-b.jpg' style = 'border:none' class='CToWUd'></td>";
                strHTML += "</tr>";
                strHTML += "</tbody>";
                strHTML += "</table>";
                strHTML += "</td>";
                strHTML += "<td width='50'></td>";
                strHTML += "</tr>";
                strHTML += "</tbody>";
                strHTML += "</table>";
                strHTML += "</td>";
                strHTML += "</tr>";
                strHTML += "<tr>";
                strHTML += "<td width='640' height='10'>";
                strHTML += "</td>";
                strHTML += "</tr>";
                strHTML += "<tr width='640'>";
                strHTML += "<td width='640'>";
                strHTML += "<img src='http://csbanbif.embluemail.com.s3.amazonaws.com/diciembre-030119-TC-Experiencia/6.png' width='640' border='0' style='display:block'>";
                strHTML += "</td>";
                strHTML += "</tr>";
                strHTML += "<tr>";
                strHTML += "<td width='640' height='10'>";
                strHTML += "</td>";
                strHTML += "</tr>";
                strHTML += "</table>";
                strHTML += "</td>";
                strHTML += "</tr>";
                strHTML += "</table>";

                return strHTML;
            }
        }
        #endregion

        #region ANIOS
        public ObtenerAniosResponse ObtenerAnios(ObtenerAniosRequest request)
        {

            var response = new ObtenerAniosResponse();
            try
            {
                var comisionesDA = new ComisionesxConsultaDA();
                var data = comisionesDA.ObtenerAnios(request);
                response.Data = data;

                if (data.Lista.Count > 0)
                {
                    response.Result = true;
                }
                else
                {
                    response.Mensaje = "Datos No encontrados";
                }
            }
            catch (Exception ex)
            {
                response.Mensaje = ex.InnerException.ToString();
            }

            return response;
        }

        #endregion

        #region CONSULTA ANIOS
        public ObtenerConsultaAnioResponse ObtenerConsultaAnio(ObtenerConsultaAnioRequest request)
        {

            var response = new ObtenerConsultaAnioResponse();
            try
            {
                var comisionesDA = new ComisionesxConsultaDA();
                var data = comisionesDA.ObtenerConsultaAnio(request);
                response.Data = data;

                if (data.CodigoConsulta>0)
                {
                    response.Result = true;
                }
                else
                {
                    response.Mensaje = "Datos No encontrados";
                }
            }
            catch (Exception ex)
            {
                response.Mensaje = ex.InnerException.ToString();
            }

            return response;
        }

        #endregion
    }
}
