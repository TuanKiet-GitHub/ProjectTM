using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThapMuoi.Interfaces.Core;

namespace ThapMuoi.Controllers.Core
{
    [Route("api/v1/[controller]")]
     [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
    }
}