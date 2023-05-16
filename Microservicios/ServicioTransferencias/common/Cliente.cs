using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace common
    
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