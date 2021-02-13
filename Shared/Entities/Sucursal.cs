using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TIEvol.Shared.Entities
{
    public class Sucursal
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(50)]
        public int Nombre { get; set; }
        
        [Required]
        [MaxLength(50)]
        public int Direccion { get; set; }
        
        // [RegularExpression(@"")]
        [Required]
        public int Fono { get; set; }
        
        [Required]
        public int Id_Comuna { get; set; }
    }
}
