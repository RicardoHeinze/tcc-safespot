using System;
using System.Collections.Generic;
using TCCSafeSpot.Models.GenericRepository;
using TCCSafeSpot.Models.ViewModels.Crimes;

namespace TCCSafeSpot.Models.Interfaces
{
    public interface ICrimeCadastradoRepository: IGenericRepository<CrimeCadastrado>
    {        
        string getNomeMes(string numMes);
        List<TipoCrimePorMes_Anual> RetornaTipoCrimePorMes_Anual(Endereco endereco);
        List<TipoCrimePorMes> RetornaTipoCrimePorMes(Endereco endereco);
        List<QtdCrimePorDia> RetornaQtdCrimePorDia(Endereco endereco);
        List<CrimeCadastrado> RetornaCrimeCadastrado(Endereco endereco);
        List<ListEnderecoConfirmacao> ListEnderecoConfirmacao(Endereco endereco);
        String GeraMsgSeguranca(Endereco endereco);


    }
}
