using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.TourAuthoring
{
    public class ObjectService : CrudService<ObjectDto, Domain.Object>, IObjectService
    {
        public ObjectService(ICrudRepository<Domain.Object> crudRepository, IMapper mapper) : base(crudRepository, mapper)
        {

        }
    }
}
