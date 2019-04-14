using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Detego.WebAPI.Dto
{
    public class GetStoreDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Category { get; set; }
        public int CountryCode { get; set; }
        public int BackStore { get; set; }
        public int FrontStore { get; set; }
        public int ShoppingWindow { get; set; }

    }
}
