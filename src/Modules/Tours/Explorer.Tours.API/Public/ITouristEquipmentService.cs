using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public
{
    public interface ITouristEquipmentService
    {
        Result<TouristEquipmentDto> Create(TouristEquipmentDto equipment);
        Result Delete(TouristEquipmentDto equipment);
    }
}
