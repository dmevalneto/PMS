using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMS.Models
{
    public class StatusOcorrenciaPB
    {
        [Key]
        public int StatusOcorrenciaPBId { get; set; }
        public string Status { get; set; }
        public string Observacao { get; set; }
        public virtual ICollection<HistoricoStatusOcorrenciaPB> HistoricoStatusOcorrenciaPB { get; set; }
    }
}