using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.TourExecution;
using Explorer.Tours.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.TourExecution
{
    public class TourIssueService : CrudService<TourIssueDto, TourIssue>, ITourIssueService
    {
        public TourIssueService(ICrudRepository<TourIssue> crudRepository, IMapper mapper) : base(crudRepository, mapper)
        {
        }
    }
}
