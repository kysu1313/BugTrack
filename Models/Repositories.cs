using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTrack.Models
{
    public class Repositories
    {
        public string name { get; set; }
        public string language { get; set; }
        public string owner { get; set; } //or login ?
        public string updated_at { get; set; }
    }

    public class RepCollections
    {
        private IEnumerable<Repositories> repositories;

        public IEnumerable<Repositories> Repositories { get => this.repositories; set => this.repositories = value; }
    }
}