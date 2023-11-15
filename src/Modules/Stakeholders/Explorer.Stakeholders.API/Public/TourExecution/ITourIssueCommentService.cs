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
    public interface ITourIssueCommentService
    {
        public Result<PagedResult<TourIssueCommentDto>> GetPaged(int page, int pageSize);
        public Result<TourIssueCommentDto> Get(int id);
        public Result<TourIssueCommentDto> Create(TourIssueCommentDto entity);
        public Result<TourIssueCommentDto> Update(TourIssueCommentDto entity);
        public Result Delete(int id);
    }
}
