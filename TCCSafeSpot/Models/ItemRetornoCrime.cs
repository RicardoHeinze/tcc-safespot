using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TCCSafeSpot.Models
{
    public class ItemRetornoCrime
    {
        public int Id { get; set; }

        public string Descricao { get; set; }

        public string Data { get; set; }

        public string Periodo { get; set; }

        public int TipoCrimeId { get; set; }

        public string TipoCrimeNome { get; set; }

        public int EnderecoId { get; set; }

        public string Estado { get; set; }

        public string Cidade { get; set; }

        public string Bairro { get; set; }

        public string Logradouro { get; set; }

        public string Numero { get; set; }

    }
}