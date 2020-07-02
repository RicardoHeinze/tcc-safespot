using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCCSafeSpot.Models.GenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        void Adicionar(T entidade);
        void ExcluirPorId(int id);
        void Atualizar(T entidade);
        List<T> ListarTodos();
        T BuscarPorId(int id);
        void Salvar();

    }
}
