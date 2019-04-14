using Detego.WebAPI.Controllers;
using Detego.WebAPI.Dto;
using Detego.WebAPI.Helpers.Expc;
using Detego.WebAPI.Helpers.Utils;
using Detego.WebAPI.Models;
using Detego.WebAPI.Models.NonDbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Detego.WebAPI.Data.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private DataContext _context;

        public StoreRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Store> AddStore(AddStoreDto userAddStore)
        {
            Random random = new Random();
            var user = await _context.SystemUser.Include(x => x.Stores).FirstOrDefaultAsync(x => x.UserName == userAddStore.UserName);
            if (user == null)
            {
                throw new NotFoundException("User Not Found username=" + userAddStore.UserName);
            }

            var category = await _context.Category.FirstOrDefaultAsync(x => x.Id == userAddStore.Category);

            if (category == null)
            {
                throw new NotFoundException("Category Not Found Id="+userAddStore.Category);
            }

            var countryCode = await _context.CountryCode.FirstOrDefaultAsync(x => x.Id == userAddStore.CountryCode);

            if (countryCode == null)
            {
                throw new NotFoundException("countryCode Not Found Id=" + userAddStore.CountryCode);
            }


            //// Map to StoreStockDetail
            StoreStockDetail storeStockDetail = new StoreStockDetail()
            {
                // Secreen Values
                BackStore = userAddStore.BackStore,
                FrontStore = userAddStore.FrontStore,
                ShoppingWindow = userAddStore.ShoppingWindow,

                Accuracy = random.GenerateDecimal(),
                MeanAgeInDays = random.Next() % 100,
                OnFloorAvailability = random.GenerateDecimal(),

            };

            // Map to Store
            Store store = new Store()
            {
                Email = userAddStore.Email,
                Name = userAddStore.Name,
                CountryCode = countryCode,
                User = user,
                StoreStockDetail = storeStockDetail,
                Category = category
            };

            user.Stores.Add(store);
            await _context.SaveChangesAsync();
            return store;

        }

        public async Task<Store> UpdateStore(UpdateStoreDto userUpdateStore)
        {
            var user = await _context.SystemUser
                .Include(x => x.Stores)
                    .ThenInclude(s => s.StoreStockDetail)
                .Include(x => x.Stores)
                    .ThenInclude(s => s.Category)
                .Include(x => x.Stores)
                    .ThenInclude(s => s.CountryCode)
                .FirstOrDefaultAsync(x => x.UserName == userUpdateStore.UserName);

            if (user == null)
            {
                return null;
            }

            var store = user.Stores.FirstOrDefault(soe => soe.Id == userUpdateStore.StoreId);
            if (store == null)
            {
                throw new NotFoundException("Store Not Found Id=" + userUpdateStore.StoreId);
            }

            var category = await _context.Category.FirstOrDefaultAsync(x => x.Id == userUpdateStore.Category);
            var countryCode = await _context.CountryCode.FirstOrDefaultAsync(x => x.Id == userUpdateStore.CountryCode);

            store.CountryCode = countryCode ?? throw new NotFoundException("countryCode Not Found Id=" + userUpdateStore.CountryCode);
            store.Category = category ?? throw new NotFoundException("Category Not Found Id=" + userUpdateStore.Category);
            store.Email = userUpdateStore.Email;
            store.Name = userUpdateStore.Name;

            store.StoreStockDetail.BackStore = userUpdateStore.BackStore;
            store.StoreStockDetail.FrontStore = userUpdateStore.FrontStore;
            store.StoreStockDetail.ShoppingWindow = userUpdateStore.ShoppingWindow;

            _context.Update(store);
            await _context.SaveChangesAsync();
            return store;

        }

        public async Task<List<Store>> GetStores(string userName)
        {
            var user = await _context.SystemUser
                .Include(x => x.Stores)
                    .ThenInclude(a => a.Category)
                .Include(x => x.Stores)
                    .ThenInclude(b => b.CountryCode)
                .FirstOrDefaultAsync(x => x.UserName == userName);

            if (user == null)
            {
                return null;
            }

            return user.Stores.OrderBy(s => s.Name).ToList();

        }

        public async Task<GetStoreDto> GetStore(int storeId)
        {
            var store = await _context.Store
                .Include(x => x.Category)
                .Include(x => x.CountryCode)
                .Include(x => x.StoreStockDetail)
                .FirstOrDefaultAsync(x => x.Id == storeId);

            GetStoreDto storeDto = new GetStoreDto();
            storeDto.BackStore = store.StoreStockDetail.BackStore;
            storeDto.Email = store.Email;
            storeDto.FrontStore = store.StoreStockDetail.FrontStore;
            storeDto.Id = store.Id;
            storeDto.Name = store.Name;
            storeDto.ShoppingWindow = store.StoreStockDetail.ShoppingWindow;
            storeDto.Category = store.Category.Id;
            storeDto.CountryCode = store.CountryCode.Id;

            return storeDto;

        }

        public async Task<ChartReportDto> GetChartReport(GetChartReportDto reportDto)
        {
            ChartReportDto chartReport = new ChartReportDto();

            #region Current Store Info

            var store = await _context.Store
                    .Include(x => x.StoreStockDetail)
                    .FirstOrDefaultAsync(x => x.Id == reportDto.StoreId);

            chartReport.StoreReport.Accuracy = store.StoreStockDetail.Accuracy;
            chartReport.StoreReport.MeanAgeInDays = store.StoreStockDetail.MeanAgeInDays;
            chartReport.StoreReport.OnFloorAvailability = store.StoreStockDetail.OnFloorAvailability;
            chartReport.StoreReport.TotalStock = store.StoreStockDetail.TotalStock;

            #endregion

            switch (reportDto.ReportType)
            {
                case Infrastructure.Enums.ReportType.Max:
                    #region Max values


                    chartReport.GeneralReport.Accuracy = await _context.SystemUser.
                                                                               Include(x => x.Stores)
                                                                                   .ThenInclude(x => x.StoreStockDetail)
                                                                               .Where(x => x.UserName == reportDto.UserName)
                                                                               .Select
                                                                               (
                                                                                   x => x.Stores
                                                                                   .Select(y => y.StoreStockDetail)
                                                                                       .Max(y => y.Accuracy)
                                                                               ).FirstOrDefaultAsync();

                    chartReport.GeneralReport.MeanAgeInDays = await _context.SystemUser.
                                                                    Include(x => x.Stores)
                                                                        .ThenInclude(x => x.StoreStockDetail)
                                                                    .Where(x => x.UserName == reportDto.UserName)
                                                                    .Select
                                                                    (
                                                                        x => x.Stores
                                                                        .Select(y => y.StoreStockDetail)
                                                                            .Max(y => y.MeanAgeInDays)
                                                                    ).FirstOrDefaultAsync();

                    chartReport.GeneralReport.OnFloorAvailability = await _context.SystemUser.
                                                                    Include(x => x.Stores)
                                                                        .ThenInclude(x => x.StoreStockDetail)
                                                                    .Where(x => x.UserName == reportDto.UserName)
                                                                    .Select
                                                                    (
                                                                        x => x.Stores
                                                                        .Select(y => y.StoreStockDetail)
                                                                            .Max(y => y.OnFloorAvailability)
                                                                    ).FirstOrDefaultAsync();

                    chartReport.GeneralReport.TotalStock = await _context.SystemUser.
                                                                    Include(x => x.Stores)
                                                                        .ThenInclude(x => x.StoreStockDetail)
                                                                    .Where(x => x.UserName == reportDto.UserName)
                                                                    .Select
                                                                    (
                                                                        x => x.Stores
                                                                        .Select(y => y.StoreStockDetail)
                                                                            .Max(y => y.TotalStock)
                                                                    ).FirstOrDefaultAsync();

                    #endregion
                    break;
                case Infrastructure.Enums.ReportType.Mean:
                    #region Mean values
                    chartReport.GeneralReport.Accuracy = await _context.SystemUser.
                                                                                         Include(x => x.Stores)
                                                                                             .ThenInclude(x => x.StoreStockDetail)
                                                                                         .Where(x => x.UserName == reportDto.UserName)
                                                                                         .Select
                                                                                         (
                                                                                             x => x.Stores
                                                                                             .Select(y => y.StoreStockDetail)
                                                                                                 .Average(y => y.Accuracy)
                                                                                         ).FirstOrDefaultAsync();

                    chartReport.GeneralReport.MeanAgeInDays = await _context.SystemUser.
                                                                    Include(x => x.Stores)
                                                                        .ThenInclude(x => x.StoreStockDetail)
                                                                    .Where(x => x.UserName == reportDto.UserName)
                                                                    .Select
                                                                    (
                                                                        x => x.Stores
                                                                        .Select(y => y.StoreStockDetail)
                                                                            .Average(y => y.MeanAgeInDays)
                                                                    ).FirstOrDefaultAsync();

                    chartReport.GeneralReport.OnFloorAvailability = await _context.SystemUser.
                                                                    Include(x => x.Stores)
                                                                        .ThenInclude(x => x.StoreStockDetail)
                                                                    .Where(x => x.UserName == reportDto.UserName)
                                                                    .Select
                                                                    (
                                                                        x => x.Stores
                                                                        .Select(y => y.StoreStockDetail)
                                                                            .Average(y => y.OnFloorAvailability)
                                                                    ).FirstOrDefaultAsync();

                    chartReport.GeneralReport.TotalStock = await _context.SystemUser.
                                                                    Include(x => x.Stores)
                                                                        .ThenInclude(x => x.StoreStockDetail)
                                                                    .Where(x => x.UserName == reportDto.UserName)
                                                                    .Select
                                                                    (
                                                                        x => x.Stores
                                                                        .Select(y => y.StoreStockDetail)
                                                                      .Average(y => y.TotalStock)
                                                                    ).FirstOrDefaultAsync();
                    #endregion
                    break;
                case Infrastructure.Enums.ReportType.Min:
                    #region Min values
                    chartReport.GeneralReport.Accuracy = await _context.SystemUser.
                                                                    Include(x => x.Stores)
                                                                        .ThenInclude(x => x.StoreStockDetail)
                                                                    .Where(x => x.UserName == reportDto.UserName)
                                                                    .Select
                                                                    (
                                                                        x => x.Stores
                                                                        .Select(y => y.StoreStockDetail)
                                                                            .Min(y => y.Accuracy)
                                                                    ).FirstOrDefaultAsync();

                    chartReport.GeneralReport.MeanAgeInDays = await _context.SystemUser.
                                                                    Include(x => x.Stores)
                                                                        .ThenInclude(x => x.StoreStockDetail)
                                                                    .Where(x => x.UserName == reportDto.UserName)
                                                                    .Select
                                                                    (
                                                                        x => x.Stores
                                                                        .Select(y => y.StoreStockDetail)
                                                                            .Min(y => y.MeanAgeInDays)
                                                                    ).FirstOrDefaultAsync();

                    chartReport.GeneralReport.OnFloorAvailability = await _context.SystemUser.
                                                                    Include(x => x.Stores)
                                                                        .ThenInclude(x => x.StoreStockDetail)
                                                                    .Where(x => x.UserName == reportDto.UserName)
                                                                    .Select
                                                                    (
                                                                        x => x.Stores
                                                                        .Select(y => y.StoreStockDetail)
                                                                            .Min(y => y.OnFloorAvailability)
                                                                    ).FirstOrDefaultAsync();

                    chartReport.GeneralReport.TotalStock = await _context.SystemUser.
                                                                    Include(x => x.Stores)
                                                                        .ThenInclude(x => x.StoreStockDetail)
                                                                    .Where(x => x.UserName == reportDto.UserName)
                                                                    .Select
                                                                    (
                                                                        x => x.Stores
                                                                        .Select(y => y.StoreStockDetail)
                                                                            .Min(y => y.TotalStock)
                                                                    ).FirstOrDefaultAsync();
                    #endregion
                    break;
            }

            return chartReport;

        }
    }
}

