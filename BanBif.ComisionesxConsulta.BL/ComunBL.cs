using BanBif.ComisionesxConsulta.BE;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BanBif.ComisionesxConsulta.BL
{
   public class ComunBL
    {
        public CorreoResponse EnviarCorreo(CorreoRequest request)
        {
            string apiUrl = "http://10.200.101.89:8084/api/Correo/EnviarCorreo";
            //string apiUrl = "https://wsinnovaapp.bif1innova10.com/api/Correo/EnviarCorreo";

            var result = new HttpResponseMessage();

            using (var client = new HttpClient())
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                var jsonObject = JsonConvert.SerializeObject(request);
                var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                result = client.PostAsync(apiUrl, content).Result;

                var dataObjects = "";

                dataObjects = result.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<CorreoResponse>(dataObjects);
                return response;
            }
        }
    }
}
