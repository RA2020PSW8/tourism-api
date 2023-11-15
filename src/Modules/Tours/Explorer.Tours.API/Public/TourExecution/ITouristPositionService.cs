using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.TourExecution
{
    public interface ITouristPositionService
    {
        Result<TouristPositionDto> GetByUser(long userId); 
        Result<TouristPositionDto> Create(TouristPositionDto touristPosition);
        Result<TouristPositionDto> Update(TouristPositionDto touristPosition);
    }
}
