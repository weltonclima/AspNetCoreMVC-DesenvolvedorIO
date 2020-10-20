using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Data.Repository
{
    public abstract class Repository<TEntidade> : IRepository<TEntidade> where TEntidade : Entidade, new()
    {
        protected readonly MeuDbContext Db;
        protected readonly DbSet<TEntidade> DbSet;
        public Repository(MeuDbContext db)
        {
            Db = db;
            DbSet = db.Set<TEntidade>();
        }
        public async Task<IEnumerable<TEntidade>> Buscar(Expression<Func<TEntidade, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }
        public virtual async Task<TEntidade> ObterPorId(Guid id)
        {
            return await DbSet.FindAsync(id);
        }
        public virtual async Task<List<TEntidade>> ObterTodos()
        {
            return await DbSet.ToListAsync();
        }
        public virtual async Task Adicionar(TEntidade entidade)
        {
            DbSet.Add(entidade);
            await SaveChanges();
        }

        public virtual async Task Atualizar(TEntidade entidade)
        {
            DbSet.Update(entidade);
            await SaveChanges();
        }
        public virtual async Task Remover(Guid id)
        {
            DbSet.Remove(new TEntidade {Id = id} );
            await SaveChanges();
        }
        
        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }
        public void Dispose()
        {
            Db?.Dispose();
        }
    }
}
