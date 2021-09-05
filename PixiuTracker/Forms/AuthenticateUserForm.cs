using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PixiuTracker.Forms
{
    public class AuthenticateUserForm
    {
        [Required]
        public string Token { get; set; }

    }
}
