using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.MarketPlace
{
    public interface IOrderItemService
    {
        Result<PagedResult<OrderItemDto>> GetPaged(int page, int pageSize);
        Result<OrderItemDto> Create(OrderItemDto orderItem);
        Result<OrderItemDto> Update(OrderItemDto orderItem);
        PagedResult<OrderItemDto> GetAllByUser(int page, int pageSize, int userId);
    }
}
