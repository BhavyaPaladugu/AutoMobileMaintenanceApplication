using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using VehicleMaintenance.DAL.DataAccess.Contract;
using VehicleMaintenance.DAL.Models;

namespace VehicleMaintenance.DAL.DataAccess
{
    public class VehicleTypeRepository : BaseRepository<VehicleType>, IVehicleTypeRepository
    {
        public override IEnumerable<VehicleType> GetAll()
        {
            return db.VehicleTypes.AsEnumerable();
        }

        public override VehicleType GetById(int? id)
        {
            return db.VehicleTypes.Find(id);
        }

        public override void Save(VehicleType vt)
        {
            db.VehicleTypes.Add(vt);
            db.SaveChanges();
        }

        public override void Delete(VehicleType vt)
        {
            db.VehicleTypes.Remove(vt);
            db.SaveChanges();
        }
    }
}