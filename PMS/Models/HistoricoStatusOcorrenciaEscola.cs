using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMS.Models
{
    public class HistoricoStatusOcorrenciaEscola
    {
        [Key]
        public int HistoricoStatusOcorrenciaEscolaId { get; set; }
        public DateTime Data { get; set; }
        public string Observacao { get; set; }

        public int StatusOcorrenciaEscolaId { get; set; }
        public virtual StatusOcorrenciaEscola StatusOcorrenciaEscola { get; set; }

        public int OcorrenciaEscolaId { get; set; }
        public virtual OcorrenciaEscola OcorrenciaEscola { get; set; }
    }
}