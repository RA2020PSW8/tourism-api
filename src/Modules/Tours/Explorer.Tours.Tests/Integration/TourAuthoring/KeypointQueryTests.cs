using Explorer.API.Controllers.Author;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.TourAuthoring;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Tours.Tests.Integration.TourAuthoring;

[Collection("Sequential")]
public class KeypointQueryTests : BaseToursIntegrationTest
{
    public KeypointQueryTests(ToursTestFactory factory) : base(factory) {}

    [Fact]
    public void Retrieves_all()
    {
        //Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);

        // Act
        var result = ((ObjectResult)controller.GetAll(0, 0).Result)?.Value as PagedResult<KeypointDto>;

        // Assert
        result.ShouldNotBeNull();
        result.Results.Count.ShouldBe(3);
        result.TotalCount.ShouldBe(3);
    }
    
    private static KeypointController CreateController(IServiceScope scope)
    {
        return new KeypointController(scope.ServiceProvider.GetRequiredService<IKeypointService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}