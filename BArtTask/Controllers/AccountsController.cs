using BArtTask.Contracts;
using BArtTask.Data;
using BArtTask.Models;
using BArtTask.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BArtTask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] AccountRequest request)
        {
            await _accountService.Create(request);
            return Ok();
        }
    }
}
