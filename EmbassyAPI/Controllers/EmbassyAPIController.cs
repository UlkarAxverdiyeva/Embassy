using EmbassyAPI.Models.DataModels;
using EmbassyAPI.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace EmbassyAPI.Controllers
{
    public class EmbassyAPIController : ApiController
    {
        [Route("api/embassyAPI/GetAllNews")]
        public IHttpActionResult GetAllNews()
        {
            using (embassyEntities db = new embassyEntities())
            {
                db.Database.Connection.Open();
                //IList<News> NewsModel = (from p in db.NEWS
                //                                        select new News {NEWSID = (int)p.NEWSID, NEWSCONTENT = p.NEWSCONTENT,NEWSTITLE = p.NEWSTITLE ,DATEPOSTED = (DateTime)p.DATEPOSTED
                //                                        ,ISDELETED = p.ISDELETED,ISARCHIVE = p.ISARCHIVE,AUTHORID =(int)p.AUTHORID,DATEMODIFIED = (DateTime)p.DATEMODIFIED
                //                                        }).ToList();
                IList<News> news = null;

                news = db.NEWS.Select(s => new News()
                {
                    NEWSID = (int)s.NEWSID,
                    NEWSCONTENT = s.NEWSCONTENT,
                    NEWSTITLE = s.NEWSTITLE,
                    DATEPOSTED =(DateTime) s.DATEPOSTED,
                    ISDELETED = s.ISDELETED,
                    ISARCHIVE = s.ISARCHIVE,
                    AUTHORID = (int)s.AUTHORID,
                    DATEMODIFIED = (DateTime)s.DATEMODIFIED

                }).ToList<News>();

                return Ok(news);
            }
        }

        [Route("api/embassyAPI/GetAllNewsLogo")]
        public IHttpActionResult GetAllNewsLogo()
        {
            using (embassyEntities db = new embassyEntities())
            {
                db.Database.Connection.Open();
                //IList<News> NewsModel = (from p in db.NEWS_LOGO
                //                         select new News()
                //                         {
                //                             NEWSID = (int)p.ID,
                //                             NEWSCONTENT = p.CONTENT,
                //                             NEWSTITLE = p.TITLE,
                //                             DATEPOSTED = (DateTime)p.DATEPOSTED,
                //                             ISDELETED = p.ISDELETED,
                //                             ISARCHIVE = p.ISARCHIVED
                //                         }).ToList();
                //}
                IList<News> news = null;

                news = db.NEWS_LOGO.Select(s => new News()
                {
                    NEWSID = (int)s.ID,
                    NEWSCONTENT = s.CONTENT,
                    NEWSTITLE = s.TITLE,
                    DATEPOSTED = (DateTime)s.DATEPOSTED,
                    ISDELETED = s.ISDELETED,
                    ISARCHIVE = s.ISARCHIVED

                }).ToList<News>();

                return Ok(news);
            }
        }

        [Route("api/embassyAPI/GetAuthorsInfo")]
        public IHttpActionResult GetAuthorsInfo()
        {
            using(embassyEntities db = new embassyEntities())
            {
                db.Database.Connection.Open();
                //IList<Authors> authorsModel = (from a in db.NEWSAUTHORS
                //                             select new Authors()
                //                             {
                //                                 AuthorID = (int)a.AUTHORID,
                //                                 AuthorEmail = a.AUTHOREMAIL,
                //                                 AuthorPass = a.PASSWORD
                //                             }).ToList();

                IList<Authors> authors = db.NEWSAUTHORS.Select(x => new Authors()
                { AuthorID = (int)x.AUTHORID,
                  AuthorEmail = x.AUTHOREMAIL,
                  AuthorPass = x.PASSWORD
                }).ToList<Authors>();

                return Ok(authors);
            }
        }
        [HttpPost]
        [Route("api/embassyAPI/AddNewNews")]
        public IHttpActionResult AddNewNews(NEWS news)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            using (embassyEntities db = new embassyEntities())
            {
                db.Database.Connection.Open();
                db.NEWS.Add(new NEWS()
                {
                    NEWSTITLE = news.NEWSTITLE,
                    NEWSCONTENT = JsonConvert.SerializeObject(news.NEWSCONTENT),
                    ISDELETED = ('N').ToString(),
                    ISARCHIVE = ('N').ToString(),
                    DATEMODIFIED = DateTime.Now,
                    DATEPOSTED = DateTime.Now
                });

                db.SaveChanges();
                return Ok();
            }
        }

        [HttpPost]
        [Route("api/embassyAPI/UpdateNews")]
        public IHttpActionResult UpdateNews(NEWS news)
        {
            using (embassyEntities db = new embassyEntities())
            {
                var updatedNews = db.NEWS.Where(x => x.NEWSID == news.NEWSID).FirstOrDefault<NEWS>();
                if (updatedNews != null)
                {
                    updatedNews.NEWSTITLE = news.NEWSTITLE;
                    updatedNews.NEWSCONTENT = news.NEWSCONTENT;
                    updatedNews.DATEMODIFIED = DateTime.Now;
                    updatedNews.DATEPOSTED = news.DATEPOSTED;
                    db.SaveChanges();
                }
                else
                    return NotFound();

                return Ok();
            }
        }

        [HttpPost]
        [Route("api/embassyAPI/DeleteNews/{id}")]
        public IHttpActionResult DeleteNews(/*NEWS news*/ int id)
        {
            using (embassyEntities db = new embassyEntities())
            {
                var deletedNews = db.NEWS.Where(x => x.NEWSID ==/*news.NEWSID*/ (int)id).FirstOrDefault();
                if (deletedNews != null)
                {
                    deletedNews.ISDELETED = ('Y').ToString();
                    deletedNews.DATEMODIFIED = DateTime.Now;
                  //  db.Entry(deletedNews).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }
                else
                    return NotFound();

                return Ok();
            }
        }
    }
}