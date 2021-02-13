using System.ComponentModel.DataAnnotations;

namespace TIEvol.Shared.Entities
{
    public class Comuna
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Nombre { get; set; }
        
        [Required]
        public string Codigo { get; set; }

        public int Id_Ciudad { get; set; }

        public Ciudad Ciudad { get; set; }
    }
}