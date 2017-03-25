using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using VehicleMaintenance.DAL;
using VehicleMaintenance.DAL.DataAccess;
using VehicleMaintenance.DAL.DataAccess.Contract;
using VehicleMaintenance.DAL.Models;

namespace VehicleMaintenance.Controllers
{
    public class VehicleTypeController : Controller
    {
        private IVehicleTypeRepository vtRep = new VehicleTypeRepository();

        // GET: VehicleType
        public ActionResult Index()
        {
            return View(vtRep.GetAll());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VehicleTypeName")]VehicleType vt)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    vtRep.Save(vt);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to save changes due to following error " + ex.Message);
            }
            
            return View(vt);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleType vt = vtRep.GetById(id);
            if (vt == null)
            {
                return HttpNotFound();
            }
            
            return View(vt);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var vtToUpdate = vtRep.GetById(id);
            if (TryUpdateModel(vtToUpdate, "", new string[] { "VehicleTypeName" }))
            {
                try
                {
                    vtRep.Save();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Unable to save changes due to following error " + ex.Message);
                }
            }            
            return View(vtToUpdate);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleType vt = vtRep.GetById(id);
            if (vt == null)
            {
                return HttpNotFound();
            }
            return View(vt);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VehicleType vt = vtRep.GetById(id);
            vtRep.Delete(vt);
            return RedirectToAction("Index");
        }
    }
}