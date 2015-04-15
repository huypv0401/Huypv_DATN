using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialNetwork.Models;

namespace SocialNetwork.Controllers
{
    public class CommentController : Controller
    {
        CommentModel commentModel = new CommentModel();
        PostModels postModel = new PostModels();
        // GET: Comment
        [HttpPost]
        public JsonResult CreateComment(string text, string postId, string userId)
        {
            Comment cmt = commentModel.InsertComment(text, userId,postId);
            //if (postModel.InsertCommentToPost(cmt.commentId, Convert.ToInt32(postId)))
            return Json(commentModel.DisplayComment(cmt));
            
        
        }

        [HttpPost]
        public JsonResult LoadComment(int postId, int commentId )
        {
            string chuoiJson = "";
            //int cmtId=0;
            chuoiJson = commentModel.LoadComment(Convert.ToInt32(postId),commentId);           
            return Json(chuoiJson);
        }
    }
}