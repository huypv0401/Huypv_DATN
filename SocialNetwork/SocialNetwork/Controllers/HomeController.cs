using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialNetwork.Models;
using SocialNetwork.Models.ViewModel;
using SocialNetwork.Controllers;

namespace ASP.NET_MVC5_Bootstrap3_3_1_LESS.Controllers
{
    public class HomeController : Controller
    {
        PostModels postModel = new PostModels();
        AccountDetailModels accDetail = new AccountDetailModels();
        [Authorize]
        public ActionResult Index()
        {
            User user = (User)System.Web.HttpContext.Current.Session["User"];
            if (user != null)
            {
                List<Post> posts = postModel.GetListPost(1);
                ViewData["favorites"] = accDetail.GetListFavorite();
                //ViewBag.userId = user.userId;
                //ViewBag.userNickName = user.nickName;
                //ViewBag.userAvatar = user.avatar;

                return View(posts);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }


        [HttpPost]
        public JsonResult InfiniteScrollPost(int pageNumber)
        {
            List<Post> posts = postModel.GetListPost(pageNumber);
            string chuoi = "";
            foreach (var post in posts)
            {
                chuoi += postModel.DisplayPost(post);
            }
            return Json(chuoi);
        }
    }
}