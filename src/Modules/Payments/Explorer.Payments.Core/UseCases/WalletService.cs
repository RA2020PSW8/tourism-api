using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
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

        public WalletService(IWalletRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _walletRepository = repository;
        }

        

        public WalletDto GetByUser(int userId)
        {
            var cart = _walletRepository.GetByUser(userId);
            return MapToDto(cart);
        }
    }
}
