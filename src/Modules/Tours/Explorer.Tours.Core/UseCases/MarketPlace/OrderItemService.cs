using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.MarketPlace;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.MarketPlace
{
    public class OrderItemService: CrudService<OrderItemDto, OrderItem>, IOrderItemService
    {
        

        public OrderItemService(ICrudRepository<OrderItem> repository, IMapper mapper) : base(repository, mapper) {}

        public PagedResult<OrderItemDto> GetAllByUser(int page, int pageSize, int currentUserId)
        {
            var result = this.GetPaged(page, pageSize);
            var filteredItems = new List<OrderItemDto>();

            foreach (OrderItemDto c in result.ValueOrDefault.Results)
            {
                if (c.UserId == currentUserId)
                {
                    filteredItems.Add(c);
                }
            }

            return new PagedResult<OrderItemDto>(filteredItems, filteredItems.Count);
        }
    }
}
