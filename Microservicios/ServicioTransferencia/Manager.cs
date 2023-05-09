using Azure;
using Common;

namespace ServicioTransferencia
{
    public static class Manager
    {
        public static async Task<bool> validarCuitBna(string cuilOriginante, string cuilDestinatario)
        {
            bool validado = false;
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("http://localhost:5092/Clientes/cuit/{cuilOriginante}");
            string tpe = response.GetType().ToString();
            if (response.IsSuccessStatusCode) { validado = true; }
            response = await client.GetAsync("http://localhost:5092/Clientes/cuit/{cuilOriginante}");
            if (response.IsSuccessStatusCode) { validado = true; }

            return validado;

        }

    }
}
