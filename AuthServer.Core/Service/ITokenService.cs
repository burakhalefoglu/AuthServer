﻿using AuthServer.Core.Configuration;
using AuthServer.Core.DTOs;
using AuthServer.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Core.Service
{
    public interface ITokenService
    {
        TokenDto createToken(User user);

        ClientTokenDto createtokenByClient(Client client);
    }
}
