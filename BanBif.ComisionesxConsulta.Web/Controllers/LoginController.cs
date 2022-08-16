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
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            ViewBag.URL = ConfigurationManager.AppSettings.Get("UrlApp").ToString();
            return View();
        }

        public ActionResult ValidarLogin(ObtenerLoginRequest request)
        {
            ObtenerLoginResponse loginResponse = new ObtenerLoginResponse();

            try
            {
                string strURL = ConfigurationManager.AppSettings["BaseUrlService"] + "api/ComisionesxConsulta/ObtenerLogin";
                string response = WebApi<ObtenerLoginRequest>.RequestWebApi(request, strURL);
                loginResponse = JsonConvert.DeserializeObject<ObtenerLoginResponse>(response);
            }
            catch (Exception ex)
            {
                loginResponse.Result = false;
            }
            return Json(loginResponse);
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
    }
}