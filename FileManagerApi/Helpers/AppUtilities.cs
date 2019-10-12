using AutoMapper;
using FileManagerApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManagerApi.Helpers
{
    public partial class AppUtilities
    {
    }

    public class AppSettings
    {
        public string Secret { get; set; }
        public string DefaultFolder { get; set; }
    }

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}
