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
                    List<ListAnimalsViewModel> animals = List(db);
                    oR.result = 1;
                    oR.data = animals;
                }
            }
            catch
            {
                oR.message = "Ocurrio un error en el server, intente mas tarde";
            }
            return oR;
        }
        [HttpPost]
        public Reply Add([FromBody] AnimalViewModel model)
        {
            Reply oR = new Reply();
            oR.result = 0;

            if (!Verify(model.token))
            {
                oR.message = "No autorizado";
                return oR;
            }
            //validaciones
            if (!Validate(model))
            {
                oR.message = error;
                return oR;
            }
            try
            {
                using (mydbEntities db = new mydbEntities())
                {
                    animal oAnimal = new animal();
                    oAnimal.idState = 1;
                    oAnimal.name = model.Nombre;
                    oAnimal.patas = model.Patas;
                    db.animals.Add(oAnimal);
                    db.SaveChanges();

                    List<ListAnimalsViewModel> animals = List(db);
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
        [HttpPut]
        public Reply Edit([FromBody] AnimalViewModel model)
        {
            Reply oR = new Reply();
            oR.result = 0;

            if (!Verify(model.token))
            {
                oR.message = "No autorizado";
                return oR;
            }
            //validaciones
            if (!Validate(model))
            {
                oR.message = error;
                return oR;
            }
            try
            {
                using (mydbEntities db = new mydbEntities())
                {
                    animal oAnimal = db.animals.Find(model.Id);
                    oAnimal.name = model.Nombre;
                    oAnimal.patas = model.Patas;

                    db.Entry(oAnimal).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    List<ListAnimalsViewModel> animals = List(db);
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
        [HttpDelete]
        public Reply Delete([FromBody] AnimalViewModel model)
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
                    animal oAnimal = db.animals.Find(model.Id);
                    oAnimal.idState = 2;
                    db.Entry(oAnimal).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    List<ListAnimalsViewModel> animals = List(db);
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
        #region HELPER
        private List<ListAnimalsViewModel> List(mydbEntities db)
        {
            return (from d in db.animals
                    where d.idState == 1
                    select new ListAnimalsViewModel
                    {
                        Name = d.name,
                        Patas = d.patas
                    }).ToList();
        }
        private bool Validate(AnimalViewModel model)
        {
            if (String.IsNullOrEmpty(model.Nombre))
            {
                error = "Nombre Obligatorio";
                return false;
            }
            return true;
        }
        #endregion
    }
}
