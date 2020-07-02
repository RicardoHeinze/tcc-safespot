using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TCCSafeSpot.Models;
using TCCSafeSpot.Models.Implements;
using TCCSafeSpot.Models.Interfaces;
using TCCSafeSpot.Models.ViewModels.Crimes;
using static TCCSafeSpot.Models.Implements.CrimeSSPRepository;

namespace TCCSafeSpot.Controllers
{
    public class HomeController : Controller
    {
        IEnderecoRepository enderecoRepository = new EnderecoRepository(new SafeSpotContext());
        ICrimeSSPRepository crimeSSPRepository = new CrimeSSPRepository(new SafeSpotContext());
        ICrimeCadastradoRepository crimeCadastradoRepository = new CrimeCadastradoRepository(new SafeSpotContext());
               
        public ActionResult Index()
        {
            //Endereco endereco = new Endereco();
            //endereco.Bairro = "CENTRO";
            //endereco.Logradouro = "RUA RIACHUELO";
            //endereco.CidadeBO = "PIRAJUI";
            //GeraMsgSeguranca(endereco);
            //RetornaTipoCrimePorMes_Anual();
            //ImportFileController import = new ImportFileController();
            //import.ImportFilesStart();
            return View();
        }

        public JsonResult RetornaTipoCrimePorMes_Anual(Endereco endereco, int? TipoBaseDados)
        {
            List<TipoCrimePorMes_Anual> listaTipoCrimePorMesAnual = new List<TipoCrimePorMes_Anual>();

            if (TipoBaseDados == 1 || TipoBaseDados == 0)
            {
                listaTipoCrimePorMesAnual = crimeSSPRepository.RetornaTipoCrimePorMes_Anual(endereco);

            }
            else if (TipoBaseDados == 2)
            {
                listaTipoCrimePorMesAnual = crimeCadastradoRepository.RetornaTipoCrimePorMes_Anual(endereco);

            }            

            return Json(listaTipoCrimePorMesAnual, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RetornaTipoCrimePorMes(Endereco endereco, int? TipoBaseDados)
        {
            List<TipoCrimePorMes> listaTipoCrimePorMes = new List<TipoCrimePorMes>();

            if (TipoBaseDados == 1 || TipoBaseDados == 0)
            {
                listaTipoCrimePorMes = crimeSSPRepository.RetornaTipoCrimePorMes(endereco);

            }
            else if (TipoBaseDados == 2)
            {
                listaTipoCrimePorMes = crimeCadastradoRepository.RetornaTipoCrimePorMes(endereco);
                
            }
            
            return Json(listaTipoCrimePorMes, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RetornaQtdCrimePorDia(Endereco endereco, int? TipoBaseDados)
        {
            List<QtdCrimePorDia> listaCrimePorDia = new List<QtdCrimePorDia>();

            if (TipoBaseDados == 1 || TipoBaseDados == 0)
            {
                listaCrimePorDia = crimeSSPRepository.RetornaQtdCrimePorDia(endereco);

            }
            else if (TipoBaseDados == 2)
            {
                listaCrimePorDia = crimeCadastradoRepository.RetornaQtdCrimePorDia(endereco);

            }

            return Json(listaCrimePorDia, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListEnderecoConfirmacao(Endereco endereco, int? TipoBaseDados)
        {
            List<ListEnderecoConfirmacao> listaCrimePorDia = new List<ListEnderecoConfirmacao>();

            if (TipoBaseDados == 1 || TipoBaseDados == 0)
            {
                listaCrimePorDia = crimeSSPRepository.ListEnderecoConfirmacao(endereco);

            }
            else if (TipoBaseDados == 2)
            {
                listaCrimePorDia = crimeCadastradoRepository.ListEnderecoConfirmacao(endereco);

            }

            return Json(listaCrimePorDia, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GeraMsgSeguranca(Endereco endereco, int? TipoBaseDados)
        {
            String ret = "";
            endereco.Logradouro = null;

            if (TipoBaseDados == 1 || TipoBaseDados == 0)
            {
                ret = crimeSSPRepository.GeraMsgSeguranca(endereco);

            }
            else if (TipoBaseDados == 2)
            {
                ret = crimeCadastradoRepository.GeraMsgSeguranca(endereco);

            }         

            return Json(ret, JsonRequestBehavior.AllowGet);
        }

    }
}