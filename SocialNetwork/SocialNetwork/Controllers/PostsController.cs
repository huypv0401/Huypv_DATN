using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialNetwork.Models;
using System.IO;

namespace SocialNetwork.Controllers
{
    public class PostsController : Controller
    {
        PostModels postModel = new PostModels();
        AccountModels accountModel = new AccountModels();
        // GET: Posts

        public ActionResult UserPage(int? id)
        {
            var posts = postModel.GetListPostByUserId(1, id);
            var user = accountModel.GetUserById(id);
            ViewBag.userId = user.userId;
            ViewBag.avatar = user.avatar;
            ViewBag.nickName = user.nickName;
            return View(posts);
        }

        [HttpPost]
        public JsonResult CreatePost(string Text, string UserId)
        {
            string linkImage = null;
            string chuoiJson = "";
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                // Get the uploaded image from the Files collection
                var httpPostedFile = System.Web.HttpContext.Current.Request.Files["UploadedImage"];

                if (httpPostedFile != null)
                {
                    var fileSavePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/images/Post"), httpPostedFile.FileName);
                    httpPostedFile.SaveAs(fileSavePath);
                }
                linkImage = "/Content/images/Post/" + httpPostedFile.FileName;
                //Insert post to database
            }
            Post post = postModel.InsertPost(Text, UserId, linkImage);
            chuoiJson = postModel.DisplayPost(post);

            return Json(chuoiJson);
        }

        public JsonResult CreateLike(bool like, int postId, int userId)
        {
            bool ok = postModel.InsertLike(like, postId, userId);
            return Json(ok);
        }

        public JsonResult LoadLike(int postId, int userId)
        {
            string chuoiJson = postModel.GetLikeByPostId(postId, userId);
            return Json(chuoiJson);
        }

        [HttpPost]
        public JsonResult InfiniteScrollPost(int pageNumber, int userId)
        {
            List<Post> posts = postModel.GetListPostByUserId(pageNumber, userId);
            string chuoi = "";
            foreach (var post in posts)
            {

                chuoi += postModel.DisplayPost(post);
            }
            return Json(chuoi);
        }
    }
}