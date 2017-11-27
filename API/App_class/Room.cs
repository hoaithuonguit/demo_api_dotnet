using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.App_class
{
    public class Room
    {
        public bool updateStatus(int id, int key)
        {
            using (TestTEduDbEntities db = new TestTEduDbEntities())
            {
                var result = from a in db.RoomTbs
                             where a.ID_room == id
                             select a;
                if (result == null) return false;
                try
                {
                    if (key == 1)
                        result.SingleOrDefault().Status = 1;
                    else
                        result.SingleOrDefault().Status = 0;
                    db.Entry(result.SingleOrDefault()).State = System.Data.Entity.EntityState.Modified;
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