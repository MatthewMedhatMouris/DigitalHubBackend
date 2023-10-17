using App.Domain.Consts;
using App.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : class, IBaseEntity
    {
        IEnumerable<T> GetAll(bool withNoTracking = true);
        IEnumerable<T> GetAll(string[] includes, bool withNoTracking = true);

        Task<IEnumerable<T>> GetAllAsync(bool withNoTracking = true);
        Task<IEnumerable<T>> GetAllAsync(string[] includes, bool withNoTracking = true);
        IQueryable<T> GetQueryable();
        T? GetById(Guid id);
        Task<T> GetByIdAsync(Guid id);

        T Find(Expression<Func<T, bool>> criteria, string[] includes = null);
        Task<T> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null);
        IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, string[] includes = null);
        IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, int take, int skip);
        IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, int? take, int? skip,
            Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending);

        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, string[] includes = null);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int skip, int take);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int? skip, int? take,
            Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending);
        T Add(T entity, string userId);
        Task<T> AddAsync(T entity, string userId);
        IEnumerable<T> AddRange(IEnumerable<T> entities, string userId);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, string userId);
        T Update(T entity, string userId);
        T UpdateIsDeleted(T entity, string userId);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
        void Attach(T entity);
        void AttachRange(IEnumerable<T> entities);
        int Count();
        int Count(Expression<Func<T, bool>> criteria);
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<T, bool>> criteria);
    }
}
