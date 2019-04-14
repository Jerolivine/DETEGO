using Detego.WebAPI.Data.Repositories.Interfaces;
using Detego.WebAPI.Dto;
using Detego.WebAPI.Helpers.Expc;
using Detego.WebAPI.Infrastructure;
using Detego.WebAPI.Models.NonDbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Detego.WebAPI.Controllers
{
    //[Authorize]
    [Produces("application/json")]
    [Route("api/Store")]
    [ApiController]
    public class StoreController : Controller
    {
        private IStoreRepository _storeRepository;
        private IAuthRepository _authRepository;

        public StoreController(IStoreRepository storeRepository, IAuthRepository authRepository)
        {
            _storeRepository = storeRepository;
            _authRepository = authRepository;
        }

        [HttpPost("addStore")]
        public async Task<ActionResult> AddStore([FromBody]AddStoreDto userAddStore)
        {

            await Validate(userAddStore.UserName);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Keys.SelectMany(i => ModelState[i].Errors).Select(m => m.ErrorMessage).ToArray());
            }

            var store = await _storeRepository.AddStore(userAddStore);
            return Ok(store);
        }

        [HttpPut("updateStore")]
        public async Task<ActionResult> UpdateStore([FromBody]UpdateStoreDto userUpdateStore)
        {
            await Validate(userUpdateStore.UserName);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Keys.SelectMany(i => ModelState[i].Errors).Select(m => m.ErrorMessage).ToArray());
            }

            var store = await _storeRepository.UpdateStore(userUpdateStore);

            var getStoreDto = new GetStoreDto();
            getStoreDto.BackStore = store.StoreStockDetail.BackStore;
            getStoreDto.Category = store.Category.Id;
            getStoreDto.CountryCode = store.CountryCode.Id;
            getStoreDto.Email = store.Email;
            getStoreDto.FrontStore = store.StoreStockDetail.FrontStore;
            getStoreDto.Id = store.Id;
            getStoreDto.Name = store.Name;
            getStoreDto.ShoppingWindow = store.StoreStockDetail.ShoppingWindow;

            return Ok(getStoreDto);
        }

        [HttpGet("getStores")]
        public async Task<ActionResult> GetStores([FromQuery]string userName)
        {
            await Validate(userName);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Keys.SelectMany(i => ModelState[i].Errors).Select(m => m.ErrorMessage).ToArray());
            }

            var stores = await _storeRepository.GetStores(userName);

            List<GetStoresDto> getStoresDtos = new List<GetStoresDto>();
            foreach(var store in stores)
            {
                getStoresDtos.Add(new GetStoresDto()
                {
                    Category = store.Category.Name,
                    CountryCode = store.CountryCode.Name,
                    Id = store.Id,
                    Name = store.Name,
                    Email = store.Email
                });
            }

            return Ok(getStoresDtos);
        }

        [HttpGet("getStore")]
        public async Task<ActionResult> GetStore([FromQuery]int storeId)
        {
            var store = await _storeRepository.GetStore(storeId);

            return Ok(store);
        }

        [HttpGet("getChartReport")]
        public async Task<ActionResult> GetChartReport([FromQuery] int storeId,[FromQuery] string userName,[FromQuery] Enums.ReportType reportType )
        {
            var reportDto = new GetChartReportDto();
            reportDto.StoreId = storeId;
            reportDto.ReportType = reportType;
            reportDto.UserName = userName;

            var chartReport = await _storeRepository.GetChartReport(reportDto);

            return Ok(chartReport);

        }

        private async Task Validate(string userName)
        {
            var isUserExists = await _authRepository.IsUserExists(userName);
            if (!isUserExists)
            {
                ModelState.AddModelError("UserName", "UserName Doesn't Exists");
            }
        }

    }
}
