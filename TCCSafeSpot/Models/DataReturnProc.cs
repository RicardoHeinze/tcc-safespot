using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TCCSafeSpot.Models
{
    public class DataReturnProc
    {
       
    }

   public class TipoCrimePorMes_Anual
   {
       public int QtdCrimeMes { get; set; }       
       public string Mes { get; set; }
   }

   public class TipoCrimePorMes
   {
       public int QtdCrime { get; set; }
       public string Nome { get; set; }
   }

   public class QtdCrimePorDia
   {
       public int QtdCrime { get; set; }
       public string Data { get; set; }
   }

    public class ListEnderecoConfirmacao
    {
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
    }


}