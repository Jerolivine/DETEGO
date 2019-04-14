using Detego.WebAPI.Data.Repositories.Interfaces;
using Detego.WebAPI.Models.LookUpModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Detego.WebAPI.Data.Repositories
{
    public class CountryCodeRepository : ICountryCodeRepository
    {

        private DataContext _context;

        public CountryCodeRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<CountryCode>> GetCountryCodes()
        {
            return await _context.CountryCode.ToListAsync();
        }
    }
}
