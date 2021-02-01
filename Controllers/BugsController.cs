using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BugTrack.Models;
using BugTrack.ViewModels;

namespace BugTrack.Controllers
{
    public class BugsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Bugs
        [AllowAnonymous]
        public async Task<ActionResult> Index()
        {
            if (User.IsInRole("CanManageProjects"))
            {
                return View("Index", await db.Bugs.ToListAsync());
            }
            return View("RestrictedAccessIndex", await db.Bugs.ToListAsync()); 
        }

        // GET: Bugs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bug bug = await db.Bugs.FindAsync(id);
            if (bug == null)
            {
                return HttpNotFound();
            }
            return View(bug);
        }

        // GET: Bugs/Create
        //[Route("Home")]
        public async Task<ActionResult> CreateFromProject(int? id)
        {

            Bug bugNew = new Bug();
            Project project = await db.Projects.FindAsync(id);
            //var severity = db.Severity.ToList();

            NewBugViewModel newBugViewModel = new NewBugViewModel
            {
                Bug = bugNew,
                Project = project,
                //Severities = severity,
            };

            newBugViewModel.Bug.projectId = project.Id;

            return View(newBugViewModel);
        }

        // POST: Bugs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Severity,Component,BugDescription,Resolved")] Bug bug)
        {

            if (ModelState.IsValid)
            {
                db.Bugs.Add(bug);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(bug);
        }

        // POST: Bugs/CreateFromProject
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SubmitBugFromProject([Bind(Include = "Severities,User,Project,Bug")] NewBugViewModel newBugViewModel)
        {
            Bug bug = newBugViewModel.Bug;
            if (ModelState.IsValid)
            {
                db.Bugs.Add(bug);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(bug);
        }

        // GET: Bugs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bug bug = await db.Bugs.FindAsync(id);
            if (bug == null)
            {
                return HttpNotFound();
            }
            return View(bug);
        }

        // POST: Bugs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Severity,Component,BugDescription,Resolved")] Bug bug)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bug).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(bug);
        }

        // GET: Bugs/Delete/5
        [Authorize]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bug bug = await db.Bugs.FindAsync(id);
            if (bug == null)
            {
                return HttpNotFound();
            }
            return View(bug);
        }

        // POST: Bugs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Bug bug = await db.Bugs.FindAsync(id);
            db.Bugs.Remove(bug);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
