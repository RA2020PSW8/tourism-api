using Explorer.Blog.API.Dtos;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Blog.Core.Domain;
using AutoMapper;
using Explorer.Blog.API.Public.Blog;

namespace Explorer.Blog.Core.UseCases.Blog
{
    public class BlogService : CrudService<BlogDto, Domain.Blog>, IBlogService
    {
        public BlogService(ICrudRepository<Domain.Blog> crudRepository, IMapper mapper) : base(crudRepository, mapper)
        {

        }
    }
}
