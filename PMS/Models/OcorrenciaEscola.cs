using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMS.Models
{
    public class OcorrenciaEscola
    {
        [Key]
        public int OcorrenciaSecId { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string logradouro { get; set; }
        public string numero { get; set; }
        public string bairro { get; set; }
        public string cep { get; set; }
        public string cidade { get; set; }
        public string estado { get; set; }
        public DateTime Data { get; set; }

        public int TipoOcorrenciaEscolaId { get; set; }
        public virtual TipoOcorrenciaEscola TipoOcorrenciaEscola { get; set; }

        public int DescricaoOcorrenciaEscolaId { get; set; }
        public virtual DescricaoOcorrenciaEscola DescricaoOcorrenciaEscola { get; set; }

        public int StatusOcorrenciaEscolaId { get; set; }
        public virtual StatusOcorrenciaEscola StatusOcorrenciaEscola { get; set; }

        public int EscolaId { get; set; }
        public virtual Escola Escola { get; set; }
    }
}