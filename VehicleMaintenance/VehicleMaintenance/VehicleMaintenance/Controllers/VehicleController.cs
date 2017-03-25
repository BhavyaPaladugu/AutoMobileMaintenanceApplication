using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using VehicleMaintenance.DAL;
using VehicleMaintenance.DAL.DataAccess;
using VehicleMaintenance.DAL.DataAccess.Contract;
using VehicleMaintenance.DAL.Models;
using VehicleMaintenance.Database.DataAccess;
using VehicleMaintenance.Database.DataAccess.Contract;

namespace VehicleMaintenance.Controllers
{
    public class VehicleController : Controller
    {
        IVehicleRepository vRep = new VehicleRepository();

        // GET: Vehicle
        public ActionResult Index()
        {
            return View(vRep.GetAll());
        }

        public ActionResult Create()
        {
            PopulateVehicleTypesDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VehicleId,Make,Model,Year,OdometerReading,VehicleTypeId")]Vehicle v)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    vRep.Save(v);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to save changes due to following error " + ex.Message);
            }
            PopulateVehicleTypesDropDownList(v.VehicleTypeId);
            return View(v);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle v = vRep.GetById(id);
            if (v == null)
            {
                return HttpNotFound();
            }
            PopulateVehicleTypesDropDownList(v.VehicleTypeId);
            return View(v);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vToUpdate = vRep.GetById(id);
            if (TryUpdateModel(vToUpdate, "", 
                new string[] { "Make","Model","Year","OdometerReading","VehicleTypeId" }))
            {
                try
                {
                    vRep.Save();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Unable to save changes due to following error " + ex.Message);
                }
            }
            PopulateVehicleTypesDropDownList(vToUpdate.VehicleTypeId);
            return View(vToUpdate);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle v = vRep.GetById(id);
            if (v == null)
            {
                return HttpNotFound();
            }
            return View(v);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vehicle v = vRep.GetById(id);
            vRep.Delete(v);
            return RedirectToAction("Index");
        }

        private void PopulateVehicleTypesDropDownList(object selectedVehicleType = null)
        {
            IVehicleTypeRepository vtRep = new VehicleTypeRepository();
            var vehicletypesQuery = from d in vtRep.GetAll()
                                   orderby d.VehicleTypeName
                                   select d;
            ViewBag.VehicleTypeID = new SelectList(vehicletypesQuery, "VehicleTypeID", "VehicleTypeName", selectedVehicleType);
        }
    }
}