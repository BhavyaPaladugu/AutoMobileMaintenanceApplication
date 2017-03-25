using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using VehicleMaintenance.DAL.DataAccess;
using VehicleMaintenance.DAL.DataAccess.Contract;
using VehicleMaintenance.DAL.Models;
using VehicleMaintenance.Database.DataAccess;
using VehicleMaintenance.Database.DataAccess.Contract;

namespace VehicleMaintenance.Controllers
{
    public class ScheduleController : Controller
    {
        IScheduleRepository sch = new ScheduleRepository();

        // GET: Schedule
        public ActionResult Index()
        {
            return View(sch.GetAll());
        }

        public ActionResult Create()
        {
            PopulateVehicleInfoDropDownList();
            PopulateTasksDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ScheduleId,VehicleId,TaskId")]Schedule s)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    sch.Save(s);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to save changes due to following error " + ex.Message);
            }
            PopulateVehicleInfoDropDownList(s.VehicleId);
            PopulateTasksDropDownList(s.TaskId);
            return View(s);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule s = sch.GetById(id);
            if (s == null)
            {
                return HttpNotFound();
            }
            PopulateVehicleInfoDropDownList(s.VehicleId);
            PopulateTasksDropDownList(s.TaskId);
            return View(s);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule sToUpdate = sch.GetById(id);
            if (TryUpdateModel(sToUpdate, "", new string[] { "VehicleId", "TaskId" }))
            {
                try
                {
                    sch.Save();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Unable to save changes due to following error " + ex.Message);
                }
            }
            PopulateVehicleInfoDropDownList(sToUpdate.VehicleId);
            PopulateTasksDropDownList(sToUpdate.TaskId);
            return View(sToUpdate);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule s = sch.GetById(id);
            if (s == null)
            {
                return HttpNotFound();
            }
            return View(s);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Schedule s = sch.GetById(id);
            sch.Delete(s);
            return RedirectToAction("Index");
        }

        private void PopulateVehicleInfoDropDownList(object selectedVehicle = null)
        {
            IVehicleRepository vRep = new VehicleRepository();
            var vehiclesQuery = from d in vRep.GetAll()
                                orderby d.VehicleId
                                    select new {
                                        VehicleId = d.VehicleId,
                                        VehicleInfo = d.Make + " " + d.Model + " - " + d.VehicleType.VehicleTypeName
                                    };
            ViewBag.VehicleId = new SelectList(vehiclesQuery, "VehicleId", "VehicleInfo", selectedVehicle);
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