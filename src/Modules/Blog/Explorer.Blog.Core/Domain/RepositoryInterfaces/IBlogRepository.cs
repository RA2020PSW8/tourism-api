using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Core.Domain.RepositoryInterfaces
{
    public interface IBlogRepository : ICrudRepository<Core.Domain.Blog>
    {
        public PagedResult<Blog> GetWithStatuses(int page, int pageSize);
    }
}
