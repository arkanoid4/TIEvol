using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TIEvol.Shared.Entities
{
    public class Sucursal
    {   
        public int Id { get; set; }
        
        [Required]
        public string Nombre { get; set; }
        
        
        public string Direccion { get; set; }
        
        
        public string Fono { get; set; }
        
        
        public int Id_Comuna { get; set; }
    }
}
