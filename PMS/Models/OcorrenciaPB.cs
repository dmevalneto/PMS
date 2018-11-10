using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMS.Models
{
    public class OcorrenciaPB
    {
        [Key]
        public int OcorrenciaPBId { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string logradouro { get; set; }
        public string numero { get; set; }
        public string bairro { get; set; }
        public string cep { get; set; }
        public string cidade { get; set; }
        public string estado { get; set; }
        public string foto { get; set; }
        public DateTime Data { get; set; }


        public int TipoOcorrenciaPBId { get; set; }
        public virtual TipoOcorrenciaPB TipoOcorrenciaPB { get; set; }

        public int DescricaoOcorrenciaPBId { get; set; }
        public virtual DescricaoOcorrenciaPB DescricaoOcorrenciaPB { get; set; }

        public int PrefeituraBairroId { get; set; }
        public virtual PrefeituraBairro PrefeituraBairro { get; set; }
    }
}