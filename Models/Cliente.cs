using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TallerMecanico.Models
{
    [Table("clientes")]
    public class Cliente
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("nombre")]
        public string Nombre { get; set; }

        [Column("telefono")]
        public string Telefono { get; set; }

        [Column("email")]
        public string Email { get; set; }

        public List<Vehiculo>? Vehiculos { get; set; }
        public List<OrdenTrabajo>? Ordenes { get; set; }
    }
}