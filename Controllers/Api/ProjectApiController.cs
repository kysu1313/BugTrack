using BugTrack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BugTrack.Controllers.Api
{
    public class ProjectApiController : ApiController
    {
        private ApplicationDbContext _context;

        public ProjectApiController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/projects
        public IEnumerable<Project> GetProjects()
        {
            return _context.Projects.ToList();
        }

        public Project GetProject(int id)
        {
            var project = _context.Projects.SingleOrDefault(c => c.Id == id);
            if (project == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return project;
        }

        // POST /api/projects
        [HttpPost]
        public Project CreateProject(Project project)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            _context.Projects.Add(project);
            _context.SaveChanges();
            return project;
        }

        // PUT /api/projects/1
        [HttpPut]
        public void UpdateProject(int id, Project project)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var projectInDb = _context.Projects.SingleOrDefault(c => c.Id == id);

            if (projectInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            projectInDb.Id = project.Id;
            projectInDb.ProjectName = project.ProjectName;
            projectInDb.OS = project.OS;
            projectInDb.Description = project.Description;
            projectInDb.Bugs = project.Bugs;
            projectInDb.Users = project.Users;

            _context.SaveChanges();
        }

        // DELETE /api/projects/1
        [HttpDelete]
        public void DeleteProject(int id)
        {

            var projectInDb = _context.Projects.SingleOrDefault(c => c.Id == id);
            if (projectInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            _context.Projects.Remove(projectInDb);
            _context.SaveChanges();
        }
    }
}
