using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThapMuoi.Exceptions;
using ThapMuoi.Helpers;
using ThapMuoi.Interfaces.Auth;
using ThapMuoi.Models.Auth;
using ThapMuoi.Models.Settings;

namespace ThapMuoi.Controllers.Auth
{
    [Route("api/v1/[controller]")]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly AppApiKey _appApiKey;
        public AuthController(IIdentityService identityService, AppApiKey appApiKey)
        {
            _identityService = identityService;
            _appApiKey = appApiKey;

        }
        
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] AuthRequest user)
        {
            try
            {
                var response = await _identityService.LoginAsync(user);
                
                return Ok(
                    new ResultMessageResponse()
                        .WithData(response)
                        .WithCode(DefaultCode.SUCCESS)
                        .WithMessage("Đăng nhập thành công")
                );
            }
            catch (ResponseMessageException ex)
            {
                return Ok(
                    new ResultMessageResponse().WithCode(ex.ResultCode)
                        .WithMessage(ex.ResultString)
                );
            }
        }
        
    }
}