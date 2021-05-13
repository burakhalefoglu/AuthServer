using AuthServer.Core.DTOs;
using SharedLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Core.Service
{
    public interface IAuthService
    {
        Task<ResponseDto<TokenDto>> CreateTokenAsync(LoginDto loginDto);
        Task<ResponseDto<TokenDto>> CreateTokenbyRefreshTokenAsync(string refreshToken);
        Task<ResponseDto<NoDataContent>> revokeRefreshtoken(string refreshToken);
        Task<ResponseDto<ClientTokenDto>> revokeRefreshtoken(ClientDto clientDto);


    }
}
