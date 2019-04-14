using AutoMapper;
using Detego.WebAPI.Data.Repositories.Interfaces;
using Detego.WebAPI.Dto;
using Detego.WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Detego.WebAPI.Data.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private DataContext _context;
        private IMapper _mapper;

        public AuthRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> IsUserExists(string userName)
        {
            return await _context.SystemUser.AnyAsync(x => x.UserName == userName);

        }

        public async Task<SystemUser> Login(string userName, string password)
        {
            var user = await _context.SystemUser.FirstOrDefaultAsync(x => x.UserName == userName && x.Password == password);
            if (user == null)
            {
                return null;
            }
            return user;
        }

        public async Task<SystemUser> Register(UserForRegisterDto userRegister)
        {
            if (await IsUserExists(userRegister.UserName))
            {
                return null;
            }

            var newUser = _mapper.Map<SystemUser>(userRegister);
            await _context.SystemUser.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return newUser;
           
        }

    }
}

