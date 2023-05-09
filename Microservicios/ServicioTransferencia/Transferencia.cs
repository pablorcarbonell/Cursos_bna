using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ServicioTransferencia
{
    public class Transferencia
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string? cuilOriginante { get; set; }
        public string? cuilDestinatario { get; set; }
        public string? cbuOrigen { get; set; }
        public string? cbuDestino { get; set; }
        public double importe { get; set; }
        public string? concepto { get; set; }
        public string? descripcion { get; set; }
        public Respuesta respuesta { get; set; } = new Respuesta();
    }

    public class Respuesta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int? idTransferencia { get; set; }
        public string? resultado { get; set; }
        public double importe { get; set; }
        public string? cuentaOrigen { get; set; }
        public string? cuentaDestino { get; set; }
    }

}
