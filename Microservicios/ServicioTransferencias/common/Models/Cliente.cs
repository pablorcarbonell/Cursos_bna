using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Common.Models
{
    public class Cliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string? nombre { get; set; }
        public string? apellido { get; set; }
        public string? cuil { get; set; }
        public string? tipoDocumento { get; set; }
        public int nroDocumento { get; set; }
        public bool esEmpleadoBNA { get; set; }
        public string? paisOrigen { get; set; }

    }
}