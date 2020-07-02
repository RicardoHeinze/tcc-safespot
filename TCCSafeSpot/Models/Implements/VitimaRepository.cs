using TCCSafeSpot.Models.GenericRepository;
using TCCSafeSpot.Models.Interfaces;

namespace TCCSafeSpot.Models.Implements
{
    public class VitimaRepository: GenericRepository<Vitima>, IVitimaRepository
    {
        public VitimaRepository(SafeSpotContext _contexto) : base(_contexto)
        {
        }
    }
}