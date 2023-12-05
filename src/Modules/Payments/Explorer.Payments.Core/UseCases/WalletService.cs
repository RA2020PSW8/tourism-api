using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.API.Internal;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.UseCases
{
    public class WalletService: CrudService<WalletDto, Wallet>, IWalletService
    {
        protected readonly IWalletRepository _walletRepository;
        protected readonly IInternalNotificationService _notificationService;

        public WalletService(IWalletRepository repository, IMapper mapper, IInternalNotificationService notificationService) : base(repository, mapper)
        {
            _walletRepository = repository;
            _notificationService = notificationService;
        }

        

        public WalletDto GetByUser(int userId)
        {
            var cart = _walletRepository.GetByUser(userId);
            return MapToDto(cart);
        }

        public Result<WalletDto> AddCoins(WalletDto updatedWalletDto)
        {
            try
        {
            var existingWallet = _walletRepository.Get(updatedWalletDto.Id);

            if (existingWallet == null) return Result.Fail("Wallet not found.");

                existingWallet.AdventureCoins = updatedWalletDto.AdventureCoins;
                _walletRepository.Update(existingWallet);
                var url = "/wallet/byUser";
                _notificationService.Generate(updatedWalletDto.UserId,Stakeholders.API.Dtos.Enums.NotificationType.COINS_GIFTED,url, DateTime.UtcNow, "");
            return Result.Ok(new WalletDto
            {
                UserId = existingWallet.UserId,
                AdventureCoins = existingWallet.AdventureCoins
            });
        }
        catch (Exception ex)
        {
            return Result.Fail($"An error occurred while updating the wallet: {ex.Message}");
        }
        }
    }
}
