using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Detego.WebAPI.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Detego.WebAPI.Controllers
{

    [Produces("application/json")]
    [Route("api/CountryCode")]
    [ApiController]
    public class CountryCodeController : ControllerBase
    {

        private ICountryCodeRepository _countryCodeRepository;

        public CountryCodeController(ICountryCodeRepository countryCodeRepository)
        {
            _countryCodeRepository = countryCodeRepository;
        }

        [HttpGet]
        public async Task<ActionResult> getCountryCodes()
        {
            var countryCodes = await _countryCodeRepository.GetCountryCodes();

            return Ok(countryCodes);
        }

    }
}