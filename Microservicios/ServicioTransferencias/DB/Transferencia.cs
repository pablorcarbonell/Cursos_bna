using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public class Transferencia
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int transferenciaId { get; set; }

        public string cuilOriginante { get; set; }
        public string cuilDestinatario { get; set; }
        public string cbuOrigen { get; set; }
        public string cbuDestino { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal importe { get; set; }
        public string concepto { get; set; }
        public string descripcion { get; set; }
        
        public int? respuestaId { get; set; }
        [ForeignKey("respuestaId")]
        public virtual Respuesta? respuesta { get; set; }= new Respuesta();

    }
}
