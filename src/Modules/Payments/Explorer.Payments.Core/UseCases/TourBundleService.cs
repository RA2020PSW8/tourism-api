using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.UseCases
{
    public class TourBundleService : CrudService<TourBundleDto, TourBundle>, ITourBundleService
    {
        private readonly ITourBundleRepository _repository;
        public TourBundleService(ITourBundleRepository repository, IMapper mapper): base(repository,mapper) 
        {
            _repository = repository;
        }

        public Result<TourBundleDto> Create(TourBundleDto bundle)
        {
            var result = _repository.Create(new TourBundle(bundle.Id, bundle.Name, bundle.TotalPrice, bundle.Status));
            return MapToDto(result);
        }


        public Result<PagedResult<TourBundleDto>> GetPaged(int page, int pageSize)
        {
            var result = _repository.GetPaged(page, pageSize);
            return MapToDto(result);
        }

        public Result<TourBundleDto> Get (long id)
        {
            var result = _repository.Get(id);
            return MapToDto(result);
        }
        Result<TourBundleDto> Update(TourBundleDto newBundle) 
        {
            var existingTour = _repository.Get(newBundle.Id);
            return MapToDto(_repository.Update(existingTour));
        }

        Result Delete(int id)
        {
            try
            {
                _repository.Delete(id);
                return Result.Ok();
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }
    }
}
