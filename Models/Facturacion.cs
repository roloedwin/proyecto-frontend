using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TallerMecanico.Models
{
    [Table("facturacion")]
    public class Facturacion
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("orden_id")]
        public int OrdenId { get; set; }

        [Column("total")]
        public decimal Total { get; set; }

        [ForeignKey(nameof(OrdenId))]
        public OrdenTrabajo? Orden { get; set; }

        public List<Pago>? Pagos { get; set; }
    }
}
