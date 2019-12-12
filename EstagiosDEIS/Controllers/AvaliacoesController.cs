using EstagiosDEIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EstagiosDEIS.Controllers
{
    public class AvaliacoesController : Controller
    {
        // GET: Avaliacoes
        private DEISContext context = new DEISContext();

        [Authorize(Roles = "Empresa,Professor")]
        public ActionResult AvaliarAluno()
        {
            return View(context.Propostas.OrderBy(x => x.NumProposta));
        }


        [Authorize(Roles = "Professor,Empresa")]
        public ActionResult Edit(int NumProposta)
        {
            if (NumProposta == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proposta proposta = context.Propostas.Find(NumProposta);
            if (proposta == null)
            {
                return HttpNotFound();
            }
            return View(proposta);
        }

        [HttpPost]
        [Authorize(Roles = "Professor,Empresa")]
        public ActionResult Edit(Proposta proposta)
        {
            if (proposta == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                var propostaToRemove = context.Propostas.Where(x => x.NumProposta == proposta.NumProposta).FirstOrDefault();
                var novaProposta = propostaToRemove;
               
                
                if (propostaToRemove != null)
                {

                    if (novaProposta.DataFim < DateTime.Now && novaProposta.DataDefesa > DateTime.Now)
                    {
                        novaProposta.NotaAluno = proposta.NotaAluno;
                        context.Propostas.Remove(propostaToRemove);
                        context.SaveChanges();
                        context.Propostas.Add(novaProposta);
                        context.SaveChanges();

                    }else
                        ModelState.AddModelError("Erro", "datas mal");
                }
                
            }
            return RedirectToAction("Index", "Home");
        }

        //[Authorize(Roles = "Professor")]
        //[Authorize(Roles = "Empresa")]
        //[HttpPost]
        //public ActionResult AvaliarAluno()
        //{
        //    return View();
        //}

        [Authorize(Roles = "Aluno")]
        public ActionResult AvaliarEmpresa()
        {
            var aluno = context.Alunos.Single(x => x.NomeAluno.Equals(User.Identity.Name));
            return View(context.Propostas.Single(x => x.NumProposta == aluno.NumProposta));
        }

        [HttpPost]
        [Authorize(Roles = "Aluno")]
        public ActionResult AvaliarEmpresa(Proposta proposta)
        {
            if (proposta == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                var propostaToRemove = context.Propostas.Where(x => x.NumProposta == proposta.NumProposta).FirstOrDefault();
                var novaProposta = propostaToRemove;


                if (propostaToRemove != null)
                {

                   
                        novaProposta.NotaEmpresa = proposta.NotaEmpresa;
                        context.Propostas.Remove(propostaToRemove);
                        context.SaveChanges();
                        context.Propostas.Add(novaProposta);
                        context.SaveChanges();

                }

            }
            return RedirectToAction("Index", "Home");

        }
    }
}