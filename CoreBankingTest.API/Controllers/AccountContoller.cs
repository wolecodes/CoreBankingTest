using CoreBanking.Test.Core.Interfaces;
using CoreBanking.Test.Core.ValueObjects;
using Microsoft.AspNetCore.Mvc;


namespace CoreBankingTest.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAccounts()
        {
            var accounts = await _accountRepository.GetAllAsync();
            return Ok(accounts);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var account = await _accountRepository.GetByIdAsync(new AccountId(id));
            if (account == null)
            {
                return NotFound();
            }
            return Ok(account);
        }
    }
}