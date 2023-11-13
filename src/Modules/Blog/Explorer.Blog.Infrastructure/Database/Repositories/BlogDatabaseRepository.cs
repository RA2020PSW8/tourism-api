using Explorer.Blog.Core.Domain;
using Explorer.Blog.Core.Domain.RepositoryInterfaces;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.Core.Domain;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Infrastructure.Database.Repositories
{
    public class BlogDatabaseRepository : CrudDatabaseRepository<Core.Domain.Blog, BlogContext>, IBlogRepository
    {
        private readonly DbSet<Core.Domain.Blog> _dbSet;
        public BlogDatabaseRepository(BlogContext dbContext) : base(dbContext)
        {
            _dbSet = dbContext.Set<Core.Domain.Blog>();
        }

        /*public PagedResult<Core.Domain.Blog> GetWithStatuses(int page, int pageSize)
        {
           var task = _dbSet.AsNoTracking().Include(b => b.BlogStatuses)
                        .Include(b => b.BlogRatings).GetPaged(page,pageSize);
           task.Wait();
           return task.Result;
        }*/

        public Core.Domain.Blog GetBlog(long id)
        {
            var entity = _dbSet.AsNoTracking().FirstOrDefault(f => f.Id == id);
            if (entity == null) throw new KeyNotFoundException("Not found: " + id);
            return entity;
        }
        public PagedResult<Core.Domain.Blog> GetWithStatuses(int page, int pageSize)
        {
            var task = _dbSet.Include(b => b.BlogStatuses).GetPaged(page,pageSize);
            task.Wait();
            return task.Result;
        }
    }
}
