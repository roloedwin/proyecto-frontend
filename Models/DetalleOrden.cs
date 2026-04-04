using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TallerMecanico.Models
{
    [Table("detalle_orden")]
    public class DetalleOrden
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("orden_id")]
        public int OrdenId { get; set; }

        [Column("servicio_id")]
        public int ServicioId { get; set; }

        [Column("cantidad")]
        public int Cantidad { get; set; }

        [ForeignKey(nameof(OrdenId))]
        public OrdenTrabajo? Orden { get; set; }

        [ForeignKey(nameof(ServicioId))]
        public Servicio? Servicio { get; set; }
    }
}
