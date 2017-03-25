using System;
using System.Collections.Generic;
using VehicleMaintenance.DAL.DataAccess.Contract;

namespace VehicleMaintenance.DAL.DataAccess
{
    public abstract class BaseRepository<T> : IBaseRepository<T>
    {
        public VMEntities db; 

        public BaseRepository()
        {
            db = new VMEntities();
        }
        
        public abstract IEnumerable<T> GetAll();
        public abstract T GetById(int? id);
        public abstract void Save(T t);
        public abstract void Delete(T t);

        public void Save()
        {
            db.SaveChanges();
        }

    }
}