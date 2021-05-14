using AuthServer.Core.Configuration;
using AuthServer.Core.DTOs;
using AuthServer.Core.Entities;
using AuthServer.Core.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SharedLibrary.Configurations;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Business
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<User> userManager;

        private readonly CustomTokenOption customTokenOptions;

        public TokenService(UserManager<User> userManager, IOptions<CustomTokenOption> options)
        {
            this.userManager = userManager;
            this.customTokenOptions = options.Value;
        }


        private IEnumerable<Claim> GetClaims(User user, List<string> audience)
        {
            var userList = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            userList.AddRange(audience.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
            return userList;

        }

        private IEnumerable<Claim> GetClaimsClient(Client client)
        {
            var clientlist = new List<Claim>()
            {
              new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
              new Claim(JwtRegisteredClaimNames.Sub,client.Id.ToString())

            };

            clientlist.AddRange(client.Audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
            return clientlist;
        }

        private string CreateRefreshToken()
        {
            var randombyte = new Byte[32];
            using var rnd = RandomNumberGenerator.Create();
            rnd.GetBytes(randombyte);
            return Convert.ToBase64String(randombyte);


        }

        public TokenDto createToken(User user)
        {
            var accessTokenExpiration = DateTime.Now.AddMinutes(customTokenOptions.AccessTokenExpiration);
            var refreshTokenExpiration = DateTime.Now.AddMinutes(customTokenOptions.RefreshTokenExpiration);

            var securityKey = SignService.GetSymmetricKey(customTokenOptions.SecurityKey);

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer: customTokenOptions.Issuer,
                expires: accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: GetClaims(user, customTokenOptions.Audience),
                signingCredentials: signingCredentials);

            var handler = new JwtSecurityTokenHandler();
            var token = handler.WriteToken(securityToken);
            return new TokenDto
            {
                AccessToken = token,
                AccessTokenExpiration = accessTokenExpiration,
                RefreshToken = CreateRefreshToken(),
                RefresTokenExpiration = refreshTokenExpiration

            };

        }

        public ClientTokenDto createtokenByClient(Client client)
        {
            var accessTokenExpiration = DateTime.Now.AddMinutes(customTokenOptions.AccessTokenExpiration);
            var securityKey = SignService.GetSymmetricKey(customTokenOptions.SecurityKey);

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.Aes256CbcHmacSha512);

            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer: customTokenOptions.Issuer,
                expires: accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: GetClaimsClient(client),
                signingCredentials: signingCredentials);

            var handler = new JwtSecurityTokenHandler();
            var token = handler.WriteToken(securityToken);
            return new ClientTokenDto
            {
                AccessToken = token,
                AccessTokenExpiration = accessTokenExpiration,

            };
        }
    }
}
