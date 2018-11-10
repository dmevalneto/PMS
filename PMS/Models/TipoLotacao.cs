using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMS.Models
{
    public class TipoLotacao
    {
        [Key]
        public int TipoLotacaoId { get; set; }
        public string Nome { get; set; }
        public string Observacao { get; set; }


        public virtual ICollection<Encaminhamento> Encaminhamento { get; set; }
    }
}