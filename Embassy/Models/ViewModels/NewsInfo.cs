using EmbassyAPI.Models.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Embassy.Models.ViewModels
{
    public class NewsInfo
    {
        public int NEWSID { get; set; }
        [Required(ErrorMessage ="Xana daxil edilmelidir")]
        [MaxLength(200)]
        public string NEWSTITLE { get; set; }
        [Required(ErrorMessage = "Xana daxil edilmelidir")]
        public string NEWSCONTENT { get; set; }
        public string ISDELETED { get; set; }
        public string ISARCHIVE { get; set; }
        public DateTime DATEPOSTED { get; set; }
        public DateTime DATEMODIFIED { get; set; }
        public int AUTHORID { get; set; }
        public List<NEWS> newsInfo { get; set; }
    }
}