using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TCCSafeSpot.Models.ViewModels.Crimes
{
    public class RetornoCrimeViewModel
    {
        public int Id { get; set; }

        public string Descricao { get; set; }

        public string Data { get; set; }

        public string Periodo { get; set; }

        public int TipoCrimeId { get; set; }

        public string TipoCrimeNome { get; set; }

        public int EnderecoId { get; set; }        

        public string EnderecoEstado { get; set; }

        public string EnderecoCidadeBO { get; set; }

        public string EnderecoBairro { get; set; }

        public string EnderecoLogradouro { get; set; }

        public string EnderecoNumero { get; set; }
    }
}