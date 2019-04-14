using Detego.WebAPI.Models.LookUpModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Detego.WebAPI.Data.Repositories.Interfaces
{
    public interface ICountryCodeRepository
    {
        Task<List<CountryCode>> GetCountryCodes();
    }
}
