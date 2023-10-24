using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class ApplicationRatingService : CrudService<ApplicationRatingDto, ApplicationRating>, IApplicationRatingService
    {
        public ApplicationRatingService(ICrudRepository<ApplicationRating> crudRepository, IMapper mapper) : base(crudRepository, mapper) 
        { 
        }

        public Result<PagedResult<ApplicationRatingDto>> GetByUserId(int page, int pageSize, int userId)
        {
            try
            {
                var result = GetPaged(page, pageSize);
                result.Value.Results.Where(r => r.UserId == userId).ToList();
                
                return result;
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }
    }
}
