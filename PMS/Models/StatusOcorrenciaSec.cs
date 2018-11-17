using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMS.Models
{
    public class StatusOcorrenciaSec
    {
        [Key]
        public int StatusOcorrenciaSecId { get; set; }
        public string Status { get; set; }
        public string Observacao { get; set; }
        public virtual ICollection<HistoricoStatusOcorrenciaSec> HistoricoStatusOcorrenciaSec { get; set; }
    }
}