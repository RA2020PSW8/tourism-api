using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.MarketPlace;
using Explorer.Tours.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.MarketPlace
{
    public class TourPreferenceService : CrudService<TourPreferenceDto, TourPreference>, ITourPreferenceService
    {
        public TourPreferenceService(ICrudRepository<TourPreference> repository, IMapper mapper) : base(repository, mapper) { }
    }
}
