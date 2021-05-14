using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestService.Two.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestTwoController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public IActionResult Gettest() => Ok("it is ok for Two api!");
    }
}
