using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Detego.WebAPI.Dto
{
    public class AddStoreDto
    {
        // Store Info
        [Required]
        public string Name { get; set; }
        [Required]
        public int CountryCode { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public int Category { get; set; }

        [Required(ErrorMessage = "Back Store Cannot Be Empty")]
        public int BackStore { get; set; }
        [Required(ErrorMessage = "Front Store Cannot Be Empty")]
        public int FrontStore { get; set; }
        [Required(ErrorMessage = "Shopping Window Cannot Be Empty")]
        public int ShoppingWindow { get; set; }
        //[Required]
        //public decimal Accuracy { get; set; }
        //[Required(ErrorMessage = "On Floor Availability Cannob Be Empty")]
        //public decimal OnFloorAvailability { get; set; }
        //[Required(ErrorMessage = "Mean Age Cannot Be Empty")]
        //public int MeanAgeInDays { get; set; }

        [Required]
        public string UserName { get; set; }
    }
}
