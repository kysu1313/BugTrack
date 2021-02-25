using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTrack.Models
{
    public class GitHub
    {
        public string username { get; set; }
        public int numRepos { get; set; }
        public IEnumerable<RepCollections> repos { get; set; }
        public string url { get; set; }

    }
}