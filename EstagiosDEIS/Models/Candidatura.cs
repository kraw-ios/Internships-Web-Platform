using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EstagiosDEIS.Models
{   
    public enum Estado
    {
        Aceite,
        EsperaResultado,
        PrecisaEntrevista,
        Rejeitado
    }

    [Table("Candidaturas")]
    public class Candidatura
    {
        public Ramo Ramo { get; set; }
        public Aluno Candidato { get; set; }
        public Estado Estado { get; set; }
        [ForeignKey("Candidato")]
        public int NumCandidatura { get; set; }
        public String LocalPreferencia { get; set; }
        public String NomeOrientador { get; set; }
        public int NumAluno { get; set; }
        public int NumeroOrientador { get; set; }
        public int NumeroDisciplinasPorConcluir { get; set; }
        public float MediaCurso { get; set; }
        //public IList<Disciplina> DisciplinasConcluidas { get; set; }
        //public IList<Disciplina> DisciplinasPorConcluir { get; set; }
        public DateTime DataDeCandidatura { get; set; }
        public IList<Proposta> PropostasEscolhidas { get; set; }

        [Display(Name = "Selecione a Proposta")]
        public string PropostasSelect { get; set; }

        public Candidatura() {
        }
    }
}