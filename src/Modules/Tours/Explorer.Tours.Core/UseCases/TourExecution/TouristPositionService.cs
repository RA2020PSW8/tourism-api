using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.TourExecution;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.TourExecution
{
    public class TouristPositionService : CrudService<TouristPositionDto, TouristPosition>, ITouristPositionService
    {
        protected readonly ITouristPositionRepository _touristPositionRepository;

        public TouristPositionService(ITouristPositionRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _touristPositionRepository = repository;
        }

        public Result<TouristPositionDto> GetByUser(long userId)
        {
            try
            {
                var result = _touristPositionRepository.GetByUser(userId);
                return MapToDto(result);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        override public Result<TouristPositionDto> Create(TouristPositionDto entity)
        {
            try
            {
                var existingTouristPosition = _touristPositionRepository.GetByUser(entity.UserId);
                return Result.Fail(FailureCode.Conflict).WithError("Tourist position for this user already exists");
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
            catch (KeyNotFoundException e)
            {
                var result = _touristPositionRepository.Create(MapToDomain(entity));
                return MapToDto(result);
            }
        }

        override public Result<TouristPositionDto> Update(TouristPositionDto entity)
        {
            try
            {
                var foundTouristPosition = _touristPositionRepository.GetByUser(entity.UserId);
                entity.Id = foundTouristPosition.Id;
                return base.Update(entity);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }
    }
}
