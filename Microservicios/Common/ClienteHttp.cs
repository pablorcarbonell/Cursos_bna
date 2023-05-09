using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ClienteHttp
    {

        public static HttpClient _Client;


        public static int REINTENTOS = 1;

    
        public static HttpResponseMessage GetResponse(string urlBase, string query, HttpMethod method)
        {
            //Logger.LogDebug("enviando get: " + DateTime.Now.ToString());
            if (_Client == null) InitHttpClient(urlBase);
            HttpResponseMessage response = null;

            response = ClienteHttp.RunRequest(urlBase, query, method);

            return response;
        }



        private static void InitHttpClient(string urlBase)
        {
            //Logger.LogDebug("Inicializando cliente Http");
            try
            {
                //_Client = new HttpClient(new HttpClientHandler() { UseDefaultCredentials = true });
                var handler = new HttpClientHandler()
                {
                    UseDefaultCredentials = true,
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };

                _Client = new HttpClient(handler, false);
                _Client.Timeout = TimeSpan.FromMilliseconds(15000);
                _Client.DefaultRequestHeaders.Clear();
                _Client.DefaultRequestHeaders.Accept.Clear();
                _Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //_Client.BaseAddress = new Uri(@"http://localhost:44384/api/");
                _Client.BaseAddress = new Uri(urlBase);
            }
            catch (Exception ex)
            {
                //Logger.LogError("Error al cargar la configuracion del HttpClient en RestApi. Revise la configuracion de comunicacion. " + ex.Message);
                throw;
            }
        }

        public static HttpResponseMessage RunRequest(string urlBase, string url, HttpMethod method, object content = null)
        {
            if (_Client == null) { InitHttpClient(urlBase); }

            _Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer");

            HttpResponseMessage response = null;
            for (int i = 1; i <= REINTENTOS; i++)
            {
                //Logger.LogDebug($"ApiBCRA - {method.Method} {url}");
                try
                {
                    var request = new HttpRequestMessage(method, url);
                    if (content != null)
                    {
                        var variable = JsonConvert.SerializeObject(content);
                        request.Content = new StringContent(JsonConvert.SerializeObject(content));
                        request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    }
                    response = _Client.Send(request);
                    break;
                }
                catch (Exception ex)
                {
                    if (i < REINTENTOS)
                    {
                        //Logger.LogInfo("Intento de Request falló. Mensaje: " + ex.Message + " - Reintentando...");
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return response;
        }

        public static HttpResponseMessage Get(string urlBase, string query)
        {
            return GetResponse(urlBase, query, HttpMethod.Get);
        }

        public static HttpResponseMessage Post(string urlBase, string query, object content)
        {
            return RunRequest(urlBase, query, HttpMethod.Post, content);
        }

    }
}
