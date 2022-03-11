using Embassy.Models.ViewModels;
using EmbassyAPI.Models.DataModels;
using EmbassyAPI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Embassy.Models.Roles;

namespace Embassy.Controllers
{
    //[Authorize]
    //[Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        embassyEntities db = new embassyEntities();
        public static HttpClient webClient = new HttpClient();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult CheckValidUser(Login login)
        {
            string result = "Fail";

            using (HttpClient webClient = new HttpClient())
            {
                HttpResponseMessage webResponse = webClient.GetAsync(ConfigurationManager.AppSettings["AuthorsURL"].ToString()).Result;
                var validAuthor = webResponse.Content.ReadAsAsync<IEnumerable<Authors>>().Result.Where(s => s.AuthorEmail == login.email && s.AuthorPass == login.password).OrderBy(s => s.AuthorPass);
                if (validAuthor.Any())        // != null get error
                    result = "Success";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //[AllowAnonymous]
        public ActionResult AddNewsView()
        {

            //CustomRoleProvider customRoleProvider = new CustomRoleProvider();
            //if (customRoleProvider.IsUserInRole("ulkar.axverdiyeva@gmail.com", "Admin"))
                return View();
         //   return HttpNotFound();
        }

        [HttpPost]
        public JsonResult AddNewsView(NewsInfo ninfo)
        {
            string result = "Fail";
            if (ModelState.IsValid)
            {
                webClient.BaseAddress = new Uri("https://localhost:44399/api/embassyAPI/AddNewNews");
                var inserted = webClient.PostAsJsonAsync<NewsInfo>("AddNewNews", ninfo);
                inserted.Wait();
                var savedData = inserted.Result;
                if (savedData.IsSuccessStatusCode)
                    result = "Success";
            }

            return Json(result, JsonRequestBehavior.AllowGet);

        }
        public ActionResult UpdateNews()
        {
            // IEnumerable<News> news;
            NewsInfo ni = new NewsInfo();

            try
            {
                HttpResponseMessage webResponse = webClient.GetAsync(ConfigurationManager.AppSettings["NewsURL"].ToString()).Result;
                ni.newsInfo = webResponse.Content.ReadAsAsync<IEnumerable<NEWS>>().Result.Where(s => s.ISDELETED == ('N').ToString() && s.ISARCHIVE == ('N'.ToString())).OrderBy(s => s.NEWSID).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(ni);
        }
        [HttpPost]
        public JsonResult UpdateNews(NEWS ninfo)
        {
            string Message = "Fail";
            HttpClient webClient = new HttpClient(); // without instance error - this instance has alredy requests one or more time.properties can only be modified before sending the first request 
            webClient.BaseAddress = new Uri("https://localhost:44399/api/embassyAPI/UpdateNews");
            var updated = webClient.PostAsJsonAsync<NEWS>("UpdateNews", ninfo);
            updated.Wait();

            var savedData = updated.Result;
            if (savedData.IsSuccessStatusCode)
                Message = "Success";
            return Json(Message, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteNews(/*NEWS news*/int id)
        {
            string Message = "Fail";
            HttpClient webClient = new HttpClient(); // without instance error - this instance has alredy requests one or more time.properties can only be modified before sending the first request 
            webClient.BaseAddress = new Uri("https://localhost:44399/api/embassyAPI/DeleteNews");
            var deleted = webClient.PostAsJsonAsync/*<NEWS>*/($"DeleteNews/{id}", /*news*/ id);
            deleted.Wait();

            var savedData = deleted.Result;
            if (savedData.IsSuccessStatusCode)
                Message = "Success";
            return Json(Message, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LoadData(int id)
        {
            NewsInfo m = new NewsInfo
            {
                newsInfo = db.NEWS.Where(x => x.NEWSID == id).ToList()
            };
            return PartialView(m);
        }
    }
}