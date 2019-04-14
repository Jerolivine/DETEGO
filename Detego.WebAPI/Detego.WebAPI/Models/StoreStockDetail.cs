using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Detego.WebAPI.Models
{
    public class StoreStockDetail
    {
        public int Id { get; set; }
        public int BackStore { get; set; }
        public int FrontStore { get; set; }
        public int ShoppingWindow { get; set; }
        public decimal Accuracy { get; set; }   
        public decimal OnFloorAvailability { get; set; }
        public int MeanAgeInDays { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int TotalStock { get; set; }

        public int StoreId { get; set; }
        public virtual Store Store { get; set; }
    }
}
