using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TallerMecanico.Models
{
    [Table("repuestos")]
    public class Repuesto
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("nombre")]
        public string Nombre { get; set; }

        [Column("precio")]
        public decimal Precio { get; set; }

        [Column("proveedor_id")]
        public int ProveedorId { get; set; }

        [ForeignKey(nameof(ProveedorId))]
        public Proveedor? Proveedor { get; set; }

        [NotMapped]
        public List<DetalleOrden>? Detalles { get; set; }
    }
}
