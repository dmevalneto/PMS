using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMS.Models
{
    public class StatusOcorrenciaGre
    {
        [Key]
        public int StatusOcorrenciaGreId { get; set; }
        public string Status { get; set; }
        public string Observacao { get; set; }

        public virtual ICollection<OcorrenciaGre> OcorrenciaGre { get; set; }
    }
}