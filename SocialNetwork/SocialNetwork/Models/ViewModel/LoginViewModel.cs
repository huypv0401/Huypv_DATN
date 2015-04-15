using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SocialNetwork.Models.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Bạn chưa nhập email!")]
        [RegularExpression(@"^(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6}$", ErrorMessage = "Email không chính xác!")]
        [Display(Name = "Email *")]
        public string userName { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập mật khẩu!")]
        [Display(Name = "Mật khẩu *")]
        public string password { get; set; }


        [Display(Name = "Ghi nhớ tài khoản?")]
        public bool RememberMe { get; set; }
    }
}