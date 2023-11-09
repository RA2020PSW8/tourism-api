using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.Enums;
using Explorer.Tours.API.Public.TourAuthoring;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.TourAuthoring
{
    public class PublicEntityRequestService : CrudService<PublicEntityRequestDto, PublicEntityRequest>, IPublicEntityRequestService
    {
        protected readonly IPublicEntityRequestRepository _publicEntityRequestRepository;
        protected readonly IKeypointRepository _keypointRepository;
        protected readonly IPublicKeypointRepository _publicKeypointRepository;
        protected readonly IObjectRepository _objectRepository;
        private readonly IMapper _mapper;

        public PublicEntityRequestService(IPublicEntityRequestRepository publicEntityRequestRepository, IKeypointRepository keypointRepository, IObjectRepository objectRepository, IPublicKeypointRepository publicKeypointRepository, IMapper mapper) : base(publicEntityRequestRepository, mapper)
        {
            _publicEntityRequestRepository = publicEntityRequestRepository;
            _keypointRepository = keypointRepository;
            _publicKeypointRepository = publicKeypointRepository;
            _objectRepository = objectRepository;
            _mapper = mapper;
        }

        public Result<PublicEntityRequestDto> Approve(PublicEntityRequestDto publicEntityRequestDto)
        {
            // var request = _publicEntityRequestRepository.Get(id);
            //var requestDto = _mapper.Map<PublicEntityRequestDto>(request);

            if (publicEntityRequestDto == null)
            {
                return Result.Fail("Request not found.");
            }

            if (publicEntityRequestDto.Status is PublicEntityRequestStatus.APPROVED or PublicEntityRequestStatus.DECLINED)
            {
                return Result.Fail("Request already approved or declined.");
            }

            if (publicEntityRequestDto.Status is PublicEntityRequestStatus.PENDING)
            {
                if (publicEntityRequestDto.EntityType is EntityType.KEYPOINT)
                {
                    var keypoint = _keypointRepository.Get(publicEntityRequestDto.EntityId);

                    if (keypoint != null)
                    {
                        var publicKeypoint = new PublicKeypoint
                        {
                            Name = keypoint.Name,
                            Latitude = keypoint.Latitude,
                            Longitude = keypoint.Longitude,
                            Description = keypoint.Description,
                            Position = keypoint.Position,
                            Image = keypoint.Image
                        };

                        _publicKeypointRepository.Create(publicKeypoint);
                    }
                }
                else if (publicEntityRequestDto.EntityType is EntityType.OBJECT)
                {
                    var tourObject = _objectRepository.Get(publicEntityRequestDto.EntityId);
                    if (tourObject != null)
                    {
                        tourObject.Status = Domain.Enum.ObjectStatus.PUBLIC;
                        _objectRepository.Update(tourObject);
                    }
                }

                publicEntityRequestDto.Status = PublicEntityRequestStatus.APPROVED;


                var request = MapToDomain(publicEntityRequestDto);
                _publicEntityRequestRepository.Update(request);

                return Result.Ok(_mapper.Map<PublicEntityRequestDto>(request));
            }
            return Result.Fail("Invalid request status.");
        }

        public Result<PublicEntityRequestDto> Decline(PublicEntityRequestDto publicEntityRequestDto)
        {
            if (publicEntityRequestDto == null)
            {
                return Result.Fail("Request not found.");
            }

            if (publicEntityRequestDto.Status is PublicEntityRequestStatus.PENDING)
            {
                publicEntityRequestDto.Status = PublicEntityRequestStatus.DECLINED;

                var request = MapToDomain(publicEntityRequestDto);
                _publicEntityRequestRepository.Update(request);

                return Result.Ok(publicEntityRequestDto);
            }

            if (publicEntityRequestDto.Status is PublicEntityRequestStatus.APPROVED or PublicEntityRequestStatus.DECLINED)
            {
                return Result.Fail("Request already approved or declined.");
            }
            return Result.Fail("Invalid request status.");
        }

        public Result<PublicEntityRequestDto> GetByEntityId(int entityId, EntityType entityType)
        {
            var result = _publicEntityRequestRepository.GetByEntityId(entityId, entityType);
            return MapToDto(result);
        }
    }
}
