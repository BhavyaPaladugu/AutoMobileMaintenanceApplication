using System;
using System.Net;
using System.Web.Mvc;
using VehicleMaintenance.DAL.DataAccess;
using VehicleMaintenance.DAL.DataAccess.Contract;
using VehicleMaintenance.DAL.Models;

namespace VehicleMaintenance.Controllers
{
    public class TaskController : Controller
    {
        ITaskRepository tRep = new TaskRepository();

        // GET: Task
        public ActionResult Index()
        {
            return View(tRep.GetAll());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TaskName")]Task t)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    tRep.Save(t);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to save changes due to following error " + ex.Message);
            }

            return View(t);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task t = tRep.GetById(id);
            if (t == null)
            {
                return HttpNotFound();
            }

            return View(t);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task tToUpdate = tRep.GetById(id);
            if (TryUpdateModel(tToUpdate, "", new string[] { "TaskName" }))
            {
                try
                {
                    tRep.Save();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Unable to save changes due to following error " + ex.Message);
                }
            }
            return View(tToUpdate);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task t = tRep.GetById(id);
            if (t == null)
            {
                return HttpNotFound();
            }
            return View(t);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Task t = tRep.GetById(id);
            tRep.Delete(t);
            return RedirectToAction("Index");
        }
    }
}