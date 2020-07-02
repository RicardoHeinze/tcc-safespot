using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TCCSafeSpot.Models;
using TCCSafeSpot.Models.Implements;
using TCCSafeSpot.Models.Interfaces;

namespace TCCSafeSpot.Controllers
{
    public class CrimeCadastradoController : Controller
    {
        ICrimeCadastradoRepository crimeCadastradoRepository = new CrimeCadastradoRepository(new SafeSpotContext());
        //IVitimaRepository vitimaRepository = new VitimaRepository(new SafeSpotContext());

        // GET: CrimeCadastrado
        public ActionResult Index()
        {
            return View();
        }        

        [HttpPost]
        public ActionResult CadastrarCrime(CrimeCadastrado crimeCadastrado, string nomeVitima, string dataNascimentoVitima, string emailVitima, string sexoVitima)
        {
            try
            {
                Vitima vitima = new Vitima();
                vitima.Nome = nomeVitima;
                vitima.Email = emailVitima;
                vitima.DataNascimento = Convert.ToDateTime(dataNascimentoVitima);
                vitima.Sexo = sexoVitima;
                crimeCadastrado.Vitima = vitima;

                if (ModelState.IsValid)
                {
                    crimeCadastradoRepository.Adicionar(crimeCadastrado);
                    crimeCadastradoRepository.Salvar();

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Algo deu errado ao tentar realizar o cadastro. Detalhes: " + ex);
            }
            

            return View(crimeCadastrado);
        }
    }
}