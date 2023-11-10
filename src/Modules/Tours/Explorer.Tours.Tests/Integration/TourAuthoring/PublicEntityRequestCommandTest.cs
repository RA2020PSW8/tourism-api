using Explorer.API.Controllers.Author;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.TourAuthoring;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Tests.Integration.TourAuthoring;

public class PublicEntityRequestCommandTest : BaseToursIntegrationTest
{
    public PublicEntityRequestCommandTest(ToursTestFactory factory) : base(factory)
    {
    }

    [Fact]
    public void Creates_request_for_object()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
        var newEntity = new PublicEntityRequestDto()
        {
            UserId = -11,
            EntityId = -3,
            EntityType = (API.Dtos.Enums.EntityType)1, //object
            Status = 0,
            Comment = ""
        };

        // Act
        var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as PublicEntityRequestDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldNotBe(0);
        result.EntityId.ShouldBe(newEntity.EntityId);
        result.EntityType.ShouldBe(newEntity.EntityType);


        // Assert - Database
        var storedEntity = dbContext.PublicEntityRequests.FirstOrDefault(i => i.EntityId == newEntity.EntityId && i.EntityType == Core.Domain.Enum.EntityType.OBJECT);
        storedEntity.ShouldNotBeNull();
        storedEntity.Id.ShouldBe(result.Id);
    }

    [Fact]
    public void Creates_request_for_keypoint()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
        var newEntity = new PublicEntityRequestDto()
        {
            UserId = -11,
            EntityId = -3,
            EntityType = (API.Dtos.Enums.EntityType)0, //keypoint
            Status = 0,
            Comment = ""
        };

        // Act
        var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as PublicEntityRequestDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldNotBe(0);
        result.EntityId.ShouldBe(newEntity.EntityId);
        result.EntityType.ShouldBe(newEntity.EntityType);


        // Assert - Database
        var storedEntity = dbContext.PublicEntityRequests.FirstOrDefault(i => i.EntityId == newEntity.EntityId && i.EntityType == Core.Domain.Enum.EntityType.KEYPOINT);
        storedEntity.ShouldNotBeNull();
        storedEntity.Id.ShouldBe(result.Id);

        var keypointToPublic = dbContext.Keypoints.FirstOrDefault(k => k.Id == newEntity.EntityId);

        var createdPublicKeypoint = dbContext.PublicKeyPoints.FirstOrDefault(kp => kp.Name == keypointToPublic.Name && kp.Longitude == keypointToPublic.Longitude && kp.Latitude == keypointToPublic.Latitude);
        createdPublicKeypoint.ShouldNotBeNull();
        createdPublicKeypoint.Name.ShouldBe(keypointToPublic.Name);
    }

    [Fact]
    public void Accepts_keypoint_request()
    {

    }

    [Fact]
    public void Accepts_object_request()
    {

    }

    [Fact]
    public void Declines_keypoint_request()
    {

    }

    [Fact]
    public void Declines_object_request()
    {

    }

    public static PublicEntityRequestController  CreateController(IServiceScope scope)
    {
        return new PublicEntityRequestController(scope.ServiceProvider.GetRequiredService<IPublicEntityRequestService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }

}



