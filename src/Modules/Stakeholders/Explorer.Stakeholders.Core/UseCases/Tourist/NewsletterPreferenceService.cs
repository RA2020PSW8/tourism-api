using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public.Tourist;
using Explorer.Stakeholders.Core.Domain;

namespace Explorer.Stakeholders.Core.UseCases.Tourist;

public class NewsletterPreferenceService : CrudService<NewsletterPreferenceDto, NewsletterPreference>, INewsletterPreferenceService
{
    public NewsletterPreferenceService(ICrudRepository<NewsletterPreference> crudRepository, IMapper mapper) : base(crudRepository, mapper)
    {
    }
}
