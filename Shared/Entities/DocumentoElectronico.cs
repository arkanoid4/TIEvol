using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TIEvol.Shared.Enum;
using TIEvol.Shared.Enums;

namespace TIEvol.Shared.Entities
{
    class DocumentoElectronico
    {
        [Key]
        public int NumeroIdentificador { get; set; }

        [Required]
        public TipoDocumento TipoDocumento { get; set; }

        [Required]
        public DateTime FechaEmision { get; set; }

        public DateTime FechaVencimiento { get; set; }

        [Required]
        public MetodoPago MetodoPago { get; set; }

        public string TimbreElectronico { get; set; }

    }
}
