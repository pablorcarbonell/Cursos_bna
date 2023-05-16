using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public class Respuesta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int respuestaId { get; set; }
        public string resultado { get; set; }
        public string cbuOrigen { get; set; }
        public string cbuDestino { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal importe { get; set; }   
    }
}
