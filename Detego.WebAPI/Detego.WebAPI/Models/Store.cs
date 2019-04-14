using Detego.WebAPI.Models.LookUpModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Detego.WebAPI.Models
{
    public class Store
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public virtual CountryCode CountryCode { get; set; }
        public virtual Category Category { get; set; }
        public virtual SystemUser User { get; set; }
        public virtual StoreStockDetail StoreStockDetail { get; set; }

    }
}
