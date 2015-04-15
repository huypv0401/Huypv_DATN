using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocialNetwork.Models.ViewModel;

namespace SocialNetwork.Models
{
    public class AccountDetailModels
    {
        SocialNetworkEntities context = new SocialNetworkEntities();

        public User GetUserById(int id)
        {
            User user = (from u in context.Users
                         where u.userId == id
                         select u).FirstOrDefault();
            return user;
        }

        public string updateAvatarUser(int userId, string avatar)
        {
            User user = GetUserById(userId);
            string linkAvatarOld = user.avatar;
            user.avatar = avatar;
            try
            {
                context.SaveChanges();
                return linkAvatarOld;
            }
            catch (Exception)
            {
                return "";
            }
        }
        public bool UpdateUser(User user)
        {
            User _user = (from u in context.Users
                          where u.userId == user.userId
                          select u).FirstOrDefault();
            _user.nickName = user.nickName;
            _user.phone = user.phone;
            _user.birthday = user.birthday;
            var addr = InsertAddr(user);
            _user.Addr = addr;
            _user.addrId = addr.addrId;

            try
            {
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public Addr InsertAddr(User user)
        {
            Addr addr = new Addr();
            addr = user.Addr;
            var country = InsertCountry(addr);
            addr.Country = country;
            addr.conntryId = country.countyId;

            var exits = (from a in context.Addrs
                         where a.conntryId == addr.conntryId && a.Country.countyId == addr.Country.countyId
                         select a).FirstOrDefault();
            if (exits != null)
                return exits;
            else
            {
                try
                {
                    context.Addrs.Add(addr);
                    context.SaveChanges();
                    return addr;
                }
                catch (Exception)
                {
                    return addr;
                }
            }
        }

        public Country InsertCountry(Addr addr)
        {
            Country country = new Country();
            country = addr.Country;
            var county = InsertCounty(country);
            country.County = county;
            country.countyId = county.countyId;

            var exits = (from c in context.Countries
                         where c.countryName.ToLower() == country.countryName.ToLower()
                         select c).FirstOrDefault();
            if (exits != null)
            {
                exits.countyId = county.countyId;
                context.SaveChanges();
                return exits;
            }

            else
            {
                try
                {
                    context.Countries.Add(country);
                    context.SaveChanges();
                    return country;
                }
                catch (Exception)
                {
                    return country;
                }
            }
        }
        public County InsertCounty(Country country)
        {
            County county = new County();
            county = country.County;

            var exits = (from c in context.Counties
                         where c.countyName.ToLower() == county.countyName.ToLower()
                         select c).FirstOrDefault();
            if (exits != null)
                return exits;
            else
            {
                try
                {
                    context.Counties.Add(county);
                    context.SaveChanges();
                    return county;
                }
                catch (Exception)
                {
                    return county;
                }
            }
        }

        public bool ChangePass(ChangePassViewModel model, int userId)
        {
            User user = GetUserById(userId);
            string _password = Helpers.md5(model._password);
            if (user.password == _password)
            {
                user.password = Helpers.md5(model.password);
                try
                {
                    context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    
                }
            }
            return false;
        }

        public List<Favorite> GetListFavoriteByUserId(int userId)
        {
            var favorite_Use_Id = (from f in context.Favorite_User
                                   where f.userId == userId
                                   select f.favoriteId);
            List<Favorite> favorites = new List<Favorite>();
            foreach (var item in favorite_Use_Id)
            {
                var favorite = (from f in context.Favorites
                                where f.favoriteId == item
                                select f).FirstOrDefault();
                favorites.Add(favorite);
            }
            return favorites;
        }

        public List<Favorite> GetListFavorite()
        {
            var fav = (from f in context.Favorites select f).ToList();
            return fav;
        }
    }
}