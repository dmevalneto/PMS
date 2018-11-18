using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMS.Models
{
    public class HistoricoStatusOcorrenciaPB
    {
        [Key]
        public int HistoricoStatusOcorrenciaPBId { get; set; }
        public DateTime Data { get; set; }
        public string Observacao { get; set; }

        public int StatusOcorrenciaPBId { get; set; }
        public virtual StatusOcorrenciaPB StatusOcorrenciaPB { get; set; }

        public int OcorrenciaPBId { get; set; }
        public virtual OcorrenciaPB OcorrenciaPB { get; set; }
    }
}