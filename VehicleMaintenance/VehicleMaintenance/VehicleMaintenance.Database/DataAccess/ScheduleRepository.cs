using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using VehicleMaintenance.DAL.DataAccess;
using VehicleMaintenance.DAL.Models;
using VehicleMaintenance.Database.DataAccess.Contract;

namespace VehicleMaintenance.Database.DataAccess
{
    public class ScheduleRepository : BaseRepository<Schedule>, IScheduleRepository
    {
        public override IEnumerable<Schedule> GetAll()
        {
            return db.Schedules
                .OrderBy(v => v.ScheduleId)
                .Include(v => v.Vehicle)
                .Include(vt => vt.Vehicle.VehicleType)
                .Include(t => t.Task)
                .AsEnumerable();
        }

        public override Schedule GetById(int? id)
        {
            return db.Schedules
                .OrderBy(v => v.ScheduleId)
                .Include(v => v.Vehicle)
                .Include(vt => vt.Vehicle.VehicleType)
                .Include(t => t.Task)
                .SingleOrDefault(y => y.ScheduleId == id);
        }

        public override void Save(Schedule s)
        {
            db.Schedules.Add(s);
            db.SaveChanges();
        }

        public override void Delete(Schedule s)
        {
            db.Schedules.Remove(s);
            db.SaveChanges();
        }
    }
}
