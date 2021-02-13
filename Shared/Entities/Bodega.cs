using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TIEvol.Shared.Entities
{
    public class Bodega
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string Codigo { get; set; }
        
        public int Id_Sucursal { get; set; }

        public Sucursal Sucursal { get; set; }
    }
}
