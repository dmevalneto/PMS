using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMS.Models
{
    public class TipoOcorrenciaSec
    {
        [Key]
        public int TipoOcorrenciaSecId { get; set; }
        public string Tipo { get; set; }
        public string Observacao { get; set; }

        public virtual ICollection<OcorrenciaSec> OcorrenciaSec { get; set; }
    }
}