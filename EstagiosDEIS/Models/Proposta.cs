using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EstagiosDEIS.Models
{
    public enum EstadoEstagio {
        PorComeçar,
        ADecorrer,
        Terminado
    }

    [Table("Propostas")]
    public class Proposta
    {


        public Aluno Estagiario { get; set; }
        //public int NumeroAluno { get; set; }
        [ForeignKey("Estagiario")]
        public int NumProposta { get; set; }
        public Ramo RamoAluno { get; set; }
        [DataType(DataType.MultilineText)]
        public String Enquadramento { get; set; }
        [DataType(DataType.MultilineText)]
        public String Objetivos { get; set; }
        [DataType(DataType.MultilineText)]
        public String CondicoesAcesso { get; set; }
        public String LocalProposta { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "(dd/MM/yyyy)", ApplyFormatInEditMode = true)]
        public DateTime? DataInicio { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "(dd/MM/yyyy)", ApplyFormatInEditMode = true)]
        public DateTime? DataFim { get; set; }
        public int NumOrientador { get; set; }
        public String NomeOrientador { get; set; }
        public IList<String> SubmetidoPor { get; set; }
        public float NotaAluno { get; set; }
        public float NotaEmpresa { get; set; }

        public String AdicionadoPor { get; set; }

        public String NomeDaEmpresa { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "(0:dd/MM/yyyy)", ApplyFormatInEditMode = true)]
        public DateTime? DataDefesa { get; set; }
        public IList<Candidatura> CandidaturasAssociadas { get; set; }

        public Proposta() {
        }


    }
}