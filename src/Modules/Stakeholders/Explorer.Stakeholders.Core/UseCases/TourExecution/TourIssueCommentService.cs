using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public.TourExecution;
using Explorer.Stakeholders.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases.TourExecution
{
    public class TourIssueCommentService : CrudService<TourIssueCommentDto, TourIssueComment>, ITourIssueCommentService
    {
        public TourIssueCommentService(ICrudRepository<TourIssueComment> crudRepository, IMapper mapper) : base(crudRepository, mapper)
        {
        }
    }
}
