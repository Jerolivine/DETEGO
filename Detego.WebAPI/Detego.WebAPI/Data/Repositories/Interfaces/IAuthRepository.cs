using Detego.WebAPI.Dto;
using Detego.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Detego.WebAPI.Data.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task<SystemUser> Login(string userName, string password);
        Task<SystemUser> Register(UserForRegisterDto userRegister);
        Task<bool> IsUserExists(string userName);
    }
}
