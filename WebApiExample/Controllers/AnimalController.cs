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
    public class AnimalController : BaseController
    {
        [HttpPost]
        public Reply Get([FromBody] SecurityViewModel model)
        {
            Reply oR = new Reply();
            oR.result = 0;

            if (!Verify(model.token))
            {
                oR.message = "No autorizado";
                return oR;
            }
            try
            {
                using (mydbEntities db = new mydbEntities())
                {
                    List<ListAnimalsViewModel> animals = (from d in db.animals
                                                          where d.idState == 1
                                                          select new ListAnimalsViewModel
                                                          {
                                                              Name = d.name,
                                                              Patas = d.patas
                                                          }).ToList();

                    oR.data = animals;
                    oR.result = 1;
                }

            }
            catch
            {
                oR.message = "Ocurrio un error en el server, intente mas tarde";
            }

            return oR;
        }
    }
}
