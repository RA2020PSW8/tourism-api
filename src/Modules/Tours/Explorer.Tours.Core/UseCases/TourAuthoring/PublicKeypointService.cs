using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.TourAuthoring;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.TourAuthoring
{
    public class PublicKeypointService : CrudService<PublicKeypointDto, PublicKeypoint>, IPublicKeypointService
    {
        protected readonly IPublicKeypointRepository _publicKeypointRepository;

        public PublicKeypointService(IPublicKeypointRepository publicKeypointRepository, IMapper mapper) : base(publicKeypointRepository, mapper)
        {
            _publicKeypointRepository = publicKeypointRepository;
        }
    }
}
