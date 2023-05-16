using DB;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace ServicioTransferencias.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferenciasController : Controller
    {
        private TransferenciasContext _context;
        public TransferenciasController(TransferenciasContext context) { _context = context; }

        [HttpGet]
        [Route("Transferencias")]
        public ActionResult Transferencias()
        {
            try
            {
                IEnumerable<Transferencia> lista = _context.Transferencias.ToList();
                return Ok(lista);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("Respuestas")]
        public ActionResult Respuestas()
        {
            try
            {
                IEnumerable<Respuesta> lista = _context.Respuestas.ToList();
                return Ok(lista);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("Transferir")]
        public ActionResult Transferir(Transferencia transfer)
        {
            using var client = new HttpClient();

            HttpResponseMessage result = client.GetAsync($"http://localhost:5092/Clientes/{transfer.cuilOriginante}").GetAwaiter().GetResult();
            //var result = await client.GetAsync($"http://localhost:5092/Clientes/{transfer.cuilOriginante}")

            transfer.respuesta = new Respuesta()
            {
                cbuDestino = transfer.cbuDestino,
                cbuOrigen = transfer.cbuOrigen,
                importe = transfer.importe,
            };

            if (result.IsSuccessStatusCode)
            {
                transfer.respuesta.resultado = "ACEPTADA";
                _context.Transferencias.Add(transfer);
                return Ok(transfer);
            }
            else
            {
                transfer.respuesta.resultado = "ERRONEA";
                _context.Transferencias.Add(transfer);
                return NotFound(transfer);
            }

        }
    }
}
