using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;

namespace Explorer.Tours.Core.UseCases.Administration;

public class EquipmentService : CrudService<EquipmentDto, Equipment>, IEquipmentService
{
    private readonly ITouristEquipmentRepository _touristEquipmentRepository;
    private readonly IEquipmentRepository _equipmentRepository;
    public EquipmentService(ICrudRepository<Equipment> repository, IMapper mapper, IEquipmentRepository equipmentRepository, ITouristEquipmentRepository touristEquipmentRepository) : base(repository, mapper) {
        _touristEquipmentRepository = touristEquipmentRepository;
        _equipmentRepository = equipmentRepository;
    }

    public IEnumerable<EquipmentForSelectionDto> GetAllForSelected(int userId) 
    {
        IEnumerable<Equipment> equipment = _equipmentRepository.GetAll();
        List<EquipmentForSelectionDto> dtosForSelection = new List<EquipmentForSelectionDto>();
        foreach (var equipmentItem in equipment)
        {
            bool isSelected = _touristEquipmentRepository.GetByTouristAndEquipment(userId, equipmentItem.Id) != null;
            EquipmentForSelectionDto dto = new EquipmentForSelectionDto((int)equipmentItem.Id, equipmentItem.Name, equipmentItem.Description, isSelected);
            dtosForSelection.Add(dto);
        }

        return dtosForSelection;
    }
}