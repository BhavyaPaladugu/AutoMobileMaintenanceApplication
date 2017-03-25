using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleMaintenance.DAL.DataAccess.Contract;
using VehicleMaintenance.DAL.Models;

namespace VehicleMaintenance.Database.DataAccess.Contract
{
    public interface IVehicleRepository : IBaseRepository<Vehicle>
    {
    }
}
