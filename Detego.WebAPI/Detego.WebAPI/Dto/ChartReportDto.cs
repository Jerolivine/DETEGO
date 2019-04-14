using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Detego.WebAPI.Models.NonDbModels
{
    public class ChartReportDto
    {
        public Report StoreReport { get; set; }
        public Report GeneralReport { get; set; }

        public ChartReportDto()
        {
            this.StoreReport = new Report();
            this.GeneralReport = new Report();
        }

    }
}
