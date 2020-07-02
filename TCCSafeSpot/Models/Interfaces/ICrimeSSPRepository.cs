using System;
using System.Collections.Generic;
using TCCSafeSpot.Models.GenericRepository;
using TCCSafeSpot.Models.ViewModels.Crimes;
using static TCCSafeSpot.Models.Implements.CrimeSSPRepository;

namespace TCCSafeSpot.Models.Interfaces
{
    public interface ICrimeSSPRepository : IGenericRepository<CrimeSSP>
    {       
        List<CrimeSSP> RetornaCrimeSSP(Endereco endereco);        
        List<TipoCrimePorMes_Anual> RetornaTipoCrimePorMes_Anual(Endereco endereco);
        List<TipoCrimePorMes> RetornaTipoCrimePorMes(Endereco endereco);
        List<QtdCrimePorDia> RetornaQtdCrimePorDia(Endereco endereco);
        string getNomeMes(string numMes);
        List<ListEnderecoConfirmacao> ListEnderecoConfirmacao(Endereco endereco);
        String GeraMsgSeguranca(Endereco endereco);

    }
}
