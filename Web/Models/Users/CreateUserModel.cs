using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.Users
{
    public class CreateUserModel
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string UserName { get; set; }

    }
}
