using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.MarketPlace
{
    public interface ITourPreferenceService
    {
        Result<TourPreferenceDto> GetByUser(int userId);
        Result<TourPreferenceDto> Create(TourPreferenceDto tourPreference);
        Result<TourPreferenceDto> Update(TourPreferenceDto tourPreference);
        Result Delete(int userId);
    }
}
