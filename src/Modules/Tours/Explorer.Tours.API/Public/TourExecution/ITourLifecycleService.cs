using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.TourExecution
{
    public interface ITourLifecycleService
    {
        Result<TourProgressDto> GetActiveByUser(long userId);
        Result<TourProgressDto> StartTour(long tourId, long userId);
        Result<TourProgressDto> AbandonActiveTour(long userId);
    }
}
