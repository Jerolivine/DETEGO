using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Detego.WebAPI.Models.NonDbModels
{
    public class Report
    {
        public double TotalStock { get; set; }
        public Decimal Accuracy { get; set; }
        public Decimal OnFloorAvailability { get; set; }
        public double MeanAgeInDays { get; set; }

    }
}
