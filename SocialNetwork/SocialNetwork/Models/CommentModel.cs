using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocialNetwork.Models;

namespace SocialNetwork.Models
{
    public class CommentModel
    {
        SocialNetworkEntities context = new SocialNetworkEntities();
        AccountModels accountModel = new AccountModels();
        public Comment InsertComment(string text, string userId, string postId)
        {
            Comment cmt = new Comment();
            cmt.text = text;
            cmt.userId = Convert.ToInt32(userId);
            cmt.time = DateTime.Now;
            cmt.postId = Convert.ToInt32(postId);
            //cmt.User = accountModel.GetUserById(Convert.ToInt32(userId));
            try
            {
                context.Comments.Add(cmt);
                context.SaveChanges();
            }
            catch (Exception)
            {

            }
            return cmt;
        }

        public string LoadComment(int postId, int cmtId)
        {
            String chuoiJson = "";
            int commmentId= 0;
            var comments = (from c in context.Comments
                            where c.postId == postId && c.commentId > cmtId
                            select c);
            foreach (var item in comments.ToList())
            {
                chuoiJson += DisplayComment(item);
                commmentId = item.commentId;
            }
            chuoiJson += "|" + commmentId;
            return chuoiJson;

        }
        public String DisplayComment(Comment cmt)
        {
            User user = accountModel.GetUserById(cmt.userId);
            string chuoiJson = "";
            try
            {
                chuoiJson += "<table>";
                chuoiJson += "<tr>";
                chuoiJson += "<td class=\"col1\">";
                chuoiJson += "<img class=\"img-responsive\" alt=\"avatar\" src=\"" + user.avatar + "\" />";
                chuoiJson += "</td>";
                chuoiJson += "<td class=\"col22\"  id=\"cmt_nickName_" + user.userId + "_" + cmt.commentId + "\">";
                chuoiJson += "<a style=\"display:inline\" id=\"" + user.userId + "\" onmouseover=\"hoverAcc(event," + user.userId + ","+ 0 +"," + cmt.commentId + ")\" href=\"http://localhost:53130/Posts/UserPage/" + cmt.userId + "\">" + user.nickName + "</a>";
                chuoiJson += "<span>" + cmt.text + "</span>";
                chuoiJson += "<span>" + cmt.time + "</span>";
                chuoiJson += "</td>";
                chuoiJson += "</tr>";
                chuoiJson += "</table>";
            }
            catch (Exception)
            {
            }

            return chuoiJson;
        }
    }
}