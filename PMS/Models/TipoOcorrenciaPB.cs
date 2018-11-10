using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMS.Models
{
    public class TipoOcorrenciaPB
    {
        [Key]
        public int TipoOcorrenciaPBId { get; set; }
        public string Tipo { get; set; }
        public string Observacao { get; set; }

        public virtual ICollection<OcorrenciaPB> OcorrenciaPB { get; set; }
    }
}