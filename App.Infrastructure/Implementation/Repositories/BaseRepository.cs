using App.Domain.Consts;
using App.Domain.Models.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using App.Application.Interfaces.Repositories;

namespace App.Infrastructure.Implementation.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IBaseEntity
    {
        public readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public virtual IEnumerable<T> GetAll(bool withNoTracking = true)
        {
            IQueryable<T> query = _context.Set<T>();

            if (withNoTracking)
                query = query.AsNoTracking();

            query = query.Where(x => !x.IsDeleted);
            query = query.OrderByDescending(x => x.CreatedOn);
            return query.ToList();
        }

        public virtual IEnumerable<T> GetAll(string[] includes, bool withNoTracking = true)
        {
            IQueryable<T> query = _context.Set<T>();

            if (withNoTracking)
                query = query.AsNoTracking();

            query = query.Where(x => !x.IsDeleted);

            if (includes != null)
                foreach (var incluse in includes)
                    query = query.Include(incluse);

            return query.ToList();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(bool withNoTracking = true)
        {
            IQueryable<T> query = _context.Set<T>();

            if (withNoTracking)
                query = query.AsNoTracking();

            query = query.Where(x => !x.IsDeleted);
            query = query.OrderByDescending(x => x.CreatedOn);

            return await query.ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(string[] includes, bool withNoTracking = true)
        {
            IQueryable<T> query = _context.Set<T>();

            if (withNoTracking)
                query = query.AsNoTracking();

            query = query.Where(x => !x.IsDeleted);

            if (includes != null)
                foreach (var incluse in includes)
                    query = query.Include(incluse);

            return await query.ToListAsync();
        }


        public IQueryable<T> GetQueryable()
        {
            return _context.Set<T>();
        }


        public T? GetById(Guid id)
        {
            return _context.Set<T>().Find(id);
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public T Find(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
                foreach (var incluse in includes)
                    query = query.Include(incluse);

            return query.SingleOrDefault(criteria);
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
                foreach (var incluse in includes)
                    query = query.Include(incluse);

            return await query.SingleOrDefaultAsync(criteria);
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            return query.Where(criteria).ToList();
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, int skip, int take)
        {
            return _context.Set<T>().Where(criteria).Skip(skip).Take(take).ToList();
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, int? skip, int? take,
            Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending)
        {
            IQueryable<T> query = _context.Set<T>().Where(criteria);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            if (orderBy != null)
            {
                if (orderByDirection == OrderBy.Ascending)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }

            return query.ToList();
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            return await query.Where(criteria).ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int take, int skip)
        {
            return await _context.Set<T>().Where(criteria).Skip(skip).Take(take).ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int? take, int? skip,
            Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending)
        {
            IQueryable<T> query = _context.Set<T>().Where(criteria);

            if (take.HasValue)
                query = query.Take(take.Value);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (orderBy != null)
            {
                if (orderByDirection == OrderBy.Ascending)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }

            return await query.ToListAsync();
        }

        public T Add(T entity, string userId)
        {
            entity.CreatedBy = userId;
            entity.CreatedOn = DateTime.UtcNow;
            _context.Set<T>().Add(entity);
            return entity;
        }

        public async Task<T> AddAsync(T entity, string userId)
        {
            entity.CreatedBy = userId;
            entity.CreatedOn = DateTime.UtcNow;
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public IEnumerable<T> AddRange(IEnumerable<T> entities, string userId)
        {
            foreach (var entity in entities)
            {
                entity.CreatedBy = userId;
                entity.CreatedOn = DateTime.UtcNow;
            }
            _context.Set<T>().AddRange(entities);
            return entities;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, string userId)
        {
            foreach (var entity in entities)
            {
                entity.CreatedBy = userId;
                entity.CreatedOn = DateTime.UtcNow;
            }
            await _context.Set<T>().AddRangeAsync(entities);
            return entities;
        }

        public T Update(T entity, string userId)
        {
            entity.UpdatedBy = userId;
            entity.UpdatedOn = DateTime.UtcNow;
            _context.Update(entity);
            return entity;
        }

        public T UpdateIsDeleted(T entity, string userId)
        {
            entity.IsDeleted = true;
            entity.UpdatedBy = userId;
            entity.UpdatedOn = DateTime.UtcNow;
            _context.Update(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public void Attach(T entity)
        {
            _context.Set<T>().Attach(entity);
        }

        public void AttachRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AttachRange(entities);
        }

        public int Count()
        {
            return _context.Set<T>().Count();
        }

        public int Count(Expression<Func<T, bool>> criteria)
        {
            return _context.Set<T>().Count(criteria);
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<T>().CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> criteria)
        {
            return await _context.Set<T>().CountAsync(criteria);
        }



    }
}
