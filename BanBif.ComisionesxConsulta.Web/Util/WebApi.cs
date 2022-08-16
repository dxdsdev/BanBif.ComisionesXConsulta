using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace BanBif.ComisionesxConsulta.Web.Util
{
    public class WebApi<TEntity>
    {
        public static string RequestWebApi(TEntity oBE, string URL)
        {
            string JSON = string.Empty;
            try
            {
                var jsonRequest = string.Empty;
                var webAddr = URL;
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    jsonRequest = JsonConvert.SerializeObject(oBE);
                    streamWriter.Write(jsonRequest);
                    streamWriter.Flush();
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    JSON = streamReader.ReadToEnd();
                }

            }
            catch (Exception ex)
            {

                JSON = string.Empty;
            }
            return JSON;
        }
    }
}