using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public.Commenting;
using Explorer.Blog.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Core.UseCases.Commenting
{
    public class ForumCommentService : CrudService<ForumCommentDto,ForumComment>,IForumCommentService
    {
        public ForumCommentService(ICrudRepository<ForumComment> repository,IMapper mapper) : base(repository, mapper) { }
    }
}
