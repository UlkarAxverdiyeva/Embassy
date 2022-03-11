using EmbassyAPI.Models.DataModels;
using EmbassyAPI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Embassy.Controllers
{
    public class ReadNewsController : Controller
    {
        embassyEntities db = new embassyEntities();
        public static HttpClient webClient = new HttpClient();

        public ActionResult GetNews()
        {
            IEnumerable<News> news;
            try
            {
                HttpResponseMessage webResponse = webClient.GetAsync(ConfigurationManager.AppSettings["NewsURL"].ToString()).Result;
                news = webResponse.Content.ReadAsAsync<IEnumerable<News>>().Result.Where(s => s.ISDELETED == ('N').ToString() && s.ISARCHIVE == ('N'.ToString())).OrderBy(s => s.NEWSID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(news);
        }

        public ActionResult EventCalendar()
        {
           // var check = DateTime.Parse(("2017-12-11").ToString()).ToString("MMMM");
            IEnumerable<News> news;
            try
            {
                HttpResponseMessage webResponse = webClient.GetAsync(ConfigurationManager.AppSettings["NewsURL"].ToString()).Result;
                news = webResponse.Content.ReadAsAsync<IEnumerable<News>>().Result.Where(s => s.ISDELETED == ('N').ToString() && s.ISARCHIVE == ('N'.ToString())).OrderBy(s => s.NEWSID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(news);
        }

        public ActionResult OfficialLogoandApplicationforEndorsments()
        {
            IEnumerable<News> news;
            try
            {
                HttpResponseMessage webResponse = webClient.GetAsync(ConfigurationManager.AppSettings["NewsLogoURL"].ToString()).Result;
                news = webResponse.Content.ReadAsAsync<IEnumerable<News>>().Result.Where(s => s.ISDELETED == ('N').ToString() && s.ISARCHIVE == ('N'.ToString())).OrderBy(s => s.NEWSID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            
            return View(news);
        }
    }
}