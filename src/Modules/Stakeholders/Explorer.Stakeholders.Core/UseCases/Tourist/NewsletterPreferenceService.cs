using AutoMapper;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Internal;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.API.Public.Tourist;
using Explorer.Stakeholders.Core.Domain;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases.Tourist;

public class NewsletterPreferenceService : CrudService<NewsletterPreferenceDto, NewsletterPreference>, INewsletterPreferenceService
{
    private IUserService _userService;
    private IInternalEmailService _internalEmailService;
    private ICrudRepository<NewsletterPreference> _newsletterPreferenceRepository;
    public NewsletterPreferenceService(ICrudRepository<NewsletterPreference> crudRepository, IMapper mapper,
        IUserService userService, IInternalEmailService emailService) : base(crudRepository, mapper)
    {
        _userService = userService; 
        _internalEmailService = emailService;
        _newsletterPreferenceRepository = crudRepository;
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

    public List<NewsletterPreferenceDto> CheckCandidatesForNewsletter()
    {
        var newsletters = _newsletterPreferenceRepository.GetPaged(0, 0).Results;
        List<NewsletterPreferenceDto> validNl = new List<NewsletterPreferenceDto>();
        foreach(NewsletterPreference nl in newsletters)
        {
            if(nl.LastSent.AddDays(nl.Frequency) < DateTime.Now && nl.Frequency != 0)
            {
                validNl.Add(MapToDto(nl));
            }
        }
        return validNl;
    }
}
