using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TallerMecanico.Models
{
    [Table("ordenes_trabajo")]
    public class OrdenTrabajo
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("fecha")]
        public DateTime Fecha { get; set; }

        [Required]
        [Column("estado")]
        public string Estado { get; set; } = string.Empty;

        [Column("cliente_id")]
        public int ClienteId { get; set; }

        [Column("vehiculo_id")]
        public int VehiculoId { get; set; }

        [ForeignKey(nameof(ClienteId))]
        public Cliente? Cliente { get; set; }

        [ForeignKey(nameof(VehiculoId))]
        public Vehiculo? Vehiculo { get; set; }

        public List<DetalleOrden>? Detalles { get; set; }
    }
}
