using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.MarketPlace;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.MarketPlace
{
    public class OrderItemService: CrudService<OrderItemDto, OrderItem>, IOrderItemService
    {
        protected readonly IOrderItemRepository _orderItemRepository; 
        protected readonly IShoppingCartService _shoppingCartService;

        public OrderItemService(IOrderItemRepository repository, IMapper mapper, IShoppingCartService shoppingCartService) : base(repository, mapper)
        {
            _orderItemRepository = repository; _shoppingCartService = shoppingCartService;
        }

        override public Result<OrderItemDto> Create(OrderItemDto entity)
        {
            try
            {
                
                ShoppingCartDto shoppingCart = _shoppingCartService.GetByUser(entity.UserId);
                if (shoppingCart != null)
                {
                    shoppingCart.OrdersId.Add(entity.Id);
                    _shoppingCartService.Update(shoppingCart); 
                }
                else
                {

                   
                    var newShoppingCart = new ShoppingCartDto
                    {
                        Id=0,
                        UserId = entity.UserId,
                        OrdersId = new List<int> {entity.Id }
                    };
                    _shoppingCartService.Create(newShoppingCart);
                }

                var result = CrudRepository.Create(MapToDomain(entity));
                return MapToDto(result);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
            
        }


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
