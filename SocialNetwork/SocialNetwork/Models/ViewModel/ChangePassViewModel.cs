using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SocialNetwork.Models.ViewModel
{
    public class ChangePassViewModel
    {
        [Required(ErrorMessage = "Bạn chưa nhập mật khẩu!")]
        [StringLength(100, ErrorMessage = "{0} phải có ít nhất {2} ký tự.", MinimumLength = 6)]
        [Display(Name = "Mật khẩu cũ *")]
        public string _password { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập mật khẩu!")]
        [StringLength(100, ErrorMessage = "{0} phải có ít nhất {2} ký tự.", MinimumLength = 6)]
        [Display(Name = "Mật khẩu mới*")]
        public string password { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "Nhập lại mật khẩu mới")]
        [System.ComponentModel.DataAnnotations.Compare("password", ErrorMessage = "Mật khẩu không trùng khớp!")]
        public string ConfirmPassword { get; set; }

    }
}