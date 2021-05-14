using Microsoft.AspNetCore.Mvc;
using SharedLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary
{
    public class CustomBaseController : ControllerBase
    {
        public IActionResult ActionResultInstance<T>(ResponseDto<T> responseDto) where T : class
        {
            return new ObjectResult(responseDto)
            {
                StatusCode = responseDto.StatuseCode
            };
        }
    }
}
