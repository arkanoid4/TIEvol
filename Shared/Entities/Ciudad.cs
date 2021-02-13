using System.ComponentModel.DataAnnotations;

namespace TIEvol.Shared.Entities
{
    public class Ciudad
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }
    }
}
