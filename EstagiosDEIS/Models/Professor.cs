using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EstagiosDEIS.Models
{
    [Table("Professores")]
    public class Professor
    {
        public int NumeroProfessor { get; set; }
        public String Nome { get; set; }
        public Boolean ComissaoEstagio { get; set; }
        public IList<Proposta> OrientadorPropostas { get; set; }
        public IList<Proposta> PropostasSubmetidas { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public Professor(){

        }
    }
}