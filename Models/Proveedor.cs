using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TallerMecanico.Models
{
    [Table("proveedores")]
    public class Proveedor
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("nombre")]
        public string Nombre { get; set; }

        [Column("telefono")]
        public string Telefono { get; set; }

        public List<Repuesto>? Repuestos { get; set; }
    }
}
