using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TCCSafeSpot.Models;
using TCCSafeSpot.Models.Implements;
using TCCSafeSpot.Models.Interfaces;

namespace TCCSafeSpot.Controllers
{
    public class ImportFileController : Controller
    {

        ICrimeSSPRepository crimeSSPRepository = new CrimeSSPRepository(new SafeSpotContext());
        ITipoCrimeRepository tipoCrimeRepository = new TipoCrimeRepository(new SafeSpotContext());

        public void ImportFilesStart()

        {
            var listFiles = ListFiles(HttpRuntime.AppDomainAppPath + "\\ImportFiles\\");

            foreach (var file in listFiles)
            {
                StartImportFile(file);
            }
        }

        private void StartImportFile(FileInfo file)
        {
            string anoArquivo = string.Empty;
            string mesArquivo = string.Empty;
            string tipoArquivo = string.Empty;

            try
            {
                ImportFile(file.Name);
            }
            catch (Exception)
            {

            }
        }

        public FileInfo[] ListFiles(string pathFile)
        {
            try
            {
                DirectoryInfo d = new DirectoryInfo(HttpRuntime.AppDomainAppPath + "\\ImportFiles\\");
                return d.GetFiles("*.csv");
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string FindCrimeType(string nameFile)
        {
            try
            {
                return nameFile.Substring(nameFile.LastIndexOf('(') + 1, nameFile.LastIndexOf(')') - nameFile.LastIndexOf('(') - 1);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public string FindCrimeYear(string nameFile)
        {
            try
            {
                return nameFile.Substring(0, nameFile.LastIndexOf('_')).Substring(nameFile.LastIndexOf("DadosBO") + 8);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public string FindCrimeMonth(string nameFile)
        {
            try
            {
                return nameFile.Substring(0, nameFile.LastIndexOf('(')).Substring(nameFile.LastIndexOf("DadosBO") + 13);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }



        public bool ImportFile(string fileName)
        {
            try
            {

                //ProjetoContext projetoContext = new ProjetoContext();
                bool isFirst = true;
                string[] lines = System.IO.File.ReadAllLines(
                    HttpRuntime.AppDomainAppPath + "\\ImportFiles\\" + fileName,
                    Encoding.UTF8
                );

                
                CrimeSSP crimeSSP = new CrimeSSP();
                Endereco endereco = new Endereco();
                TipoCrime tipoCrime = tipoCrimeRepository.GetTipoCrime(FindCrimeType(fileName));                

                // Display the file contents by using a foreach loop.
                foreach (string line in lines)
                {
                    if (!isFirst)
                    {

                        var lineArray = line.Split(';');

                        endereco = new Endereco();
                        endereco.Bairro = lineArray[14];
                        endereco.Cep = "";
                        endereco.CidadeBO = lineArray[15];
                        endereco.Estado = lineArray[16];
                        endereco.CidadeBO = lineArray[15];
                        endereco.Logradouro = lineArray[12];
                        endereco.Numero = lineArray[13];
                        
                        crimeSSP = new CrimeSSP();
                        crimeSSP.Endereco = endereco;
                        crimeSSP.TipoCrimeId = tipoCrime.Id;
                        crimeSSP.Data = lineArray[5];

                        if (!endereco.Numero.Equals(0) && !endereco.Bairro.Equals(String.Empty) && !endereco.Logradouro.Equals(String.Empty) && !endereco.CidadeBO.Equals(String.Empty) && !endereco.Estado.Equals(String.Empty))
                        {
                            crimeSSPRepository.Adicionar(crimeSSP);
                        }
                        
                    }
                    else
                    {
                        isFirst = false;
                    }
                }
                
                crimeSSPRepository.Salvar();

            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

    }
}