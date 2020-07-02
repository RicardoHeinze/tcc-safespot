using TCCSafeSpot.Models.GenericRepository;

namespace TCCSafeSpot.Models.Interfaces
{
    public interface ITipoCrimeRepository : IGenericRepository<TipoCrime>
    {
        TipoCrime GetTipoCrime(string tipoCrimeNome);
    }
}
