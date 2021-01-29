using BugTrack.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrack.Models
{
    public class Project
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }
        [Display(Name = "Operating System")]
        public string OS { get; set; }
        [Display(Name = "Description of Project")]
        [StringLength(100)] 
        public string Description { get; set; }
        public virtual IEnumerable<Bug> Bugs { get; set; }
        public virtual IEnumerable<User> Users { get; set; }
    }
}