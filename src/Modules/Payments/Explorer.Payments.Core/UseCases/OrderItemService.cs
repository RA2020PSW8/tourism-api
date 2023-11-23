using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.API.Internal;
using FluentResults;
using OrderItemDto = Explorer.Payments.API.Dtos.OrderItemDto;
using ShoppingCartDto = Explorer.Payments.API.Dtos.ShoppingCartDto;

namespace Explorer.Payments.Core.UseCases;

public class OrderItemService : CrudService<OrderItemDto, OrderItem>, IOrderItemService
{
    protected readonly IOrderItemRepository _orderItemRepository;

    protected readonly IShoppingCartService _shoppingCartService;
    protected readonly IInternalTourService _tourService;


    public OrderItemService(IOrderItemRepository repository, IMapper mapper,
        IShoppingCartService shoppingCartService, IInternalTourService tourService) : base(repository, mapper)
    {
        _orderItemRepository = repository;
        _shoppingCartService = shoppingCartService;
        _tourService = tourService;
    }

    public override Result<OrderItemDto> Create(OrderItemDto entity)
    {
        try
        {
            var shoppingCart = _shoppingCartService.GetByUser(entity.UserId);
            if (shoppingCart != null)
            {
                foreach (var orderId in shoppingCart.OrdersId)
                {
                    var item = _orderItemRepository.Get(orderId);
                    if (item.TourId == entity.TourId)
                        return Result.Fail(FailureCode.Conflict)
                            .WithError("This tour is already in the shopping cart!");
                }

                shoppingCart.OrdersId.Add(entity.Id);
                var price = _tourService.Get(entity.TourId).Value.Price;
                shoppingCart.Price += price;
                _shoppingCartService.Update(shoppingCart);
            }
            else
            {
                var price = _tourService.Get(entity.TourId).Value.Price;

                var newShoppingCart = new ShoppingCartDto
                {
                    Id = 1,
                    UserId = entity.UserId,
                    Price = price,
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
        var result = GetPaged(page, pageSize);
        var filteredItems = new List<OrderItemDto>();

        foreach (var order in result.ValueOrDefault.Results)
            if (shoppingCart != null)
                foreach (var id in shoppingCart.OrdersId)
                    if (order.UserId == currentUserId && order.Id == id)
                        filteredItems.Add(order);
        return new PagedResult<OrderItemDto>(filteredItems, filteredItems.Count);
    }

    public override Result Delete(int id)
    {
        var orderItem = _orderItemRepository.Get(id);
        var shoppingCart = _shoppingCartService.GetByUser(orderItem.UserId);
        try
        {
            for (var i = shoppingCart.OrdersId.Count - 1; i >= 0; i--)
            {
                var orderId = shoppingCart.OrdersId[i];
                if (id == orderId)
                {
                    shoppingCart.OrdersId.RemoveAt(i);
                    _shoppingCartService.Update(shoppingCart);
                }
            }

            if (shoppingCart.OrdersId.Count == 0) _shoppingCartService.Delete(shoppingCart.Id);
            _orderItemRepository.Delete(id);
            return Result.Ok();
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
    }
}