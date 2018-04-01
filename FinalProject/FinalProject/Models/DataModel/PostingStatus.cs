using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models.DataModel
{
    public class PostingStatus
    {
        public PostingStatus()
        {
            this.Postings = new HashSet<Posting>();
        }

        public int ID { get; set; }

        public string Status { get; set; }

        public ICollection<Posting> Postings { get; set; }
    }
}