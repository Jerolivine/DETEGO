using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Detego.WebAPI.Dto
{
    public class UserForLoginDto
    {
        [Required(ErrorMessage = "UserName Is Required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password Is Required")]
        public string Password { get; set; }

    }
}
