using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases
{
    public class TouristEquipmentService : CrudService<TouristEquipmentDto, TouristEquipment>, ITouristEquipmentService
    {
        private readonly ITouristEquipmentRepository _touristEquipmentRepository;

        public TouristEquipmentService(ITouristEquipmentRepository touristEquipmentRepository, ICrudRepository<TouristEquipment> repository, IMapper mapper) : base(repository, mapper)
        {
            _touristEquipmentRepository = touristEquipmentRepository;
        }

        public Result<TouristEquipmentDto> ItemSelection(TouristEquipmentDto dto)
        {
            TouristEquipment foundTouristEquipment = _touristEquipmentRepository.GetByTouristAndEquipment(dto.TouristId, dto.EquipmentId);
            if (foundTouristEquipment == null)
            {
                return Create(dto);
            }
            return Delete((int)foundTouristEquipment.Id);
        }

    }
}
