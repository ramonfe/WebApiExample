using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiExample.Models;

namespace WebApiExample.Controllers
{
    public class BaseController : ApiController
    {
        public string error = "";
        public bool Verify(string token)
        {
            using (mydbEntities db = new mydbEntities())
            {
                if (db.users.Where(d => d.token == token && d.idEstatus == 1).Count() > 0)
                    return true;
                else
                    return false;
            }
        }
    }
}
