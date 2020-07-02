using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace TCCSafeSpot.Models.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T: class
    {
        SafeSpotContext contexto = null;

        public GenericRepository(SafeSpotContext _Contexto)
        {
            contexto = _Contexto;
        }

        public void Adicionar(T entidade)
        {
            contexto.Set<T>().Add(entidade);            
        }

        public void Atualizar(T entidade)
        {
            contexto.Entry(entidade).State = EntityState.Modified;            
        }

        public void ExcluirPorId(int id)
        {
            var entidade = contexto.Set<T>().Find(id);
            contexto.Set<T>().Remove(entidade);            
        }

        public T BuscarPorId(int id)
        {
            return contexto.Set<T>().Find(id);
        }

        public List<T> ListarTodos()
        {
            return contexto.Set<T>().ToList();
        }

        public void Salvar()
        {
            contexto.SaveChanges();
        }
    }
}