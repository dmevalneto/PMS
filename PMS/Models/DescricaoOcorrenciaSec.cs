using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMS.Models
{
    public class DescricaoOcorrenciaSec
    {
        [Key]
        public int DescricaoOcorrenciaSecId { get; set; }
        public string Descricao { get; set; }
        public string Observacao { get; set; }

        public virtual ICollection<OcorrenciaSec> OcorrenciaSec { get; set; }
    }
}