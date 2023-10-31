using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.TourAuthoring
{
    public class PublicEntityRequestService : CrudService<PublicEntityRequestDto, PublicEntityRequest>, IPublicEntityRequestService
    {
        public PublicEntityRequestService(ICrudRepository<PublicEntityRequest> crudRepository, IMapper mapper) : base(crudRepository, mapper)
        {
        }
    }
}
