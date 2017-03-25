using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using VehicleMaintenance.DAL.DataAccess;
using VehicleMaintenance.DAL.DataAccess.Contract;
using VehicleMaintenance.DAL.Models;

namespace VehicleMaintenance.Controllers
{
    public class InvalidAssociationController : Controller
    {
        IInvalidAssociationRepository inv = new InvalidAssociationRepository();

        // GET: InvalidAssociation
        public ActionResult Index()
        {
            return View(inv.GetAll());
        }

        public ActionResult Create()
        {
            PopulateVehicleTypesDropDownList();
            PopulateTasksDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InvalidAssociationId,VehicleTypeId,TaskId")]InvalidAssociation iv)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    inv.Save(iv);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to save changes due to following error " + ex.Message);
            }
            PopulateVehicleTypesDropDownList(iv.VehicleTypeId);
            PopulateTasksDropDownList(iv.TaskId);
            return View(iv);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvalidAssociation iv = inv.GetById(id);
            if (iv == null)
            {
                return HttpNotFound();
            }
            PopulateVehicleTypesDropDownList(iv.VehicleTypeId);
            PopulateTasksDropDownList(iv.TaskId);
            return View(iv);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvalidAssociation ivToUpdate = inv.GetById(id);
            if (TryUpdateModel(ivToUpdate, "", new string[] { "VehicleTypeId", "TaskId" }))
            {
                try
                {
                    inv.Save();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Unable to save changes due to following error " + ex.Message);
                }
            }
            PopulateVehicleTypesDropDownList(ivToUpdate.VehicleTypeId);
            PopulateTasksDropDownList(ivToUpdate.TaskId);
            return View(ivToUpdate);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvalidAssociation iv = inv.GetById(id);
            if (iv == null)
            {
                return HttpNotFound();
            }
            return View(iv);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InvalidAssociation iv = inv.GetById(id);
            inv.Delete(iv);
            return RedirectToAction("Index");
        }

        private void PopulateVehicleTypesDropDownList(object selectedVehicleType = null)
        {
            IVehicleTypeRepository vtRep = new VehicleTypeRepository(); 
            var vehicleTypesQuery = from d in vtRep.GetAll()
                                orderby d.VehicleTypeId
                                select d;
            ViewBag.VehicleTypeId = new SelectList(vehicleTypesQuery, "VehicleTypeId", "VehicleTypeName", selectedVehicleType);
        }

        private void PopulateTasksDropDownList(object selectedTask = null)
        {
            ITaskRepository tRep = new TaskRepository();
            var tasksQuery = from d in tRep.GetAll()
                             orderby d.TaskId
                             select d;
            ViewBag.TaskId = new SelectList(tasksQuery, "TaskId", "TaskName", selectedTask);
        }
    }
}