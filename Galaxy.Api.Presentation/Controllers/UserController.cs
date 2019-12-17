using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Galaxy.Api.Core.Interfaces;
using Galaxy.Api.Core.Models;
using Galaxy.Api.Core.Models.UserModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Galaxy.Api.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }
        
        [HttpGet("activate")]
        public async Task<IActionResult> Activate(string activationToken)
        {
            var response = await _userService.ActivateAsync(activationToken);
            return GenerateResult(response);
        }
        
        private IActionResult GenerateResult(UserActionResponse response)
        {
            if (response.Success) return Ok();
            
            return new ObjectResult(new OperationFailedViewModel
            {
                Errors = response.Errors
            })
            {
                StatusCode = (int) HttpStatusCode.BadRequest
            };
        }
    }
}