using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.API.Internal
{
    public interface IInternalTourService
    {
        Result<TourDto> Get(int id);
    }
}
