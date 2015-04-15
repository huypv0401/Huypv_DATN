using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialNetwork.Models.ViewModel;
using SocialNetwork.Models;

namespace SocialNetwork.Controllers
{
    public class SearchController : Controller
    {
        SearchModels searchModel = new SearchModels();
        // GET: Search
        [Authorize]
        //public ActionResult Index()
        //{
        //    return View();
        //}
        //[Authorize]
        //public ActionResult Search(SearchViewModels model)
        //{
        //    List<User> users = searchModel.SearchValue(model);
        //    TempData["users"] = users;
        //    return RedirectToAction("ResultSearch");
        //}
        //[Authorize]
        //public ActionResult ResultSearch()
        //{
        //    List<User> users = TempData["users"] as List<User>;
        //    return View(users);
        //}


        //public ActionResult SearchAjax()
        //{
        //    return View();
        //}

        public JsonResult GetSearchResult(string search)
        {
            return Json(searchModel.GetSearchJsonResult(search));
        }
    }
}