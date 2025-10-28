using CoreBanking.Test.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace CoreBankingTest.API.Controller
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
        public IActionResult GetAllAccounts()
        {
            var accounts = _accountRepository.GetAllAccounts();
            return Ok(accounts);
        }

        [HttpGet("{id}")]
        public IActionResult GetAccountById(int id)
        {
            var account = _accountRepository.GetAccountById(id);
            if (account == null)
            {
                return NotFound();
            }
            return Ok(account);
        }
    }
}