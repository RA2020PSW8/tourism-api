using AutoMapper;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public.TourExecution;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases.TourExecution
{
    public class TourIssueService : CrudService<TourIssueDto, TourIssue>, ITourIssueService
    {
        private readonly ITourIssueRepository _repo;
        public TourIssueService(ITourIssueRepository crudRepository, IMapper mapper) : base(crudRepository, mapper)
        {
            _repo = crudRepository;
        }

        public Result<PagedResult<TourIssueDto>> GetByUserPaged(int page, int pageSize, int id)
        {
            var result = _repo.GetByUserPaged(page, pageSize, id);
            return MapToDto(result);
        }
    }
}
