using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.App_class
{
    public class Room
    {
        public bool updateStatus(int id)
        {
            using (TestTEduDbEntities db = new TestTEduDbEntities())
            {
                var result = from a in db.RoomTbs
                             where a.ID_room == id
                             select a;
                if (result == null) return false;
                try
                {
                    result.FirstOrDefault().Status = 1;
                    db.Entry(result).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return false;
                }
                return true;
            }
        }
    }
}