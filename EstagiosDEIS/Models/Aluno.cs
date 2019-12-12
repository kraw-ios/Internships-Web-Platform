using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EstagiosDEIS.Models
{
    [Table("Alunos")]
    public class Aluno
    {
        
        public int NumeroAluno { get; set; }
        public String NomeAluno { get; set; }
        //[ForeignKey("Proposta")]
        public int NumProposta { get; set; }
        public Proposta Proposta { get; set; }
        public int NumCandidatura { get; set; }
        public Candidatura Candidatura { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public Aluno(int numero, String nome)
        {
            this.NumeroAluno = numero;
            this.NomeAluno = nome;
        }

        public Aluno() { }
    }
}