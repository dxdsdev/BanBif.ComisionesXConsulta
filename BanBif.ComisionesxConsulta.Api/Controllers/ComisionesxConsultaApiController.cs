using BanBif.ComisionesxConsulta.BE;
using BanBif.ComisionesxConsulta.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BanBif.ComisionesxConsulta.Api.Controllers
{
    public class ComisionesxConsultaApiController : ApiController
    {

        #region GLOBALES

        [Route("api/ComisionesxConsulta/ObtenerNombreCliente")]
        [HttpPost]
        public IHttpActionResult ObtenerNombreCliente(ObtenerNombreClienteRequest request)
        {
            var oBL = new ComisionesxConsultaBL();
            return Json(oBL.ObtenerNombreCliente(request));
        }
        #endregion

        #region PANTALLA LOGIN
        [Route("api/ComisionesxConsulta/ObtenerLogin")]
        [HttpPost]
        public IHttpActionResult ObtenerLogin(ObtenerLoginRequest request)
        {
            var oBL = new ComisionesxConsultaBL();
            return Json(oBL.ObtenerLogin(request));
        }
        #endregion

        #region CLIENTES
        [Route("api/ComisionesxConsulta/ObtenerDatosClientes")]
        [HttpPost]
        public IHttpActionResult ObtenerDatosClientes(ObtenerDatosClientesRequest request)
        {
            var oBL = new ComisionesxConsultaBL();
            return Json(oBL.ObtenerDatosClientes(request));
        }



        #endregion

        #region TOKEN
        [Route("api/ComisionesxConsulta/EnviarToken")]
        [HttpPost]
        public IHttpActionResult EnviarToken(EnviarRequest request)
        {
            var oBL = new ComisionesxConsultaBL();
            return Json(oBL.EnviarToken(request));
        }

        [Route("api/ComisionesxConsulta/ConfirmarToken")]
        [HttpPost]
        public IHttpActionResult ConfirmarToken(ConfirmarTokenRequest request)
        {
            var oBL = new ComisionesxConsultaBL();
            return Json(oBL.ConfirmarToken(request));
        }

        #endregion

        #region CUENTA CLIENTE
        [Route("api/ComisionesxConsulta/ListarNroCuenta")]
        [HttpPost]
        public IHttpActionResult ListarNroCuenta(ObtenerNroCuentaRequest request)
        {
            var oBL = new ComisionesxConsultaBL();
            return Json(oBL.ListarNroCuenta(request));
        }

        #endregion

        #region UBIGEO
        [Route("api/ComisionesxConsulta/ListarDistritos")]
        [HttpPost]
        public IHttpActionResult ListarDistritos()
        {
            var oBL = new ComisionesxConsultaBL();
            return Json(oBL.ListarDistritos());
        }

        [Route("api/ComisionesxConsulta/ListarOficina")]
        [HttpPost]
        public IHttpActionResult ListarOficina(ListarOficinaRequest request)
        {
            var oBL = new ComisionesxConsultaBL();
            return Json(oBL.ListarOficina(request));
        }
        #endregion

        #region CONSULTA
        [Route("api/ComisionesxConsulta/GuardarConsulta")]
        [HttpPost]
        public IHttpActionResult GuardarConsulta(ConsultaRequest request)
        {
            var oBL = new ComisionesxConsultaBL();
            return Json(oBL.GuardarConsulta(request));
        }
        #endregion

        #region ANIOS
        [Route("api/ComisionesxConsulta/ObtenerAnios")]
        [HttpPost]
        public IHttpActionResult ObtenerAnios(ObtenerAniosRequest request)
        {
            var oBL = new ComisionesxConsultaBL();
            return Json(oBL.ObtenerAnios(request));
        }

        #endregion

        #region CONSULTA ANIOS
        [Route("api/ComisionesxConsulta/ObtenerConsultaAnio")]
        [HttpPost]
        public IHttpActionResult ObtenerConsultaAnio(ObtenerConsultaAnioRequest request)
        {
            var oBL = new ComisionesxConsultaBL();
            return Json(oBL.ObtenerConsultaAnio(request));
        }

        #endregion
    }
}
