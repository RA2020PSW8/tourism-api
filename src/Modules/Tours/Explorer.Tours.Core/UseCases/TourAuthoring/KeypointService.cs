using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.TourAuthoring;
using Explorer.Tours.Core.Domain;
using FluentResults;

namespace Explorer.Tours.Core.UseCases.TourAuthoring;

public class KeypointService : CrudService<KeypointDto, Keypoint>, IKeypointService
{
    public KeypointService(ICrudRepository<Keypoint> crudRepository, IMapper mapper) : base(crudRepository, mapper)
    {
    }
    
    public Result<List<KeypointDto>> CreateMultiple(List<KeypointDto> keypoints)
    {
        List<KeypointDto> results = new List<KeypointDto>();
        foreach (var keypoint in keypoints)
        {
            var result = CrudRepository.Create(MapToDomain(keypoint));
            results.Add(MapToDto(result));
        }

        return results;
    }
}