using Detego.WebAPI.Data.Repositories.Interfaces;
using Detego.WebAPI.Models.LookUpModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Detego.WebAPI.Data.Repositories
{
    public class ReportTypeRepository : IReportTypeRepository
    {

        private DataContext _context;

        public ReportTypeRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<ReportType>> GetReportTypes()
        {
            var reportTypes = await _context.ReportType.ToListAsync();

            return reportTypes;

        }
    }
}
