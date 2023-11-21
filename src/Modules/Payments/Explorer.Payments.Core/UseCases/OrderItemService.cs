using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Payments.Core.UseCases;

public class OrderItemService: CrudService<OrderItemDto, OrderItem>, IOrderItemService
    {
        protected readonly IOrderItemRepository _orderItemRepository;
        protected readonly IShoppingCartService _shoppingCartService;
        //protected readonly ITourService _tourService;


        public OrderItemService(IOrderItemRepository repository, IMapper mapper, IShoppingCartService shoppingCartService/* ITourService tourService*/) : base(repository, mapper)
        {
            _orderItemRepository = repository; _shoppingCartService = shoppingCartService; //_tourService = tourService;
        }

        override public Result<OrderItemDto> Create(OrderItemDto entity)
        {
            try
            {
                ShoppingCartDto shoppingCart = new ShoppingCartDto();//_shoppingCartService.GetByUser(entity.UserId);
                if (shoppingCart != null)
                {
                    foreach (int orderId in shoppingCart.OrdersId)
                    {
                        OrderItem item = _orderItemRepository.Get(orderId);
                        if (item.TourId == entity.TourId)
                        {
                            return Result.Fail(FailureCode.Conflict)
                                .WithError("This tour is already in the shopping cart!");
                        }
                    }
                    shoppingCart.OrdersId.Add(entity.Id);
                    //TourDto tour = _tourService.GetById(entity.TourId);
                    shoppingCart.Price += 1;//tour.Price;
                    _shoppingCartService.Update(shoppingCart);
                }
                else
                {
                    //TourDto tour = new TourDto(); //_tourService.GetById(entity.TourId);

                    var newShoppingCart = new ShoppingCartDto
                    {
                        Id = 1,
                        UserId = entity.UserId,
                        Price = 5,//tour.Price,
                        OrdersId = new List<int> { entity.Id }
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
            var shoppingCart = _shoppingCartService.GetByUser(currentUserId);
            var result = this.GetPaged(page, pageSize);
            var filteredItems = new List<OrderItemDto>();

            foreach (OrderItemDto order in result.ValueOrDefault.Results)
                if (shoppingCart != null)
                {
                    foreach (int id in shoppingCart.OrdersId)
                    {
                        if (order.UserId == currentUserId && order.Id == id)
                        {
                            filteredItems.Add(order);
                        }
                    }
                }
            return new PagedResult<OrderItemDto>(filteredItems, filteredItems.Count);
        }
        override public Result Delete(int id)
        {
            OrderItem orderItem = _orderItemRepository.Get(id);
            ShoppingCartDto shoppingCart = _shoppingCartService.GetByUser(orderItem.UserId);
            try
            {

                for (int i = shoppingCart.OrdersId.Count - 1; i >= 0; i--)
                {
                    int orderId = shoppingCart.OrdersId[i];
                    if (id == orderId)
                    {
                        shoppingCart.OrdersId.RemoveAt(i);
                        _shoppingCartService.Update(shoppingCart);
                    }
                }
                if(shoppingCart.OrdersId.Count == 0)
                {
                    _shoppingCartService.Delete(shoppingCart.Id);
                }
                _orderItemRepository.Delete(id);
                return Result.Ok();
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }
    }