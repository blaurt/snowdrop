using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Snowdrop.DAL.Context;
using Snowdrop.Data.Entities.Core;

namespace Snowdrop.DAL.Repositories
{
    public sealed class Repository<TEntity>: IRepository<TEntity> where TEntity: BaseEntity
    {
        private readonly SnowdropContext _context = default;
        private readonly DbSet<TEntity> _entities = default;
            
        
        public Repository(IServiceProvider serviceProvider)
        {
            _context = serviceProvider.GetService<SnowdropContext>();
            if (_context == null)
            {
                throw new NullReferenceException("Cannot get db context!");
            }

            _entities = _context.Set<TEntity>();
        }
        
        public Task<TEntity> GetSingle(int id)
        {
            return _entities.SingleOrDefaultAsync(entity => entity.Id == id);
        }

        public Task<TEntity> GetSingle(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.Where(predicate).SingleOrDefaultAsync();
        }

        public Task Insert(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(entity)}");
            }
            
            _entities.Add(entity);
            return _context.SaveChangesAsync();
        }

        public Task Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(entity)}");
            }

            entity.UpdatedAt = DateTime.UtcNow;
            _entities.Update(entity);
            return _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await GetSingle(id);
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(entity)}");
            }
            
            entity.UpdatedAt = DateTime.UtcNow;

            _entities.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}