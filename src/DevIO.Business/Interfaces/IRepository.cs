using DevIO.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Interfaces
{
    public interface IRepository<TEntidade> : IDisposable where TEntidade : Entidade
    {
        Task Adicionar(TEntidade entidade);
        Task<TEntidade> ObterPorId(Guid id);    
        Task<List<TEntidade>> ObterTodos();
        Task Atualizar(TEntidade entity);
        Task Remover(Guid id);
        Task<IEnumerable<TEntidade>> Buscar(Expression<Func<TEntidade, bool>> predicate);
        Task<int> SaveChanges();
    }
}
