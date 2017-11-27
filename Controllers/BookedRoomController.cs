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
using API.Models;

namespace API.Controllers
{
    public class BookedRoomController : ApiController
    {
        private TestTEduDbEntities db = new TestTEduDbEntities();

        // GET: api/BookedRoom
        public IQueryable<BookedRoomTb> GetBookedRoomTbs()
        {
            return db.BookedRoomTbs;
        }

        // GET: api/BookedRoom/5
        [ResponseType(typeof(BookedRoomTb))]
        public IHttpActionResult GetBookedRoomTb(int id)
        {
            BookedRoomTb bookedRoomTb = db.BookedRoomTbs.Find(id);
            if (bookedRoomTb == null)
            {
                return NotFound();
            }

            return Ok(bookedRoomTb);
        }

        // PUT: api/BookedRoom/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBookedRoomTb(int id, BookedRoomTb bookedRoomTb)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bookedRoomTb.ID)
            {
                return BadRequest();
            }

            db.Entry(bookedRoomTb).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookedRoomTbExists(id))
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

        // POST: api/BookedRoom
        [ResponseType(typeof(BookedRoomTb))]
        public IHttpActionResult PostBookedRoomTb(BookedRoomTb bookedRoomTb)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BookedRoomTbs.Add(bookedRoomTb);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (BookedRoomTbExists(bookedRoomTb.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = bookedRoomTb.ID }, bookedRoomTb);
        }

        // DELETE: api/BookedRoom/5
        [ResponseType(typeof(BookedRoomTb))]
        public IHttpActionResult DeleteBookedRoomTb(int id)
        {
            BookedRoomTb bookedRoomTb = db.BookedRoomTbs.Find(id);
            if (bookedRoomTb == null)
            {
                return NotFound();
            }

            db.BookedRoomTbs.Remove(bookedRoomTb);
            db.SaveChanges();

            return Ok(bookedRoomTb);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BookedRoomTbExists(int id)
        {
            return db.BookedRoomTbs.Count(e => e.ID == id) > 0;
        }
    }
}