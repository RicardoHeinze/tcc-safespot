using TCCSafeSpot.Models.GenericRepository;
using TCCSafeSpot.Models.Interfaces;

namespace TCCSafeSpot.Models.Implements
{
    public class TipoCrimeRepository : GenericRepository<TipoCrime>, ITipoCrimeRepository
    {
        public TipoCrimeRepository(SafeSpotContext _Contexto) : base(_Contexto)
        {
        }

        public TipoCrime GetTipoCrime(string tipoCrimeNome)
        {
            TipoCrime tipoCrimeRet = new TipoCrime();
            switch (tipoCrimeNome)
            {
                case "FURTO DE CELULAR":
                    tipoCrimeRet.Nome = "FURTO DE CELULAR";
                    tipoCrimeRet.Id = 1;
                    break;
                case "FURTO DE VEICULOS":
                    tipoCrimeRet.Nome = "FURTO DE VEÍCULO";
                    tipoCrimeRet.Id = 2;
                    break;
                case "HOMICIDIO DOLOSO":
                    tipoCrimeRet.Nome = "HOMICÍDIO DOLOSO";
                    tipoCrimeRet.Id = 3;
                    break;
                case "LATROCINIO":
                    tipoCrimeRet.Nome = "LATROCÍNIO";
                    tipoCrimeRet.Id = 4;
                    break;
                case "LESAO CORPORAL SEGUIDA DE MORTE":
                    tipoCrimeRet.Nome = "LESÃO CORPORAL DOLOSA SEGUIDA DE MORTE";
                    tipoCrimeRet.Id = 5;
                    break;
                case "ROUBO DE CELULAR":
                    tipoCrimeRet.Nome = "ROUBO DE CELULAR";
                    tipoCrimeRet.Id = 6;
                    break;
                case "ROUBO DE VEICULO":
                    tipoCrimeRet.Nome = "ROUBO DE VEÍCULOS";
                    tipoCrimeRet.Id = 7;
                    break;
                default:
                    tipoCrimeRet = null;
                    break;
            }
            return tipoCrimeRet;
        }
    }
}