using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using BugTrack.Models;

namespace BugTrack.Controllers.Api
{
    public class BugApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/BugApi
        public IQueryable<Bug> GetBugs()
        {
            return db.Bugs;
        }

        // GET: api/BugApi/5
        [ResponseType(typeof(Bug))]
        public async Task<IHttpActionResult> GetBug(int id)
        {
            Bug bug = await db.Bugs.FindAsync(id);
            if (bug == null)
            {
                return NotFound();
            }

            return Ok(bug);
        }

        // PUT: api/BugApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBug(int id, Bug bug)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bug.Id)
            {
                return BadRequest();
            }

            db.Entry(bug).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BugExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/BugApi
        [ResponseType(typeof(Bug))]
        public async Task<IHttpActionResult> PostBug(Bug bug)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Bugs.Add(bug);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = bug.Id }, bug);
        }

        // DELETE: api/BugApi/5
        [ResponseType(typeof(Bug))]
        public async Task<IHttpActionResult> DeleteBug(int id)
        {
            Bug bug = await db.Bugs.FindAsync(id);
            if (bug == null)
            {
                return NotFound();
            }

            db.Bugs.Remove(bug);
            await db.SaveChangesAsync();

            return Ok(bug);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BugExists(int id)
        {
            return db.Bugs.Count(e => e.Id == id) > 0;
        }
    }
}