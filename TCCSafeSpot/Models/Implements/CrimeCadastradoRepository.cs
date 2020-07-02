using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using TCCSafeSpot.Models.GenericRepository;
using TCCSafeSpot.Models.Interfaces;
using TCCSafeSpot.Models.ViewModels.Crimes;

namespace TCCSafeSpot.Models.Implements
{
    public class CrimeCadastradoRepository : GenericRepository<CrimeCadastrado>, ICrimeCadastradoRepository
    {
        private SafeSpotContext contexto = null;

        public CrimeCadastradoRepository(SafeSpotContext _Contexto) : base(_Contexto)
        {
            contexto = _Contexto;
        }

      
        public string getNomeMes(string numMes)
        {
            var nomeMes = "";

            switch (numMes)
            {
                case "1":
                    nomeMes = "Janeiro";
                    break;
                case "2":
                    nomeMes = "Fevereiro";
                    break;
                case "3":
                    nomeMes = "Março";
                    break;
                case "4":
                    nomeMes = "Abril";
                    break;
                case "5":
                    nomeMes = "Maio";
                    break;
                case "6":
                    nomeMes = "Junho";
                    break;
                case "7":
                    nomeMes = "Julho";
                    break;
                case "8":
                    nomeMes = "Agosto";
                    break;
                case "9":
                    nomeMes = "Setembro";
                    break;
                case "10":
                    nomeMes = "Outubro";
                    break;
                case "11":
                    nomeMes = "Novembro";
                    break;
                case "12":
                    nomeMes = "Dezembro";
                    break;

            }

            return nomeMes;
        }

        public List<TipoCrimePorMes_Anual> RetornaTipoCrimePorMes_Anual(Endereco endereco)
        {
            List<TipoCrimePorMes_Anual> listaCrimesPorMesAnual = new List<TipoCrimePorMes_Anual>();
            TipoCrimePorMes_Anual obj;

            var resultado = RetornaCrimeCadastrado(endereco);

            var dataInicio = Convert.ToDateTime("01/01/2019");

            for (var i = 1; i <= 12; i++)
            {
                obj = new TipoCrimePorMes_Anual();

                var dataFim = dataInicio.AddMonths(1);

                var retorno = (from listCrime in resultado
                               where Convert.ToDateTime(listCrime.Data) >= dataInicio
                                 && Convert.ToDateTime(listCrime.Data) < dataFim
                               select new
                               {
                                   QtdCrime = 0,
                                   tipoCrimeId = listCrime.TipoCrimeId,
                                   Data = Convert.ToDateTime(listCrime.Data)

                               }
                               ).GroupBy(_ => _.tipoCrimeId).Select(_ => _.FirstOrDefault()).ToList();


                foreach (var item in retorno)
                {
                    obj.QtdCrimeMes += resultado.Count(_ => _.TipoCrimeId == item.tipoCrimeId);
                    obj.Mes = getNomeMes(i.ToString());

                }

                if (obj.Mes == null)
                {
                    obj.QtdCrimeMes = 0;
                    obj.Mes = getNomeMes(i.ToString()); 
                }

                listaCrimesPorMesAnual.Add(obj);
                dataInicio = dataFim;
            }

            return listaCrimesPorMesAnual;
        }

        public List<TipoCrimePorMes> RetornaTipoCrimePorMes(Endereco endereco)
        {
            List<TipoCrimePorMes> listaCrimesPorMes = new List<TipoCrimePorMes>();
            TipoCrimePorMes obj;
            string crimeSSP = "";

            var resultado = RetornaCrimeCadastrado(endereco);

            if (resultado.Count != 0)
            {
                crimeSSP = resultado.OrderByDescending(_ => _.Data).First().Data;
            }
            else
            {
                crimeSSP = "01/12/2018";
            }

            var dataInicio = new DateTime(
               int.Parse(crimeSSP.Split(' ')[0].Split('/')[2]),
               int.Parse(crimeSSP.Split(' ')[0].Split('/')[1]),
               1, 0, 0, 0);

            var dataFim = dataInicio.AddMonths(1);

            var retorno = (from listCrime in resultado
                           where Convert.ToDateTime(listCrime.Data) >= dataInicio
                             && Convert.ToDateTime(listCrime.Data) < dataFim
                           select new
                           {
                               QtdCrime = 0,
                               tipoCrimeId = listCrime.TipoCrimeId
                           }
                           ).GroupBy(_ => _.tipoCrimeId).Select(_ => _.FirstOrDefault()).ToList();

            foreach (var item in retorno)
            {
                obj = new TipoCrimePorMes();
                obj.QtdCrime = resultado.Count(_ => _.TipoCrimeId == item.tipoCrimeId);
                obj.Nome = this.contexto.TipoCrime.Find(item.tipoCrimeId).Nome;
                listaCrimesPorMes.Add(obj);
            }

            return listaCrimesPorMes;

        }

        public List<QtdCrimePorDia> RetornaQtdCrimePorDia(Endereco endereco)
        {
            List<QtdCrimePorDia> listaCrimesPorDia = new List<QtdCrimePorDia>();
            QtdCrimePorDia obj;
            string crimeSSP = "";

            var resultado = RetornaCrimeCadastrado(endereco);

            if (resultado.Count != 0)
            {
                crimeSSP = resultado.OrderByDescending(_ => _.Data).First().Data;
            }
            else
            {
                crimeSSP = "01/12/2018";
            }

            var dataInicio = new DateTime(
               int.Parse(crimeSSP.Split(' ')[0].Split('/')[2]),
               int.Parse(crimeSSP.Split(' ')[0].Split('/')[1]),
               1, 0, 0, 0);

            var dataFim = dataInicio.AddMonths(1);

            var retorno = (from listCrime in resultado
                           where Convert.ToDateTime(listCrime.Data) >= dataInicio
                             && Convert.ToDateTime(listCrime.Data) < dataFim
                           select new
                           {
                               QtdCrime = 0,//resultado.Count(_ => _.Data.Equals(listCrime.Data)),
                               Data = listCrime.Data.Substring(0, 10)
                           }
                           ).GroupBy(_ => Convert.ToDateTime(_.Data.Substring(0, 10))).Select(_ => _.FirstOrDefault()).OrderBy(_ => _.Data).ToList();


            foreach (var item in retorno)
            {
                obj = new QtdCrimePorDia();
                obj.QtdCrime = resultado.Count(_ => _.Data.Equals(item.Data));
                obj.Data = item.Data;
                listaCrimesPorDia.Add(obj);
            }

            return listaCrimesPorDia;
        }


        public List<ListEnderecoConfirmacao> ListEnderecoConfirmacao(Endereco endereco)
        {
            var resultado = RetornaCrimeCadastrado(endereco);

            var retorno = (from listCrime in resultado
                           where listCrime.Endereco.Logradouro.Contains(endereco.Logradouro.ToUpper())
                           select new ListEnderecoConfirmacao
                           {
                               Bairro = listCrime.Endereco.Bairro,
                               Cidade = listCrime.Endereco.CidadeBO,
                               Logradouro = listCrime.Endereco.Logradouro
                           }
                           ).GroupBy(_ => new { _.Bairro, _.Cidade, _.Logradouro }).Select(_ => _.FirstOrDefault()).ToList();

            return retorno;
        }

        public String GeraMsgSeguranca(Endereco endereco)
        {
            var mediaBairros = 0;
            var bairroSelecionado = 0;
            var qtdBairros = 0;

            var resultado = RetornaCrimeCadastrado(endereco);

            var retorno = resultado
                            .Where(_ => _.Endereco.CidadeBO == endereco.CidadeBO)
                            .Select(_ => new
                            {
                                Cidade = _.Endereco.CidadeBO,
                                Bairro = _.Endereco.Bairro,
                                QuantidadeCrime = _.Endereco.CidadeBO.Count()
                            }).ToList();



            foreach (var item in retorno)
            {
                qtdBairros += 1;
                if (item.Bairro == endereco.Bairro)
                    bairroSelecionado = item.QuantidadeCrime;
                else
                {
                    mediaBairros += item.QuantidadeCrime;
                }
            }

            if (bairroSelecionado < (mediaBairros / qtdBairros))
                return "A localidade pesquisa é segura.";
            else if (bairroSelecionado == (mediaBairros / qtdBairros))
                return "A localidade pesquisa está na média.";
            else if (bairroSelecionado < (mediaBairros / qtdBairros))
                return "A localidade pesquisa é insegura.";
            else
                return "Houve um erro, tente novamente";

        }

        public List<CrimeCadastrado> RetornaCrimeCadastrado(Endereco endereco)
        {
            List<CrimeCadastrado> listaCrimes = new List<CrimeCadastrado>();

            if (endereco.Bairro == null && endereco.CidadeBO == null)
            {
                // Busca por Logradouro                
                listaCrimes = this.contexto.CrimeCadastrado.AsNoTracking().Where(c => c.Endereco.Logradouro.Contains(endereco.Logradouro.ToUpper())).ToList();

            }
            else if (endereco.CidadeBO == null && endereco.Logradouro == null)
            {
                // Busca por Bairro
                listaCrimes = this.contexto.CrimeCadastrado.AsNoTracking().Where(c => c.Endereco.Bairro.Contains(endereco.Bairro.ToUpper())).ToList();

            }
            else if (endereco.Logradouro == null && endereco.Bairro == null)
            {
                if (endereco.CidadeBO.Equals("São Paulo"))
                {
                    endereco.CidadeBO = "S.Paulo";
                }
                // Busca por Cidade
                listaCrimes = this.contexto.CrimeCadastrado.AsNoTracking().Where(c => c.Endereco.CidadeBO.Contains(endereco.CidadeBO.ToUpper())).ToList();

            }
            else if (endereco.Logradouro == null)
            {
                // Busca por Cidade e Bairro
                listaCrimes = this.contexto.CrimeCadastrado.AsNoTracking().Where(c => c.Endereco.CidadeBO.Contains(endereco.CidadeBO.ToUpper())).Where(c => c.Endereco.Bairro.Contains(endereco.Bairro.ToUpper())).ToList();

            }
            else if (endereco.Bairro == null)
            {
                // Busca por Cidade e Logradouro
                listaCrimes = this.contexto.CrimeCadastrado.AsNoTracking().Where(c => c.Endereco.CidadeBO.Contains(endereco.CidadeBO.ToUpper())).Where(c => c.Endereco.Logradouro.Contains(endereco.Logradouro.ToUpper())).ToList();

            }
            else
            {
                // Busca por Bairro e Endereço
                listaCrimes = this.contexto.CrimeCadastrado.AsNoTracking().Where(c => c.Endereco.Bairro.Contains(endereco.Bairro.ToUpper())).Where(c => c.Endereco.Logradouro.Contains(endereco.Logradouro.ToUpper())).ToList();

            }

            return listaCrimes;
        }

    }
}