﻿using Explorer.API.Controllers.Tourist;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Infrastructure.Database;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Tests.Integration;
[Collection("Sequential")]
public class WalletCommandTests: BasePaymentsIntegrationTest
{
    public WalletCommandTests(PaymentsTestFactory factory): base(factory) { }

    [Fact]
    public void Creates()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();
        var newEntity = new WalletDto
        {
            Id = 1,
            UserId = 2,
            AdventureCoins = 0
        };

        // Act
        var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as WalletDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldNotBe(-1);

        // Assert - Database
        var storedEntity = dbContext.Wallets.FirstOrDefault(i => i.UserId == newEntity.UserId);
        storedEntity.ShouldNotBeNull();
        storedEntity.Id.ShouldBe(result.Id);
    }
    

    [Fact]
    public void Updates()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();
        var updatedEntity = new WalletDto
        {
            Id = -1,
            UserId = -2,
            AdventureCoins = 0
        };

        // Act
        var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as WalletDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldBe(-1);
        result.UserId.ShouldBe(updatedEntity.UserId);
        result.AdventureCoins.ShouldBe(updatedEntity.AdventureCoins);

        // Assert - Database
        var storedEntity = dbContext.Wallets.FirstOrDefault(i => i.UserId == -2);
        storedEntity.ShouldNotBeNull();
        storedEntity.AdventureCoins.ShouldBe(updatedEntity.AdventureCoins);
        var oldEntity = dbContext.Wallets.FirstOrDefault(i => i.UserId == -11);
        oldEntity.ShouldBeNull();
    }

    [Fact]
    public void Update_fails_invalid_id()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var updatedEntity = new WalletDto
        {
            Id = -1000,
            AdventureCoins = 5
        };

        // Act
        var result = (ObjectResult)controller.Update(updatedEntity).Result;

        // Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(404);
    }


    private static WalletController CreateController(IServiceScope scope)
    {
        return new WalletController(scope.ServiceProvider.GetRequiredService<IWalletService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}