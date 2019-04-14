using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Detego.WebAPI.Dto
{
    public class UserForRegisterDto
    {
        [Required(ErrorMessage = "User Name Is Required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password Is Required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "FirstName Is Required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "LastName Is Required")]
        public string LastName { get; set; }

    }
}
