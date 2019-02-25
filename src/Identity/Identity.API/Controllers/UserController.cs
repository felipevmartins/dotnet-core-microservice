using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Api.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserContext _userContext;

        public UserController(UserContext userContext)
        {
            _userContext = userContext;
        }

        [HttpPost]
        public void Post([FromBody] User user)
        {
            _userContext.Add(user);
            _userContext.SaveChanges();
        }
    }
}