using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMS.Models
{
    public class DescricaoOcorrenciaPB
    {
        [Key]
        public int DescricaoOcorrenciaPBId { get; set; }
        public string Descricao { get; set; }
        public string Observacao { get; set; }

        public virtual ICollection<OcorrenciaPB> OcorrenciaPB { get; set; }
    }
}