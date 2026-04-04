using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TallerMecanico.Models
{
    [Table("vehiculos")]
    public class Vehiculo
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("marca")]
        public string Marca { get; set; }

        [Column("modelo")]
        public string Modelo { get; set; }

        [Required]
        [Column("placa")]
        public string Placa { get; set; }

        [Column("cliente_id")]
        public int ClienteId { get; set; }

        [ForeignKey("ClienteId")]
        public Cliente? Cliente { get; set; }

        public List<OrdenTrabajo>? Ordenes { get; set; }
    }
}