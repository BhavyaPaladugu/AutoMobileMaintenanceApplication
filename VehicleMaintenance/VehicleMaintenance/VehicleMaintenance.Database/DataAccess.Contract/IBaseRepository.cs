using System.Collections.Generic;

namespace VehicleMaintenance.DAL.DataAccess.Contract
{
    public interface IBaseRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetById(int? id);
        void Save(T t);
        void Delete(T t);
        void Save();
    }
}
