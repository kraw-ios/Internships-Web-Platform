using EstagiosDEIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EstagiosDEIS.Controllers
{
    public class CandidaturasController : Controller
    {
        private DEISContext context = new DEISContext();
        public List<Proposta> listaPropostas { get; set; } = new List<Proposta>();

        //public IList<String> nomePropostas { get; set; }
       // private int idCandidatura;



        // GET: Candidaturas
        [Authorize(Roles = "Aluno")]
        [HttpGet]
        public ActionResult Candidatar()
        {
            /*CandidaturaEPropostas model = new CandidaturaEPropostas();
            model.Propostas = context.Propostas;
            model.Candidatura = new Candidatura();
            model.Ids = new List<int>();*/
            //Candidatura candidatura = new Candidatura();
            //var tuple = new Tuple<Candidatura, IEnumerable<Proposta>> (candidatura, context.Propostas.OrderBy(x => x.NumProposta));

            return View(new Candidatura());
        }

        [Authorize(Roles = "Aluno")]
        [HttpPost]
        public ActionResult Candidatar(Candidatura candidatura)
        {
            if (candidatura == null /*|| candidatura.Ids.Capacity <= 0*/) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid) {
            var numID = 0;
                if (context.Candidaturas.Count() > 0)
                {
                    numID = context.Candidaturas.OrderByDescending(x => x.NumCandidatura).FirstOrDefault().NumCandidatura;
                }

                //List<Proposta> listaPropostas = new List<Proposta>();
                //var prop = new Proposta();

                /* for (int i = 0; i < candidatura.Ids.Capacity; i++) {
                     prop = context.Propostas.Single(o => o.NumProposta == candidatura.Ids[i]);
                     listaPropostas.Add(prop);
                 }*/
                if (numID == 0)
                    numID = 2;
                else
                    numID += 1;
                Candidatura cand = new Candidatura()
                {
                    NumCandidatura = numID,
                    NumAluno = context.Alunos.Where(x => x.NomeAluno.Equals(User.Identity.Name)).FirstOrDefault().NumeroAluno,
                    Ramo = candidatura.Ramo,
                    Estado = Estado.EsperaResultado,
                    NumeroDisciplinasPorConcluir = candidatura.NumeroDisciplinasPorConcluir,
                    MediaCurso = candidatura.MediaCurso,
                    Candidato = context.Alunos.Single(x => x.NomeAluno.Equals(User.Identity.Name)),
                    DataDeCandidatura = DateTime.Now,
                    PropostasEscolhidas = listaPropostas

                };


                foreach (var item in listaPropostas) {
                    var numCP = 0;
                    if (context.Candidaturas.Count() > 0)
                    {
                        numCP = context.CandidaturasPropostas.OrderByDescending(x => x.id).FirstOrDefault().id;
                    }
                    CandidaturaEPropostas cp = new CandidaturaEPropostas()
                    {
                        id = numCP + 1,
                        numCandidatura = cand.NumCandidatura,
                        numProposta = item.NumProposta
                    };
                    context.CandidaturasPropostas.Add(cp);
                    context.SaveChanges();
                }
                Aluno aluno = context.Alunos.Single(x => x.NomeAluno.Equals(User.Identity.Name));
                aluno.NumCandidatura = cand.NumCandidatura;
                context.Candidaturas.Add(cand);
                //context.SaveChanges();
                //UpdateModel(aluno);
                context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View();
        }


        public ActionResult EscolherPropostas() {
            return View(context.Propostas);
        }

        [HttpPost]
        public ActionResult EscolherPropostas(int[] ids) {
            //listaPropostas = new List<Proposta>();
            var prop = new Proposta();

            foreach (var id in ids) {
                 prop = context.Propostas.Single(o => o.NumProposta == id);
                 listaPropostas.Add(prop);
            }

            return View("Candidatar");
        }

        [Authorize(Roles = "Aluno")]
        public ActionResult Visualizar()
        {
            Aluno aluno = context.Alunos.Where(x => x.NomeAluno.Equals(User.Identity.Name)).FirstOrDefault();
            return View(context.Candidaturas.Where(x => x.NumCandidatura == aluno.NumCandidatura).FirstOrDefault());
        }

        [Authorize(Roles = "Professor")]
        public ActionResult Processar(int idCandidatura) {
            IList<String> nomePropostas = new List<String>();
            foreach (var item in context.Propostas) {

                nomePropostas.Add(item.Enquadramento);
            }

            ViewBag.Propostas = new SelectList(nomePropostas.ToList());

            return View(context.Candidaturas.Where(x => x.NumCandidatura == idCandidatura).FirstOrDefault());
        }

        [Authorize(Roles = "Professor")]
        [HttpPost]
        public ActionResult Processar(Candidatura candidatura)
        {
            if (ModelState.IsValid)
            {
                int idProposta = 0;
                var numID = context.Professores.Where(x => x.Nome == candidatura.NomeOrientador).FirstOrDefault().NumeroProfessor;

                string nomeProposta = candidatura.PropostasSelect;
                var proposta = context.Propostas.Where(x => x.Enquadramento.Equals(nomeProposta)).FirstOrDefault();
                idProposta = proposta.NumProposta;


                Candidatura cand = new Candidatura()
                {
                    Estado = candidatura.Estado,
                    Ramo = candidatura.Ramo,
                    NumeroOrientador = numID,
                    NumAluno = candidatura.NumAluno,
                    NumCandidatura = candidatura.NumCandidatura,
                    NomeOrientador = candidatura.NomeOrientador,
                    DataDeCandidatura = candidatura.DataDeCandidatura,
                    MediaCurso = candidatura.MediaCurso,
                    NumeroDisciplinasPorConcluir = candidatura.NumeroDisciplinasPorConcluir,
                    LocalPreferencia = candidatura.LocalPreferencia,
                    //PropostasSelect = candidatura.PropostasSelect
                };

                var candidaturaToRemove = context.Candidaturas.Where(x => x.NumCandidatura == candidatura.NumCandidatura).FirstOrDefault();

                var aluno = context.Alunos.Where(x => x.NumeroAluno == candidatura.NumAluno).FirstOrDefault();
                aluno.NumProposta = idProposta;
                context.Candidaturas.Remove(candidaturaToRemove);
                context.SaveChanges();
                context.Candidaturas.Add(cand);
                context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        //[Authorize(Roles = "Professor")]
        public ActionResult EscolherCandidaturas() {
            return View(context.Candidaturas);
        }

        //[Authorize(Roles = "Professor")]
        [HttpPost]
        public ActionResult EscolherCandidaturas(int idCandidatura)
        {
            return View("Processar", idCandidatura);
        }
    }
}