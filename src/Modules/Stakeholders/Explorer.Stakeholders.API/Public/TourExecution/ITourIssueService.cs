using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Public.TourExecution
{
    public interface ITourIssueService
    {
        public Result<PagedResult<TourIssueDto>> GetPaged(int page, int pageSize);
        public Result<TourIssueDto> Get(int id);
        public Result<TourIssueDto> Create(TourIssueDto entity);
        public Result<TourIssueDto> Update(TourIssueDto entity);
        public Result Delete(int id);
        public Result<PagedResult<TourIssueDto>> GetByUserPaged(int page, int pageSize, int id);
    }
}
