using AuthServer.Core.DTOs;
using AuthServer.Core.Entities;
using AuthServer.Core.Service;
using Microsoft.AspNetCore.Identity;
using SharedLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Business
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ResponseDto<UserDto>> CreateUserAsync(CreateUserDto createUserDto)
        {
            var user = new User { Email = createUserDto.Email, UserName = createUserDto.UserName };

            var result = await _userManager.CreateAsync(user, createUserDto.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();

                return ResponseDto<UserDto>.Fail(new ErrorDto(errors, true), 400);
            }
            return ResponseDto<UserDto>.Success(ObjectMapper.Mapper.Map<UserDto>(user), 200);
        }

        public async Task<ResponseDto<UserDto>> GetUserByNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return ResponseDto<UserDto>.Fail("UserName not found", 404, true);
            }

            return ResponseDto<UserDto>.Success(ObjectMapper.Mapper.Map<UserDto>(user), 200);
        }
    }
}
