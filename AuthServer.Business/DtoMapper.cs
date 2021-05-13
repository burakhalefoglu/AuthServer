using AuthServer.Core.DTOs;
using AuthServer.Core.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Business
{
    class DtoMapper : Profile
    {
        public DtoMapper()
        {
            CreateMap<UserDto, User>().ReverseMap();

        }
    }
}
