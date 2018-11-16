using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMS.Models
{
    public class TipoOcorrenciaEscola
    {
        [Key]
        public int TipoOcorrenciaEscolaId { get; set; }
        public string Tipo { get; set; }
        public string Observacao { get; set; }

        public virtual ICollection<OcorrenciaEscola> OcorrenciaEscola { get; set; }

    }
}