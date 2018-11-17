using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMS.Models
{
    public class HistoricoStatusOcorrenciaSec
    {
        [Key]
        public int HistoricoStatusOcorrenciaSecId { get; set; }
        public DateTime Data { get; set; }
        public string Observacao { get; set; }

        public int StatusOcorrenciaSecId { get; set; }
        public virtual StatusOcorrenciaSec StatusOcorrenciaSec { get; set; }

        public int OcorrenciaSecId { get; set; }
        public virtual OcorrenciaSec OcorrenciaSec { get; set; }
    }
}