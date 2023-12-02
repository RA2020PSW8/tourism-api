using Explorer.Stakeholders.API.Dtos;
using FluentResults;

namespace Explorer.Stakeholders.API.Internal;

public interface IInternalProfileService
{
    Result<List<PersonDto>> GetMany(List<int> peopleIds);
}