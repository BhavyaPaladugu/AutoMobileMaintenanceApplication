using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleMaintenance.DAL.DataAccess;
using VehicleMaintenance.DAL.Models;
using VehicleMaintenance.Database.DataAccess.Contract;

namespace VehicleMaintenance.Database.DataAccess
{
    public class VehicleRepository : BaseRepository<Vehicle>, IVehicleRepository
    {
        public override IEnumerable<Vehicle> GetAll()
        {
            return db.Vehicles
                .OrderBy(v => v.VehicleId)
                .Include(v => v.VehicleType)
                .AsEnumerable();
        }

        public override Vehicle GetById(int? id)
        {
            return db.Vehicles
                .Include(x => x.VehicleType)
                .SingleOrDefault(y => y.VehicleId == id);
        }

        public override void Save(Vehicle v)
        {
            db.Vehicles.Add(v);
            db.SaveChanges();
        }

        public override void Delete(Vehicle v)
        {
            db.Vehicles.Remove(v);
            db.SaveChanges();
        }
    }
}
