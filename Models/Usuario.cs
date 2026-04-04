using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TallerMecanico.Models
{
    [Table("usuarios")]
    public class Usuario
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("username")]
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [Column("password")]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Column("rol")]
        [StringLength(30)]
        public string Rol { get; set; } = string.Empty;
    }
}
