using AuthServer.Core.DTOs;
using SharedLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Core.Service
{
    public interface IUserService
    {
        Task<ResponseDto<UserDto>> CreateUserAsync(CreateUserDto createUserDto);

        Task<ResponseDto<UserDto>> GetUserByNameAsync(string userName);

    }
}
