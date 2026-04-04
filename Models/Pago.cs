using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TallerMecanico.Models
{
    [Table("pagos")]
    public class Pago
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("factura_id")]
        public int FacturaId { get; set; }

        [Column("monto")]
        public decimal Monto { get; set; }

        [Required]
        [Column("metodo")]
        public string Metodo { get; set; }

        [Column("fecha")]
        public DateTime Fecha { get; set; }

        [ForeignKey(nameof(FacturaId))]
        public Facturacion? Factura { get; set; }
    }
}
