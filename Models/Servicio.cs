using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TallerMecanico.Models
{
    [Table("servicios")]
    public class Servicio
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Column("precio")]
        public decimal Precio { get; set; }

        public List<DetalleOrden>? Detalles { get; set; }
    }
}
