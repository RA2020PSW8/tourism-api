﻿using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.Core.Domain;

namespace Explorer.Blog.Core.Mappers;

public class BlogProfile : Profile
{
    public BlogProfile()
    {
        CreateMap<BlogDto, Domain.Blog>().ReverseMap();
        CreateMap<BlogStatusDto, BlogStatus>().ReverseMap();
    }
}