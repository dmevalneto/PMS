using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMS.Models
{
    public class DescricaoOcorrenciaEscola
    {
        [Key]
        public int DescricaoOcorrenciaEscolaId { get; set; }
        public string Descricao { get; set; }
        public string Observacao { get; set; }

        public virtual ICollection<OcorrenciaEscola> OcorrenciaEscola { get; set; }
    }
}