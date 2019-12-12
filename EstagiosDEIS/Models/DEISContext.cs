using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace EstagiosDEIS.Models
{
    public class DEISContext : ApplicationDbContext
    {

        public DEISContext() /*: base("name=DefaultConnection")*/{
        }

        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Professor> Professores { get; set; }
        public DbSet<Proposta> Propostas { get; set; }
        public DbSet<Candidatura> Candidaturas { get; set; }
        public DbSet<Mensagens> Mensagens { get; set; }
        public DbSet<CandidaturaEPropostas> CandidaturasPropostas{get; set;}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Aluno>().HasKey(c => c.NumeroAluno);
            modelBuilder.Entity<Empresa>().HasKey(c => c.IDEmpresa);
            modelBuilder.Entity<Professor>().HasKey(c => c.NumeroProfessor);
            modelBuilder.Entity<Proposta>().HasKey(c => c.NumProposta).HasMany<Candidatura>(c => c.CandidaturasAssociadas).WithMany(p => p.PropostasEscolhidas)
                .Map(cp => { cp.MapLeftKey("PropostaRefId"); cp.MapRightKey("CandidaturaRefId"); cp.ToTable("PropostaCandidatura"); });
            modelBuilder.Entity<Candidatura>().HasKey(c => c.NumCandidatura); //.HasMany<Proposta>(c => c.PropostasEscolhidas).WithMany(p => p.CandidaturasAssociadas);
            modelBuilder.Entity<Mensagens>().HasKey(c => c.NumMensagem);
            modelBuilder.Entity<CandidaturaEPropostas>().HasKey(c => c.id);
        }
    }
}