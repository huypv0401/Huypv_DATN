using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocialNetwork.Models.ViewModel;

namespace SocialNetwork.Models
{
    public class SearchModels
    {
        SocialNetworkEntities context = new SocialNetworkEntities();

        public string GetSearchJsonResult(string search)
        {
            List<User> users = (from u in context.Users
                                where (u.nickName.ToLower().Contains(search))
                                select u).ToList();
            string chuoiJson = "";
            foreach (var item in users)
            {
                var url = item.avatar;
                chuoiJson += "<li><a href=\"http://localhost:53130/Posts/UserPage/" + item.userId + "\">";
                chuoiJson += "<div> <img src=\"" + url + "\" class=\"cphoto\" />";
                chuoiJson += "<div class=\"cinfo\"><span class=\"cname\">" + item.nickName + "</span><br />";
                chuoiJson += "<span class=\"caddress\">" + item.birthday + "</span></div></div></a></li>";
            }
            return chuoiJson;
        }

        public List<User> SearchValue(SearchViewModels model)
        {
            model.name = model.name.ToLower();
            
            //string[] names = model.name.Split(' ');
            //List<User> users = new List<User>();
            //foreach (var item in names)
            //{
            //    var a = from u in context.Users
            //            where u.nickName.ToLower().Contains(item)
            //            select u;
            //    users.Add(a);
            //}


            //model.name = Helpers.RemoveSign4VietnameseString(model.name);
            //List<User> users = (from u in context.Users
            //                    where (Helpers.RemoveSign4VietnameseString(u.nickName.ToLower()).Contains(model.name))
            //                    select u).ToList();
            

            List<User> users = (from u in context.Users
                                where (u.nickName.ToLower().Contains(model.name))
                                select u).ToList();
            return users;
        }
    }
}