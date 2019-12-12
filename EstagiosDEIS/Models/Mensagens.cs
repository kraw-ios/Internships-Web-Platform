using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EstagiosDEIS.Models
{
    [Table("Mensagens")]
    public class Mensagens
    {

        public int NumMensagem { get; set; }

        public String Remetente { get; set; }

        public String Destinatario { get; set; }

        [DataType(DataType.MultilineText)]
        public String Mensagem { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DataMensagem { get; set; }
    }
}