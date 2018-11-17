using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMS.Models
{
    public class OcorrenciaGre
    {
        [Key]
        public int OcorrenciaGreId { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string logradouro { get; set; }
        public string numero { get; set; }
        public string bairro { get; set; }
        public string cep { get; set; }
        public string cidade { get; set; }
        public string estado { get; set; }
        public DateTime Data { get; set; }

        public int TipoOcorrenciaGreId { get; set; }
        public virtual TipoOcorrenciaGre TipoOcorrenciaGre { get; set; }

        public int DescricaoOcorrenciaGreId { get; set; }
        public virtual DescricaoOcorrenciaGre DescricaoOcorrenciaGre { get; set; }



        public int GreId { get; set; }
        public virtual Gre Gre { get; set; }

        public virtual ICollection<HistoricoStatusOcorrenciaGre> HistoricoStatusOcorrenciaGre { get; set; }
    }
}