using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialNetwork.Models;
using SocialNetwork.Models.ViewModel;
using System.IO;

namespace SocialNetwork.Controllers
{
    public class AccountDetailController : Controller
    {
        AccountDetailModels accountDetailModel = new AccountDetailModels();
        // GET: AccountDetail
        public ActionResult Profile(int id)
        {
            User user = accountDetailModel.GetUserById(id);
            Session["User"] = user;
            //ViewData["Favorites"] = accountDetailModel.GetListFavoriteByUserId(user.userId);
            return View(user);
        }
        [HttpPost]
        public ActionResult UpdateProfile(User model)
        {
            accountDetailModel.UpdateUser(model);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ChangePass(ChangePassViewModel model)
        {


            if (model.password == null)
                return View();
            else
            {
                User user = (User)System.Web.HttpContext.Current.Session["User"];
                if (accountDetailModel.ChangePass(model, user.userId))
                    return RedirectToAction("Profile", new {id=user.userId });
                else
                    return View();
            }
        }

        public ActionResult ChangeAvatar(int userId)
        {
            string linkImage = "";
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                // Get the uploaded image from the Files collection
                var httpPostedFile = System.Web.HttpContext.Current.Request.Files["UploadedImage"];

                if (httpPostedFile != null)
                {
                    var fileSavePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/images/Avatar"), httpPostedFile.FileName);
                    httpPostedFile.SaveAs(fileSavePath);
                }
                linkImage = "/Content/images/Avatar/" + httpPostedFile.FileName;

            }
            string linkAvatarold = accountDetailModel.updateAvatarUser(userId, linkImage);
            //linkAvatarold = linkAvatarold.Replace("/", "\\");
            //string folder = @"C:\Users\HuyPV\Documents\Visual Studio 2013\Projects\SocialNetwork\SocialNetwork";

            //linkAvatarold = folder + linkAvatarold;
            //if (System.IO.File.Exists(linkAvatarold))
            //{ System.IO.File.Delete(linkAvatarold); }
            return RedirectToAction("Profile", new { id = userId });
        }
    }
}