using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using VehicleMaintenance.DAL.DataAccess.Contract;
using VehicleMaintenance.DAL.Models;

namespace VehicleMaintenance.DAL.DataAccess
{
    public class InvalidAssociationRepository : BaseRepository<InvalidAssociation>, IInvalidAssociationRepository
    {
        public override IEnumerable<InvalidAssociation> GetAll()
        {
            return db.InvalidAssociations
            .OrderBy(v => v.InvalidAssociationId)
            .Include(v => v.VehicleType)
            .Include(t => t.Task)
            .AsEnumerable();
        }

        public override InvalidAssociation GetById(int? id)
        {
            return db.InvalidAssociations
                .OrderBy(v => v.InvalidAssociationId)
                .Include(v => v.VehicleType)
                .Include(t => t.Task)
                .SingleOrDefault(y => y.InvalidAssociationId == id);
        }

        public override void Save(InvalidAssociation iv)
        {
            db.InvalidAssociations.Add(iv);
            db.SaveChanges();
        }

        public override void Delete(InvalidAssociation iv)
        {
            db.InvalidAssociations.Remove(iv);
            db.SaveChanges();
        }
    }
}