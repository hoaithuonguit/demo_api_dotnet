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
    public class RoomController : ApiController
    {
        private TestTEduDbEntities db = new TestTEduDbEntities();

        // GET: api/Room 
        public IQueryable<Room_Model> GetRoomTbs()
        {
            var result = from a in db.RoomTbs
                         join b in db.BookedRoomTbs on a.ID_room equals b.ID_room into table
                         from c in table.DefaultIfEmpty()
                         join d in db.UserTbs on c.ID_user equals d.ID_User into table_result
                         from e in table_result.DefaultIfEmpty()
                         select new Room_Model() {
                             id = a.ID_room,
                             Name = a.Name,
                             Status = a.Status,
                             Description = a.Description,
                             BookedDate = a.Status==1?c.Date:DateTime.MinValue,
                             BookedPerson = e.UserName
                         };
            return result;
        }

        // GET: api/Room/5
        [ResponseType(typeof(RoomTb))]
        public IHttpActionResult GetRoomTb(int id)
        {
            RoomTb roomTb = db.RoomTbs.Find(id);
            if (roomTb == null)
            {
                return NotFound();
            }

            return Ok(roomTb);
        }

        // PUT: api/Room/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRoomTb(int id, RoomTb roomTb)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != roomTb.ID_room)
            {
                return BadRequest();
            }

            db.Entry(roomTb).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomTbExists(id))
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

        // POST: api/Room
        [ResponseType(typeof(RoomTb))]
        public IHttpActionResult PostRoomTb(RoomTb roomTb)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RoomTbs.Add(roomTb);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (RoomTbExists(roomTb.ID_room))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = roomTb.ID_room }, roomTb);
        }

        // DELETE: api/Room/5
        [ResponseType(typeof(RoomTb))]
        public IHttpActionResult DeleteRoomTb(int id)
        {
            RoomTb roomTb = db.RoomTbs.Find(id);
            if (roomTb == null)
            {
                return NotFound();
            }

            db.RoomTbs.Remove(roomTb);
            db.SaveChanges();

            return Ok(roomTb);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RoomTbExists(int id)
        {
            return db.RoomTbs.Count(e => e.ID_room == id) > 0;
        }
    }
}