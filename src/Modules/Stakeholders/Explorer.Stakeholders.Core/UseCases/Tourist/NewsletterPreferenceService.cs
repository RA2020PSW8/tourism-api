using AutoMapper;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public.Tourist;
using Explorer.Stakeholders.Core.Domain;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases.Tourist;

public class NewsletterPreferenceService : CrudService<NewsletterPreferenceDto, NewsletterPreference>, INewsletterPreferenceService
{
    public NewsletterPreferenceService(ICrudRepository<NewsletterPreference> crudRepository, IMapper mapper) : base(crudRepository, mapper)
    {

    }

    public override Result<NewsletterPreferenceDto> Create(NewsletterPreferenceDto np)
    {
        try
        {
            try
            {
                var exists = CrudRepository.Get(np.UserID);
                exists.Frequency = np.Frequency;
                exists.LastSent = np.LastSent;
                return MapToDto(CrudRepository.Update(exists));
            }
            catch (KeyNotFoundException) 
            {
                return MapToDto(CrudRepository.Create(MapToDomain(np)));
            }
                
        }
        catch (ArgumentException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
    }
}
