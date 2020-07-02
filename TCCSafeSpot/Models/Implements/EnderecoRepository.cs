using TCCSafeSpot.Models.GenericRepository;
using TCCSafeSpot.Models.Interfaces;

namespace TCCSafeSpot.Models.Implements
{
    public class EnderecoRepository : GenericRepository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(SafeSpotContext _Contexto) : base(_Contexto)
        {
        }
    }
}