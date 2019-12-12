using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EstagiosDEIS;
using EstagiosDEIS.Models;

namespace EstagiosDEIS.Controllers
{

    public class EstagiosController : Controller
    {
        // GET: Estagios
        private DEISContext context = new DEISContext();



        [AllowAnonymous]
        public ActionResult Lista()
        {
            return View(context.Propostas.OrderBy(x => x.NumProposta));
        }

        [AllowAnonymous]
        public ActionResult OrderListID()
        {
            return View(context.Propostas.OrderBy(x => x.NumProposta));
        }

        

        [Authorize(Roles = "Professor,Empresa")]
        public ActionResult Inserir()
        {
            
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Professor,Empresa")]
        public ActionResult Inserir(Proposta proposta)
        {
            //try
            //{
                if (ModelState.IsValid)
                {
                    if (proposta != null)
                    {
                    //if (!context.Propostas.Any(x => x.NumProposta == proposta.NumProposta))
                    //{

                    var numID = 0;
                    var numOrientador = 0;


                   
                    try
                    {
                        numID = context.Propostas.OrderByDescending(x => x.NumProposta).FirstOrDefault().NumProposta;
                        numOrientador = context.Professores.Where(x => x.Nome == proposta.NomeOrientador).FirstOrDefault().NumeroProfessor;
                    }
                    catch (NullReferenceException e) {
                       
                    }
                        
                            

                        Proposta prop = new Proposta()
                        {
                            NumProposta = numID + 1,
                            RamoAluno = proposta.RamoAluno,
                            NomeOrientador = proposta.NomeOrientador,
                            NumOrientador = numOrientador,
                            Enquadramento = proposta.Enquadramento,
                            Objetivos = proposta.Objetivos,
                            CondicoesAcesso = proposta.CondicoesAcesso,
                            LocalProposta = proposta.LocalProposta,
                            DataInicio = proposta.DataInicio,
                            DataDefesa = proposta.DataDefesa,
                            DataFim = proposta.DataFim,
                            AdicionadoPor = User.Identity.Name,
                            NomeDaEmpresa = proposta.NomeDaEmpresa,
                        };

                            context.Propostas.Add(prop);
                            context.SaveChanges();
                            return RedirectToAction("Index", "Home");
                        //}
                       // ModelState.AddModelError("Erro", "Já existe uma proposta igual!");
                    }
                }
           // }
            //catch { }

            return View();
        }





        [HttpGet]
        [Authorize(Roles ="Professor,Empresa")]
        public ActionResult Remover()
        {
            return View(context.Propostas.OrderBy(x => x.NumProposta));
        }

        
        [Authorize(Roles = "Professor,Empresa")]
        public ActionResult RemoverProposta(int NumProposta)
        {
            if (NumProposta == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Proposta proposta = context.Propostas.Find(NumProposta);
            context.Propostas.Remove(proposta);
            context.SaveChanges();
            
            return RedirectToAction("Index", "Home");
           // return View(proposta);
        }


       


        [Authorize(Roles = "Professor")]
        public ActionResult PropostasOrientadas()
        {
            return View(context.Propostas.Where(x => x.NomeOrientador.Equals(User.Identity.Name)));
        }

        [Authorize(Roles = "Professor")]
        public ActionResult GerirParticipacao() {
            return View();
        }

        [Authorize(Roles = "Professor,Empresa")]
        public ActionResult PropostasSubmetidas()
        {
            return View(context.Propostas.Where(x => x.AdicionadoPor.Equals(User.Identity.Name)));
        }
    }
}