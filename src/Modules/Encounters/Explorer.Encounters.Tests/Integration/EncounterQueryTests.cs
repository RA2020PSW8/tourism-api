﻿using Explorer.API.Controllers;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

using Shouldly;

namespace Explorer.Encounters.Tests.Integration
{
    public class EncounterQueryTests: BaseEncountersIntegrationTest
    {
        public EncounterQueryTests(EncountersTestFactory factory) : base(factory) { }

        [Fact]
        public void Retrieves_all()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = ((ObjectResult)controller.GetAll(0, 0).Result)?.Value as PagedResult<EncounterDto>;

            // Assert
            result.ShouldNotBeNull();
            result.Results.Count.ShouldBe(3);
            result.TotalCount.ShouldBe(3);
        }

        private static EncounterController CreateController(IServiceScope scope)
        {
            return new EncounterController(scope.ServiceProvider.GetRequiredService<IEncounterService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
