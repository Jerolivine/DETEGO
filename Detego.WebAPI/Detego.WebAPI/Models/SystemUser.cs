using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Detego.WebAPI.Models
{
    public class SystemUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual List<Store> Stores { get; set; }

        public SystemUser()
        {
            this.Stores = new List<Store>();
        }

    }

}
