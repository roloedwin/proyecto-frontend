using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TallerMecanico.Models
{
    [Table("empleados")]
    public class Empleado
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [Column("cargo")]
        public string Cargo { get; set; } = string.Empty;

        [NotMapped]
        public List<OrdenTrabajo>? Ordenes { get; set; }
    }
}
