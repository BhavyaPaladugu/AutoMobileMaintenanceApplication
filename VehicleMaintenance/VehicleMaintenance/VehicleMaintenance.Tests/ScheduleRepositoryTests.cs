using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using VehicleMaintenance.DAL.DataAccess;
using VehicleMaintenance.DAL.DataAccess.Contract;
using VehicleMaintenance.DAL.Models;
using VehicleMaintenance.Database.DataAccess;
using VehicleMaintenance.Database.DataAccess.Contract;

namespace VehicleMaintenance.Tests
{
    [TestClass]
    public class ScheduleRepositoryTests
    {
        [TestMethod]
        public void ScheduleTestsAllInOneMethod()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                var fixture = new Fixture();
                
                IVehicleRepository vRep = new VehicleRepository();
                var autoVehicle = fixture.Create<Vehicle>();
                vRep.Save(autoVehicle);                
                
                ITaskRepository tRep = new TaskRepository();
                var autoTask = fixture.Create<DAL.Models.Task>();
                tRep.Save(autoTask);

                IScheduleRepository sRep = new ScheduleRepository();
                var autoSchedule = fixture.Create<Schedule>();
                autoSchedule.Vehicle = autoVehicle;
                autoSchedule.Task = autoTask;
                autoSchedule.VehicleId = autoVehicle.VehicleId;
                autoSchedule.TaskId = autoTask.TaskId;           
                sRep.Save(autoSchedule);

                var insertedSchedule = sRep.GetById(autoSchedule.ScheduleId);
                Assert.AreEqual(insertedSchedule.Vehicle.Make, autoSchedule.Vehicle.Make);
                Assert.AreEqual(insertedSchedule.Vehicle.VehicleType.VehicleTypeName, autoSchedule.Vehicle.VehicleType.VehicleTypeName);
                Assert.AreEqual(insertedSchedule.Task.TaskName, autoSchedule.Task.TaskName);

                IEnumerable<Schedule> sList = sRep.GetAll().Where(sc => sc.ScheduleId == autoSchedule.ScheduleId); ;
                Assert.AreEqual(sList.Count(), 1);

                sRep.Delete(autoSchedule);
                var deletedSchedule = sRep.GetById(autoSchedule.ScheduleId);
                Assert.IsNull(deletedSchedule);
            }
        }
    }
}
