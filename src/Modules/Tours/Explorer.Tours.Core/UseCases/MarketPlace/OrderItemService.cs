using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.MarketPlace;
using Explorer.Tours.API.Public.TourAuthoring;
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
        protected readonly ITourService _tourService;

        public OrderItemService(IOrderItemRepository repository, IMapper mapper, IShoppingCartService shoppingCartService, ITourService tourService) : base(repository, mapper)
        {
            _orderItemRepository = repository; _shoppingCartService = shoppingCartService; _tourService = tourService;
        }

        override public Result<OrderItemDto> Create(OrderItemDto entity)
        {
            try
            {
                
                ShoppingCartDto shoppingCart = _shoppingCartService.GetByUser(entity.UserId);
                if (shoppingCart != null)
                {
                    shoppingCart.OrdersId.Add(entity.Id);
                    TourDto tour = _tourService.GetById(entity.TourId);
                    shoppingCart.Price += tour.Price;
                    _shoppingCartService.Update(shoppingCart); 
                }
                else
                {

                    TourDto tour = _tourService.GetById(entity.TourId);
                    
                    var newShoppingCart = new ShoppingCartDto
                    {
                        Id=1,
                        UserId = entity.UserId,
                        Price = tour.Price,
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
            var r = _shoppingCartService.GetByUser(currentUserId);
            var result = this.GetPaged(page, pageSize);
            var filteredItems = new List<OrderItemDto>();

            foreach (OrderItemDto c in result.ValueOrDefault.Results)
            {
                foreach(int id in r.OrdersId)
                {
                    if (c.UserId == currentUserId && c.Id==id)
                    {
                        filteredItems.Add(c);
                    }
                }
               
            }
            return new PagedResult<OrderItemDto>(filteredItems, filteredItems.Count);
        }
    }
}
