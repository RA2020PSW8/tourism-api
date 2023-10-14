using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Core.Mappers
{
    public class ForumCommentProfile : Profile
    {
        public ForumCommentProfile()
        {
            CreateMap<ForumCommentDto, ForumComment>().ReverseMap();
        }
    }
}
