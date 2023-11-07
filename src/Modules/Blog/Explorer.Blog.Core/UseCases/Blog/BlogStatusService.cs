using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public.Blog;
using Explorer.Blog.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Public;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Core.UseCases.Blog
{
    public class BlogStatusService : CrudService<BlogStatusDto, BlogStatus>, IBlogStatusService
    {
       public BlogStatusService(ICrudRepository<BlogStatus> crudRepository, IMapper mapper) : base(crudRepository, mapper)
        {

        }
    }
}
