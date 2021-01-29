using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BugTrack.Models;

namespace BugTrack.Models
{
    public class Bug
    {
        public int Id { get; set; }
        public Severity BugSeverity { get; set; }
        public string Component { get; set; }
        [Required]
        public Project project { get; set; }
        [Display(Name = "Bug Description")]
        public string BugDescription { get; set; }
        public Boolean Resolved { get; set; }
        [NotMapped]
        public virtual User AssocUser { get; set; }

    }

    //public enum Severity
    //{
    //    One = 1,
    //    Two = 2,
    //    Three = 3,
    //    Four = 4
    //}
}
