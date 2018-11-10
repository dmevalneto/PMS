using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMS.Models
{
    public class Terceirizado
    {
        [Key]
        public int TerceirizadoId { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataAdmissao { get; set; }
        public string Telefone { get; set; }

        public int PrestadoraServicoId { get; set; }
        public virtual PrestadoraServico PrestadoraServico { get; set; }

        public int CargoId { get; set; }
        public virtual Cargo Cargo { get; set; }

        public int OcorrenciaId { get; set; }
        public virtual Ocorrencia Ocorrencia { get; set; }

        public virtual ICollection<Encaminhamento> Encaminhamento { get; set; }


    }
}