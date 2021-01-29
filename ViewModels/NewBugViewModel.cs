using BugTrack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTrack.ViewModels
{
    public class NewBugViewModel
    {
        public IEnumerable<Severity> Severities { get; set; }
        public User User { get; set; }
        public Project Project { get; set; }

        public Bug Bug { get; set; }

    }
}