using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestServive.One.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestOneController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public IActionResult Gettest() => Ok("it is ok for One api!");

    }
}
