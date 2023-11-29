using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.UseCases;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Infrastructure.Authentication;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/wallet")]
    public class WalletController: BaseApiController
    {
        private readonly IWalletService _walletService;
        private readonly IProfileService _userService;

        public WalletController(IWalletService walletService, IProfileService userService) { _walletService = walletService; _userService = userService; }

        [HttpGet("byUser")]
        public ActionResult<PagedResult<WalletDto>> GetByUser()
        {
            int personId = User.PersonId();
            var person = _userService.Get(personId);
            long userId = person.UserId;
            var result = _walletService.GetByUser((int)userId);
            var resultValue = Result.Ok(result);
            return CreateResponse(resultValue);
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<PagedResult<WalletDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _walletService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<WalletDto> Create([FromBody] WalletDto wallet)
        {
            var result = _walletService.Create(wallet);
            return CreateResponse(result);
        }
        [HttpPut("{id:int}")]
        public ActionResult<WalletDto> Update([FromBody] WalletDto wallet)
        {
            var result = _walletService.Update(wallet);
            return CreateResponse(result);
        }
    }
}
