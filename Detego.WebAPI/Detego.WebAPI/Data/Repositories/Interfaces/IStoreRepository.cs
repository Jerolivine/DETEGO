using Detego.WebAPI.Dto;
using Detego.WebAPI.Models;
using Detego.WebAPI.Models.NonDbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Detego.WebAPI.Controllers
{
    public interface IStoreRepository
    {
        Task<Store> AddStore(AddStoreDto userAddStore);
        Task<Store> UpdateStore(UpdateStoreDto userUpdateStore);
        Task<List<Store>> GetStores(string userName);
        Task<GetStoreDto> GetStore(int storeId);
        Task<ChartReportDto> GetChartReport(GetChartReportDto reportDto);

    }
}
