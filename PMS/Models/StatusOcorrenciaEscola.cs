using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMS.Models
{
    public class StatusOcorrenciaEscola
    {
        [Key]
        public int StatusOcorrenciaEscolaId { get; set; }
        public string Status { get; set; }
        public string Observacao { get; set; }
        public virtual ICollection<HistoricoStatusOcorrenciaEscola> HistoricoStatusOcorrenciaEscola { get; set; }
    }
}