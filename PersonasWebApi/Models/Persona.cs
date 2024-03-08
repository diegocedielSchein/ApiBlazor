using System.ComponentModel.DataAnnotations;

namespace PersonasWebApi.Models
{
    public class Persona
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string TipoIdentificacion { get; set; }

        [Required]
        public int Identificacion { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Apellido { get; set; }

        [Required]
        public string Estado { get; set; }

        [Required]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        public uint Celular { get; set; }

        [Required]
        public string Correo { get; set; }

    }
}
