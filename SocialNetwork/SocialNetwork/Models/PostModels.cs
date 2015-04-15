using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocialNetwork;

namespace SocialNetwork.Models
{

    public class PostModels
    {
        static int pageSize = 3;
        SocialNetworkEntities context = new SocialNetworkEntities();
        User user = (User)System.Web.HttpContext.Current.Session["User"];

        public List<Post> GetListPost(int pageNumber)
        {
            var userFollows = (from r in context.Relationships
                                   where r.user == user.userId
                                   select r.userFollow);

            List<Post> posts = new List<Post>();
            var postss = (from p in context.Posts
                          where userFollows.Contains(p.userId) || p.userId == user.userId
                          orderby p.timePost
                     select p
                     );
            posts = postss.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return posts;
        }

        public List<Post> GetListPostByUserId(int pageNumber, int? userId)
        {
            List<Post> posts = new List<Post>();
            posts = (from p in context.Posts
                     where p.userId == userId
                     orderby p.timePost
                     select p
                     ).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return posts;
        }


        public int InsertImage(string link)
        {
            Image image = new Image();
            image = (from i in context.Images
                     where i.link == link
                     select i).FirstOrDefault();
            if (image != null)
                return image.imageId;
            else
            {
                try
                {
                    Image img = new Image();
                    img.link = link;
                    context.Images.Add(img);
                    context.SaveChanges();
                    return img.imageId;
                }
                catch (Exception)
                {

                }
                return 0;

            }

        }

        public bool InsertLike(bool like, int postId, int userId)
        {


            try
            {
                var exitsLike = (from l in context.Likes where l.userId == userId && l.postId == postId select l).FirstOrDefault();
                if (exitsLike != null)
                {
                    exitsLike.like1 = like;
                    exitsLike.dislike = !like;
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    Like li = new Like();
                    li.like1 = like;
                    li.dislike = !like;
                    li.postId = postId;
                    li.userId = userId;
                    context.Likes.Add(li);
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public Post InsertPost(string text, string userId, string image)
        {
            Post post = new Post();
            post.text = text;
            post.userId = Convert.ToInt32(userId);
            post.timePost = DateTime.Now;
            post.timeHide = DateTime.Now.AddDays(1);
            if (image != null)
            { post.imageId = InsertImage(image); }

            try
            {
                context.Posts.Add(post);
                context.SaveChanges();
            }
            catch (Exception)
            {

            }

            return post;
        }

        public string GetLikeByPostId(int postId, int userId)
        {
            var like = (from l in context.Likes
                        where l.postId == postId
                        select l);
            int sumLike = (from sl in like
                           where sl.like1 == true
                           select sl).ToList().Count;
            int sumDislike = like.ToList().Count() - sumLike;
            var exitsUser = (from l in like
                             where l.postId == postId && l.userId == userId
                             select l).FirstOrDefault();
            string chuoiJson;
            if (exitsUser != null)
            {
                if (exitsUser.like1 == true)
                    chuoiJson = "true" + "|" + "true" + "|" + sumLike + "|" + sumDislike;
                else
                    chuoiJson = "true" + "|" + "false" + "|" + sumLike + "|" + sumDislike;
            }
            else
                chuoiJson = "false" + "|" + "false" + "|" + sumLike + "|" + sumDislike;
            return chuoiJson;
        }

        public string DisplayPost(Post post)
        {
            AccountModels accountModel = new AccountModels();
            User user = new User();
            if (post.User == null)
            {
                user = accountModel.GetUserById(post.userId);
            }
            else user = post.User;

            string postString = "";
            try
            {
                postString += "<div class=\"post_container\">";
                postString += "<div class=\"post_head\">";
                postString += "<div class=\"post_avatar\">";
                postString += "<img class=\"img-responsive\" alt=\"avatar\" src=\"" + user.avatar + "\" />";
                postString += "</div>";
                postString += "<div class=\"post_info\">";
                postString += "<div class=\"post_nickName\" id=\"post_nickName_"+user.userId+"_"+post.postId+"\"> ";
               // postString += "<a id=\""+user.userId + "\"onmouseover=\"hoverAcc(event,user.userId,post.postId)\" href=\"http://localhost:53130/Posts/UserPage/" + post.userId + "\">" + user.nickName + "</a>";
                postString += "<a id=\"" + user.userId + "\" onmouseover=\"hoverAcc(event,"+user.userId +"," + post.postId +","+0+ ")\" href=\"http://localhost:53130/Posts/UserPage/"+user.userId +"\">" + user.nickName +  "</a>";
                postString += " </div>";
                postString += "<div class=\"post_time\">" + post.timePost + "</div>";
                postString += "</div>";
                postString += "</div>";
                postString += "<div class=\"post_context\">" + post.text + "</div>";
                postString += "<div class=\"post_image\">";
                if (post.imageId != null)
                {
                    postString += "<img class=\"img-responsive\" style=\"margin:0 auto\" alt=\"avatar\" src=" + post.Image.link + " />";
                }
                postString += "</div>";
                postString += "<hr />";
                postString += "<div class=\"post_like\">";

                postString += "<table>";
                postString += "<tr>";
                postString += "<td class=\"like\" id=\"like\">";
                postString += "<a  title=\"Thích điều này\" role=\"button\">";
                postString += "<i class=\"ilike\"></i>";
                postString += "<span>Thích</span>";
                postString += "</a>";
                postString += "</td>";
                postString += "<td class=\"dislike\" id=\"dislike\">";
                postString += "<a  title=\"Thích điều này\" role=\"button\">";
                postString += "<i class=\"idislike\"></i>";
                postString += " <span>Không thích</span>";
                postString += "</a>";
                postString += "</td>";
                postString += "<td class=\"cmt\" id=\"cmt\">";
                postString += "<a  title=\"Thích điều này\" role=\"button\">";
                postString += "<i class=\"icmt\"></i>";
                postString += "<span>Bình luận</span>";
                postString += "</a>";
                postString += " </td>";
                postString += "</tr>";
                postString += " </table>";

                postString += "</div>";


                postString += "<hr />";//---------div comment------------
                postString += " <div class=\"comment\" style=\"width:100%; height:auto\">";
                postString += "<table>";
                postString += " <tr>";
                postString += "<td class=\"col1\">";
                postString += "<img class=\"img-responsive\" alt=\"avatar\" src=\"" + user.avatar + "\" />";
                postString += "</td>";
                postString += "<td class=\"col2\">";
                postString += "<input type=\"text\" placeholder=\"Bạn nghĩ gì...?\" />";
                postString += "</td>";
                postString += " <td class=\"col3\"></td>";
                postString += "</tr>";
                postString += " </table>";
                postString += "</div>";

                postString += "</div>";

            }
            catch (Exception)
            {
            }
            return postString;
        }
    }
}