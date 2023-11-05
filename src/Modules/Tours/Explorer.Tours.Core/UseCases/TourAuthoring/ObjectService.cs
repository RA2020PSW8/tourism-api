using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.TourAuthoring;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.TourAuthoring
{
    public class ObjectService : CrudService<ObjectDto, Domain.Object>, IObjectService
    {
        /*public ObjectService(ICrudRepository<Domain.Object> crudRepository, IMapper mapper) : base(crudRepository, mapper)
        {

        }*/

        protected readonly IObjectRepository _objectRepository;
        protected readonly IMapper _mapper;

        public ObjectService(IObjectRepository objectRepository, IMapper mapper) : base(objectRepository, mapper) 
        {
            _objectRepository = objectRepository;
            _mapper = mapper;   
        }
    }
}
