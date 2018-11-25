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
        [Required(ErrorMessage = "Campo requerido")]
        public string Nome { get; set; }
        public string Cpf { get; set; }


        //[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime DataAdmissao { get; set; }
        
        public string Telefone { get; set; }

        public int TipoLotacao { get; set; }
        public int Lotacao { get; set; }

        public int PrestadoraServicoId { get; set; }
        public virtual PrestadoraServico PrestadoraServico { get; set; }

        public int CargoId { get; set; }
        public virtual Cargo Cargo { get; set; }

        public int OcorrenciaId { get; set; }
        public virtual Ocorrencia Ocorrencia { get; set; }

        public virtual ICollection<Encaminhamento> Encaminhamento { get; set; }
    }
}