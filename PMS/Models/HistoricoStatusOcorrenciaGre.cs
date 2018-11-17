using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMS.Models
{
    public class HistoricoStatusOcorrenciaGre
    {
        [Key]
        public int HistoricoStatusOcorrenciaGreId { get; set; }
        public DateTime Data { get; set; }
        public string Observacao { get; set; }

        public int StatusOcorrenciaGreId { get; set; }
        public virtual StatusOcorrenciaGre StatusOcorrenciaGre { get; set; }

        public int OcorrenciaGreId { get; set; }
        public virtual OcorrenciaGre OcorrenciaGre { get; set; }
    }
}