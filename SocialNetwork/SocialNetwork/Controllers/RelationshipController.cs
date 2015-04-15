using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialNetwork.Models;

namespace SocialNetwork.Controllers
{
    public class RelationshipController : Controller
    {
        RelationshipModels relModel = new RelationshipModels();
        [HttpPost]
        public JsonResult AddFollow(int myAccId, int userId)
        {
            return Json(relModel.InsertFollow(myAccId,userId));
        }
        
    }
}