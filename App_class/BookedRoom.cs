using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.App_class
{
    public class BookedRoom
    {
        public bool CheckStatus(int id)
        {
            using (TestTEduDbEntities db = new TestTEduDbEntities())
            {
                var check = from a in db.RoomTbs
                            where a.ID_room == id && a.Status ==1
                            select a;
                return (check != null) ? true : false;// neu phong con trong = true
            }
        }
    }
}