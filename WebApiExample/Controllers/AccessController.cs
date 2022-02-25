using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiExample.Models.WS;
using WebApiExample.Models;

namespace WebApiExample.Controllers
{
    public class AccessController : ApiController
    {
        [HttpGet]
        public Reply HolaMundo()
        {
            Reply oR = new Reply
            {
                result = 1,
                message = "Holi"
        };
            return oR;
        }
        [HttpPost]
        public Reply Login(AccessViewModel model)
        {
            Reply oR = new Reply();


            try
            {
                using (mydbEntities db = new mydbEntities())
                {
                    var lst = db.users.Where(d => d.email == model.email && d.password == model.password && d.idEstatus == 1);
                    if (lst.Count() > 0)
                    {
                        oR.result = 1;
                        oR.data = Guid.NewGuid().ToString();

                        user oUser = lst.First();
                        oUser.token = oR.data.ToString();
                        db.Entry(oUser).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        oR.message = "Datos incorrectos";
                    }
                }
            }
            catch (Exception e)
            {
                oR.message = e.ToString();
                throw;
            }
            return oR;
        }
    }
}
