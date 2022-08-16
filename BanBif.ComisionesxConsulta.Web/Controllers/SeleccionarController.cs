using BanBif.ComisionesxConsulta.BE;
using BanBif.ComisionesxConsulta.Web.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;


namespace BanBif.ComisionesxConsulta.Web.Controllers
{
    public class SeleccionarController : Controller
    {
        // GET: SeleccionarAnios
        public ActionResult Index()
        {
            ViewBag.URL = ConfigurationManager.AppSettings.Get("UrlApp").ToString();

            /*LISTAR DISTRITOS*/
            ListadoDistritoResponse ContenidoResponse1 = new ListadoDistritoResponse();
            string strURL1 = ConfigurationManager.AppSettings["BaseUrlService"] + "api/ComisionesxConsulta/ListarDistritos";
            string response1 = WebApi<Distrito>.RequestWebApi(null, strURL1);
            ContenidoResponse1 = JsonConvert.DeserializeObject<ListadoDistritoResponse>(response1);
            ViewBag.Distrito = ContenidoResponse1.Data;

            /*LISTAR AÑOS*/
            var request = new ObtenerAniosRequest { Estado = 1 };
            ObtenerAniosResponse response = new ObtenerAniosResponse();
            string strURLAnio = ConfigurationManager.AppSettings["BaseUrlService"] + "api/ComisionesxConsulta/ObtenerAnios";
            string result = WebApi<ObtenerAniosRequest>.RequestWebApi(request, strURLAnio);
            response = JsonConvert.DeserializeObject<ObtenerAniosResponse>(result);
            ViewBag.Anios = response.Data;
            var codigosAnios = "";
            foreach (var item in response.Data.Lista) {
                codigosAnios += "|" + item.Codigo.ToString();
            }
            ViewBag.CodigosAnios = codigosAnios;

            return View();
        }

        public ActionResult ListarOficinas(ListarOficinaRequest request)
        {
            ListarOficinaResponse contenidoResponse = new ListarOficinaResponse();

            try
            {
                ListarOficinaResult oListarRazaResult = new ListarOficinaResult();
                string strURL = ConfigurationManager.AppSettings["BaseUrlService"] + "api/ComisionesxConsulta/ListarOficina";
                string response = WebApi<ListarOficinaRequest>.RequestWebApi(request, strURL);
                contenidoResponse = JsonConvert.DeserializeObject<ListarOficinaResponse>(response);
            }
            catch (Exception ex)
            {
                contenidoResponse.Result = false;
            }
            return Json(contenidoResponse);
        }

        public ActionResult ListarCuenta(ObtenerNroCuentaRequest request)
        {
            ListaNroCuentaResponse contenidoResponse = new ListaNroCuentaResponse();

            try
            {
                string strURL = ConfigurationManager.AppSettings["BaseUrlService"] + "api/ComisionesxConsulta/ListarNroCuenta";
                string response = WebApi<ObtenerNroCuentaRequest>.RequestWebApi(request, strURL);
                contenidoResponse = JsonConvert.DeserializeObject<ListaNroCuentaResponse>(response);
            }
            catch (Exception ex)
            {
                contenidoResponse.Result = false;
            }
            return Json(contenidoResponse);
        }

        public ActionResult ObtenerNroCuenta(ObtenerNroCuentaRequest request)
        {
            ListaNroCuentaResponse contenidoResponse = new ListaNroCuentaResponse();

            try
            {
                string strURL = ConfigurationManager.AppSettings["BaseUrlService"] + "api/ComisionesxConsulta/ListarNroCuenta";
                string response = WebApi<ObtenerNroCuentaRequest>.RequestWebApi(request, strURL);
                contenidoResponse = JsonConvert.DeserializeObject<ListaNroCuentaResponse>(response);
            }
            catch (Exception ex)
            {
                contenidoResponse.Result = false;
            }
            return Json(contenidoResponse);
        }

        public ActionResult EnviarToken(EnviarRequest request)
        {
            CorreoResponse contenidoResponse = new CorreoResponse();

            try

            {
                string strURL = ConfigurationManager.AppSettings["BaseUrlService"] + "api/ComisionesxConsulta/EnviarToken";
                string response = WebApi<EnviarRequest>.RequestWebApi(request, strURL);
                contenidoResponse = JsonConvert.DeserializeObject<CorreoResponse>(response);
            }
            catch (Exception ex)
            {
                contenidoResponse.Enviado = false;
                contenidoResponse.Resultado = ex.InnerException.ToString();

            }
            return Json(contenidoResponse);
        }

        public ActionResult ObtenerAnios(ObtenerAniosRequest request)
        {
            ObtenerAniosResponse contenidoResponse = new ObtenerAniosResponse();

            try

            {
                string strURL = ConfigurationManager.AppSettings["BaseUrlService"] + "api/ComisionesxConsulta/ObtenerAnios";
                string response = WebApi<ObtenerAniosRequest>.RequestWebApi(request, strURL);
                contenidoResponse = JsonConvert.DeserializeObject<ObtenerAniosResponse>(response);
            }
            catch (Exception ex)
            {
                contenidoResponse.Result = false;
                contenidoResponse.Mensaje = ex.InnerException.ToString();

            }
            return Json(contenidoResponse);
        }

    }
}