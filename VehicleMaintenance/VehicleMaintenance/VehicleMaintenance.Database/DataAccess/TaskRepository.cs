using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using VehicleMaintenance.DAL.DataAccess.Contract;
using VehicleMaintenance.DAL.Models;

namespace VehicleMaintenance.DAL.DataAccess
{
    public class TaskRepository : BaseRepository<Task>, ITaskRepository
    {
        public override IEnumerable<Task> GetAll()
        {
            return db.Tasks.AsEnumerable();
        }

        public override Task GetById(int? id)
        {
            return db.Tasks.Find(id);
        }

        public override void Save(Task t)
        {
            db.Tasks.Add(t);
            db.SaveChanges();
        }

        public override void Delete(Task t)
        {
            db.Tasks.Remove(t);
            db.SaveChanges();
        }
    }
}