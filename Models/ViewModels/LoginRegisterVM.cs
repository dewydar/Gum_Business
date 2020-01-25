using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GymBusiness.Models.ViewModels
{
    public class LoginRegisterVM
    {
        public RegisterViewModel Register { get; set; }
        public LoginViewModel Login { get; set; }
    }
}