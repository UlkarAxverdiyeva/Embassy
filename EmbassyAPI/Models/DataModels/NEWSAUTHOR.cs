//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EmbassyAPI.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class NEWSAUTHOR
    {
        public NEWSAUTHOR()
        {
            this.NEWS = new HashSet<NEWS>();
            this.NEWS_LOGO = new HashSet<NEWS_LOGO>();
        }
    
        public decimal AUTHORID { get; set; }
        public string AUTHORNAME { get; set; }
        public string AUTHOREMAIL { get; set; }
        public string USERNAME { get; set; }
        public string PASSWORD { get; set; }
        public string ISACTIVE { get; set; }
        public string ROLES { get; set; }
    
        public virtual ICollection<NEWS> NEWS { get; set; }
        public virtual ICollection<NEWS_LOGO> NEWS_LOGO { get; set; }
    }
}
