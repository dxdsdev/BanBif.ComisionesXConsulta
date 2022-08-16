using BanBif.ComisionesxConsulta.BE;
using BanBif.ComisionesxConsulta.Web.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanBif.ComisionesxConsulta.Web.Controllers
{
    public class ConfirmarController : Controller
    {
        // GET: Confirmar
        public ActionResult Index()
        {
            ViewBag.URL = ConfigurationManager.AppSettings.Get("UrlApp").ToString();
            return View();
        }

        public ActionResult ObtenerDatosClientes(ObtenerDatosClientesRequest request)
        {
            ObtenerDatosClientesResponse contenidoResponse = new ObtenerDatosClientesResponse();

            try
            {
                string strURL = ConfigurationManager.AppSettings["BaseUrlService"] + "api/ComisionesxConsulta/ObtenerDatosClientes";
                string response = WebApi<ObtenerDatosClientesRequest>.RequestWebApi(request, strURL);
                contenidoResponse = JsonConvert.DeserializeObject<ObtenerDatosClientesResponse>(response);
            }
            catch (Exception ex)
            {
                contenidoResponse.Result = false;
            }
            return Json(contenidoResponse);
        }

        public ActionResult ConfirmarToken(ConfirmarTokenRequest request)
        {
            var confirmarTokenResponse = new ConfirmarTokenResponse();

            try
            {
                string strURL = ConfigurationManager.AppSettings["BaseUrlService"] + "api/ComisionesxConsulta/ConfirmarToken";
                string response = WebApi<ConfirmarTokenRequest>.RequestWebApi(request, strURL);
                confirmarTokenResponse = JsonConvert.DeserializeObject<ConfirmarTokenResponse>(response);
            }
            catch (Exception ex)
            {
                confirmarTokenResponse.Result = false;
            }
            return Json(confirmarTokenResponse);
        }
    }
}