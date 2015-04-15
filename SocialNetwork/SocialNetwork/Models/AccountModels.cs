using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocialNetwork.Models.ViewModel;

namespace SocialNetwork.Models
{
    public class AccountModels
    {
        SocialNetworkEntities context = new SocialNetworkEntities();
        RelationshipModels relModel = new RelationshipModels();
        public bool register(RegisterViewModel model)
        {
            User user = new User();
            user.userName = model.userName;
            user.password = Helpers.md5(model.password);
            user.nickName = model.nickName;
            user.sex = model.sex;
            user.birthday = model.birthday;
            user.avatar = "/Content/images/Avatar/avatar.jpg";
            bool vR;
            try
            {
                context.Users.Add(user);
                context.SaveChanges();
                vR = true;
            }
            catch (Exception)
            {
                vR = false;
            }
            return vR;
        }

        public User checkLogin(LoginViewModel model)
        {
            model.password = Helpers.md5(model.password);
            User user;
            try
            {
                user = context.Users.FirstOrDefault(x => x.userName == model.userName && x.password == model.password);
            }
            catch (Exception)
            {
                user = null;
            }
            return user;
        }

        public User GetUserNameByEmail(string email)
        {
            User user;
            try
            {
                user = context.Users.FirstOrDefault(x => x.userName == email);
            }
            catch (Exception)
            {

                user = null;
            }

            return user;
        }

        public User GetUserById(int? id)
        {
            User user;
            try
            {
                user = context.Users.FirstOrDefault(x => x.userId == id);
            }
            catch (Exception)
            {

                user = null;
            }

            return user;
        }

        public string DisplayAccountInfor(User user)
        {

            User myAcc = (User)System.Web.HttpContext.Current.Session["User"];
            bool followed = relModel.CheckFollow(myAcc.userId, user.userId);

            string chuoi = "";
            //chuoi += "<div class=\"show\" id=\"divHidden_" + user.userId + "\">";
            chuoi += "<div class=\"acc_infor\">";
            chuoi += " <div class=\"acc_avatar\">";
            chuoi += " <img src=\"" + user.avatar + "\" class=\"img-thumbnail img-responsive\" />";
            chuoi += " </div>";
            chuoi += "<div class=\"acc_nickName\">";
            chuoi += " <p>" + user.nickName + "</p>";
            chuoi += "</div>";
            chuoi += "</div>";
            if (myAcc.userId != user.userId)
            {
                chuoi += "<div class=\"acc_bottom\">";
                if (followed)
                {
                    chuoi += " <input type=\"button\" onclick=\"follow(" + myAcc.userId + "," + user.userId + ")\"" + "  id=\"btn_fllow_" + myAcc.userId + "_" + user.userId + "\" class=\"btn btn-sm btn-info\" style=\"margin: 3px auto\" value=\"Đang theo dõi\" />";
                }
                else
                {
                    chuoi += " <input type=\"button\" onclick=\"follow(" + myAcc.userId + "," + user.userId + ")\"" + "  id=\"btn_fllow_" + myAcc.userId + "_" + user.userId + "\" class=\"btn btn-sm btn-info\" style=\"margin: 3px auto\" value=\"Theo dõi\" />";
                }
                chuoi += " <input type=\"button\" class=\"btn btn-sm btn-info\" style=\"float:right;margin:3px;\" value=\"Tin nhắn\" />";
                chuoi += " </div>";
            }

            //chuoi += " </div>";
            return chuoi;
        }
    }
}