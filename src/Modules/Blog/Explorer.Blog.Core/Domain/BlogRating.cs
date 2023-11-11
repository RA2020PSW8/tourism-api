using Explorer.Blog.API.Dtos;
using Explorer.Blog.Core.Domain.Enums;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Core.Domain
{
    public class BlogRating : ValueObject<BlogRating>
    {
        public long BlogId { get; private set; }
        public long UserId { get; private set; }
        public double Rating { get; private set; }
        public DateTime CreationTime { get; private set; }

        public BlogRating()
        {

        }

        public BlogRating(long blogId,long userId,double rating,DateTime creationTime)
        {
            BlogId = blogId;
            UserId = userId;
            Rating = rating;
            if(creationTime.Date > DateTime.Today)
                throw new Exception("Incorrect date");
            else
                CreationTime = creationTime;
        }

        protected override bool EqualsCore(BlogRating rating)
        {
            return BlogId == rating.BlogId &&
                    UserId == rating.UserId &&
                    Rating == rating.Rating &&
                    CreationTime == rating.CreationTime;
        }

        protected override int GetHashCodeCore()
        {
            throw new NotImplementedException();
        }
    }
}
