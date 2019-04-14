using Detego.WebAPI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Detego.WebAPI.Dto
{
    public class GetChartReportDto
    {
        public int StoreId { get; set; }
        public string UserName { get; set; }
        public Enums.ReportType ReportType { get; set; }
    }
}
