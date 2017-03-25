using System.Data.Entity;
using VehicleMaintenance.DAL.Models;

namespace VehicleMaintenance.DAL.DataAccess.Contract
{
    public interface ITaskRepository : IBaseRepository<Task>
    {
    }
}
