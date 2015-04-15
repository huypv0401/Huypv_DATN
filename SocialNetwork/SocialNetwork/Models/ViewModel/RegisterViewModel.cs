using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialNetwork.Models.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Bạn chưa nhập email!")]
        [RegularExpression(@"^(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6}$", ErrorMessage = "Email không chính xác!")]
        [Remote("doesUserNameExist", "Account", HttpMethod = "POST", ErrorMessage = "Email này đã được đăng ký. Vui lòng chọn email khác.")]
        [Display(Name = "Email *")]
        public string userName { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập mật khẩu!")]
        [StringLength(100, ErrorMessage = "{0} phải có ít nhất {2} ký tự.", MinimumLength = 6)]
        [Display(Name = "Mật khẩu *")]
        public string password { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "Nhập lại mật khẩu")]
        [System.ComponentModel.DataAnnotations.Compare("password", ErrorMessage = "Mật khẩu không trùng khớp!")]
        public string ConfirmPassword { get; set; }


        [Display(Name = "Tên tài khoản")]
        public string nickName { get; set; }

        [Display(Name = "Giới tính")]
        public Nullable<bool> sex { get; set; }
        [Display(Name = "Ngày sinh")]
        public Nullable<System.DateTime> birthday { get; set; }
        [Display(Name = "Địa chỉ")]
        public string address { get; set; }
    }
}