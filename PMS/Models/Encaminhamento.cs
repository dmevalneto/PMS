using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMS.Models
{
    public class Encaminhamento
    {
        [Key]
        public int EncaminhamentoId { get; set; }

        public int TerceirizadoId { get; set; }
        public virtual Terceirizado Terceirizado { get; set; }

        public DateTime Data { get; set; }

        public int TipoLotacaoId { get; set; }
        public virtual TipoLotacao TipoLotacao { get; set; }

        public int LotacaoId { get; set; }
        
    }
}