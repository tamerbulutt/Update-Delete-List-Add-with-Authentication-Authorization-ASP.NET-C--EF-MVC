using FormWorks.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FormWorks.Controllers
{
    public class PersonalController : Controller
    {
        MVCFormWorksEntities db = new MVCFormWorksEntities();
        public ActionResult List()
        {
            var model = db.tblPersonal.ToList();
            return View(model);
        }
        [HttpGet]
        [Authorize(Roles ="A")]
        public ActionResult AddPersonal()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]//Saldırıları engellemek için token kullanımıyla beraber
        public ActionResult AddPersonal(tblPersonal personal)
        {
            if(!ModelState.IsValid)           
                return View("AddPersonal");//Viewdan dolayı personal nesnesini yollamaya gerek kalmadı
            
            db.tblPersonal.Add(personal);
            db.SaveChanges();
            return RedirectToAction("List","Personal");
        }

        public ActionResult Delete(int id) //jquery ajax ile de silinebilir daha performanslı olabilir
        {
            var deletedPersonal = db.tblPersonal.Find(id);

            if (deletedPersonal == null)
                return HttpNotFound();

            db.tblPersonal.Remove(deletedPersonal);
            db.SaveChanges();

            return RedirectToAction("List","Personal");
        }
        [HttpGet]
        public ActionResult Update(int id)
        {
            var updatedPersonal = db.tblPersonal.Find(id);

            if (updatedPersonal==null)         
                return HttpNotFound();   
            
            return View(updatedPersonal);
        }
        [HttpPost]
        public ActionResult Update(tblPersonal personal)
        {
            //var updatedPersonal = db.tblPersonal.Find(personal.ID);

            if (!ModelState.IsValid) //ServerSide validation control
                return View("Update");
            //Quick way for update!
            db.Entry(personal).State = System.Data.Entity.EntityState.Modified;
            
            /* Second way for update manual!
            updatedPersonal.Name = personal.Name;
            updatedPersonal.LastName = personal.LastName;
            updatedPersonal.City = personal.City;
            updatedPersonal.Price = personal.Price;*/

            db.SaveChanges();
            return RedirectToAction("List","Personal");
        }
    }
}