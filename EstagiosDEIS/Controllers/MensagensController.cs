using EstagiosDEIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EstagiosDEIS.Controllers
{
    public class MensagensController : Controller
    {
        // GET: Mensagens

        private DEISContext context = new DEISContext();
        public String destinoMsg;

        public ActionResult Index()
        {
            return View();
        }


        [Authorize(Roles = "Professor,Empresa,Aluno")]
        public ActionResult ResponderMensagem(String destName)
        {
            destinoMsg = destName;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Professor,Empresa,Aluno")]
        public ActionResult ResponderMensagem(Mensagens _msg)
        {
            if (ModelState.IsValid)
            {
                if (_msg != null)
                {
                    var numID = context.Mensagens.OrderByDescending(x => x.NumMensagem).FirstOrDefault().NumMensagem;
                    Mensagens msg_local = new Mensagens()
                    {
                        NumMensagem = numID + 1,
                        DataMensagem = DateTime.Now,
                        Destinatario = destinoMsg,
                        Remetente = User.Identity.Name,
                        Mensagem = _msg.Mensagem,
                    };
                    

                    context.Mensagens.Add(msg_local);
                    context.SaveChanges();
                    return RedirectToAction("Index", "Home");
                    //}
                    // ModelState.AddModelError("Erro", "Já existe uma proposta igual!");
                }
            }
            return View();
        }

        public ActionResult LerMensagens()
        {
            return View(context.Mensagens.OrderBy(x => x.DataMensagem).Where(x => x.Destinatario == User.Identity.Name));
        }

        [Authorize(Roles = "Professor,Empresa,Aluno")]
        public ActionResult EscreverMensagem()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Professor,Empresa,Aluno")]
        public ActionResult EscreverMensagem(Mensagens _msg)
        {

            if (ModelState.IsValid)
            {
                if (_msg != null)
                {
                    //if (!context.Propostas.Any(x => x.NumProposta == proposta.NumProposta))
                    //{
                    var numID = context.Mensagens.OrderByDescending(x => x.NumMensagem).FirstOrDefault().NumMensagem;
                    

                    Mensagens msg = new Mensagens()
                    {
                        NumMensagem = numID + 1,
                        Destinatario = _msg.Destinatario,
                        Remetente = User.Identity.Name,
                        DataMensagem = DateTime.Now,
                        Mensagem = _msg.Mensagem,
                    };

                    context.Mensagens.Add(msg);
                    context.SaveChanges();
                    return RedirectToAction("Index", "Home");
                    //}
                    // ModelState.AddModelError("Erro", "Já existe uma proposta igual!");
                }
            }
            return View();
        }
    }
}