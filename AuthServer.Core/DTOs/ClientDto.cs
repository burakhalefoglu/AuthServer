﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Core.DTOs
{
    public class ClientDto
    {
        public int ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}