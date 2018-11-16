using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMS.Models
{
    public class OcorrenciaSec
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

        public int TipoOcorrenciaSecId { get; set; }
        public virtual TipoOcorrenciaSec TipoOcorrenciaSec { get; set; }

        public int DescricaoOcorrenciaSecId { get; set; }
        public virtual DescricaoOcorrenciaSec DescricaoOcorrenciaSec { get; set; }

        public int StatusOcorrenciaSecId { get; set; }
        public virtual StatusOcorrenciaSec StatusOcorrenciaSec { get; set; }

        public int SecretariaId { get; set; }
        public virtual Secretaria Secretaria { get; set; }
    }
}