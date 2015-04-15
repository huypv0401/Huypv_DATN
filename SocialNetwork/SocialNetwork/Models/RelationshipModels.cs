using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetwork.Models
{
    public class RelationshipModels
    {
        SocialNetworkEntities context = new SocialNetworkEntities();
        public string InsertFollow(int myAccId, int userId)
        {
            if (CheckFollow(myAccId, userId))
            {
                if (RemoveFollow(myAccId, userId))
                    return "Theo dõi";
                return "Đang theo dõi";
            }

            else
            {
                Relationship rel = new Relationship();
                rel.user = myAccId;
                rel.userFollow = userId;
                rel.status = 1;
                try
                {
                    context.Relationships.Add(rel);
                    context.SaveChanges();
                    return "Đang theo dõi";
                }
                catch (Exception)
                {
                    return "Theo dõi";
                }
            }

        }

        public bool RemoveFollow(int myAccId, int userId)
        {
            var rel = (from r in context.Relationships
                       where r.user == myAccId && r.userFollow == userId
                       select r).FirstOrDefault();
            try
            {
                context.Relationships.Remove(rel);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }
        public bool CheckFollow(int myAccId, int userId)
        {
            var rel = (from r in context.Relationships
                       where r.user == myAccId && r.userFollow == userId
                       select r).FirstOrDefault();
            if (rel != null)
                return true;
            return false;
        }
    }
}