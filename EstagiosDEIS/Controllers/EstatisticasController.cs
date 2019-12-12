using EstagiosDEIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EstagiosDEIS.Controllers
{
    public class EstatisticasController : Controller
    {
        // GET: Estatisticas
        private DEISContext context = new DEISContext();


        [Authorize(Roles = "Professor")]
        public ActionResult Estatisticas()
        {
            Estatisticas estatisticas = new Estatisticas()
            {
                nAlunos = context.Alunos.Count(),
                nCandidaturas = context.Candidaturas.Count(),
                nEmpresas = context.Empresas.Count(),
                nPropostas = context.Propostas.Count(),
            };
            return View(estatisticas);
        }
    }
}