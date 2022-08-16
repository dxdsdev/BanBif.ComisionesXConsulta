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
    public class ResumenController : Controller
    {
        // GET: Resumen
        public ActionResult Index()
        {
            ViewBag.Dia = DateTime.Now.ToString("dd/MM/yyyy");
            ViewBag.Hora = DateTime.Now.ToString("HH:mm");
            ViewBag.URL = ConfigurationManager.AppSettings.Get("UrlApp").ToString();
            return View();
        }

        public ActionResult ObtenerNombreCliente(ObtenerNombreClienteRequest request)
        {
            ObtenerNombreClienteResponse contenidoResponse = new ObtenerNombreClienteResponse();

            try
            {
                string strURL = ConfigurationManager.AppSettings["BaseUrlService"] + "api/ComisionesxConsulta/ObtenerNombreCliente";
                string response = WebApi<ObtenerNombreClienteRequest>.RequestWebApi(request, strURL);
                contenidoResponse = JsonConvert.DeserializeObject<ObtenerNombreClienteResponse>(response);
            }
            catch (Exception ex)
            {
                contenidoResponse.Result = false;
            }
            return Json(contenidoResponse);
        }
    }
}