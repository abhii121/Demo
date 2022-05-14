using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplication6;

namespace WebApplication6.Controllers
{
    public class userRegistrationsController : ApiController
    {
        private webapi1Entities db = new webapi1Entities();

        // GET: api/userRegistrations
        public IQueryable<userRegistration> GetuserRegistrations()
        {
            return db.userRegistrations;
        }

        // GET: api/userRegistrations/5
        [ResponseType(typeof(userRegistration))]
        public IHttpActionResult GetuserRegistration(int id)
        {
            userRegistration userRegistration = db.userRegistrations.Find(id);
            if (userRegistration == null)
            {
                return NotFound();
            }

            return Ok(userRegistration);
        }

        // PUT: api/userRegistrations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutuserRegistration(int id, userRegistration userRegistration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userRegistration.id)
            {
                return BadRequest();
            }

            db.Entry(userRegistration).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!userRegistrationExists(id))
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

        // POST: api/userRegistrations
        [ResponseType(typeof(userRegistration))]
        public IHttpActionResult PostuserRegistration(userRegistration userRegistration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.userRegistrations.Add(userRegistration);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = userRegistration.id }, userRegistration);
        }

        // DELETE: api/userRegistrations/5
        [ResponseType(typeof(userRegistration))]
        public IHttpActionResult DeleteuserRegistration(int id)
        {
            userRegistration userRegistration = db.userRegistrations.Find(id);
            if (userRegistration == null)
            {
                return NotFound();
            }

            db.userRegistrations.Remove(userRegistration);
            db.SaveChanges();

            return Ok(userRegistration);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool userRegistrationExists(int id)
        {
            return db.userRegistrations.Count(e => e.id == id) > 0;
        }
    }
}