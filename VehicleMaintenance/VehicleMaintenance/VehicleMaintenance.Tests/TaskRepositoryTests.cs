using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using VehicleMaintenance.DAL.DataAccess;
using VehicleMaintenance.DAL.DataAccess.Contract;
using VehicleMaintenance.DAL.Models;

namespace VehicleMaintenance.Tests
{
    [TestClass]
    public class TaskRepositoryTests
    {
        [TestMethod]
        public void TaskTestsAllInOneMethod()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                var fixture = new Fixture();
                var autoTask = fixture.Create<Task>();
                ITaskRepository tRep = new TaskRepository();
                tRep.Save(autoTask);

                var insertedTask = tRep.GetById(autoTask.TaskId);
                Assert.AreEqual(insertedTask.TaskName, autoTask.TaskName);

                IEnumerable<Task> tList = tRep.GetAll().Where(v => v.TaskId == autoTask.TaskId);
                Assert.AreEqual(tList.Count(), 1);

                tRep.Delete(autoTask);
                var deletedTask = tRep.GetById(autoTask.TaskId);
                Assert.IsNull(deletedTask);
            }
        }
    }
}
