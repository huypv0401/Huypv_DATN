using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialNetwork.Models;
using SocialNetwork.Models.ViewModel;
using System.Web.Security;

namespace SocialNetwork.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        AccountModels accountModels = new AccountModels();
        // GET: Account
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = accountModels.checkLogin(model);
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.userName, model.RememberMe);

                    this.Session["User"] = user;
                    return RedirectToAction("Index", "Home");
                }

                else
                {
                    ModelState.AddModelError("", "Email hoặc mật khẩu không chính xác!");
                    return View(model);
                }
            }
            else
                return View(model);
        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }


        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (accountModels.register(model))
                    return RedirectToAction("Index", "Home");
                else
                    return View(model);
            }
            else
            {
                ModelState.AddModelError("", "Login data is incorrect!");
            }
            return View(model);
        }


        [HttpPost]
        public JsonResult doesUserNameExist(string UserName)
        {

            var user = accountModels.GetUserNameByEmail(UserName);

            return Json(user == null);
        }

        public JsonResult LoadAccountByUserId(int userId)
        {
            User user = accountModels.GetUserById(userId);
            string chuoiJson = accountModels.DisplayAccountInfor(user);
            return Json(chuoiJson);
        }
    }
}