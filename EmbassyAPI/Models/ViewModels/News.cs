using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmbassyAPI.Models.ViewModels
{
    public class News
    {
        [Key]
        public int NEWSID { get; set; }
        public int AUTHORID { get; set; }
        [Required(ErrorMessage = "Xana daxil edilmelidir")]
        [MaxLength(200)]
        public string NEWSTITLE { get; set; }
        [Required(ErrorMessage = "Xana daxil edilmelidir")]
        public string NEWSCONTENT { get; set; }
        public DateTime DATEPOSTED { get; set; }
        public DateTime DATEMODIFIED { get; set; }
        public string ISDELETED { get; set; }
        public string ISARCHIVE { get; set; }
    }
}